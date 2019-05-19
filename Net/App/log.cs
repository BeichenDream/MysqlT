using Mysql.MysqlData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysqlT.App
{
    class log
    {
        private static FileStream logstream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToLongDateString() + ".log", FileMode.Append, FileAccess.Write);
        public static Random random = new Random();
        public static void Write(string a)
        {
            byte[] data = Encoding.Default.GetBytes(a);
            logstream.Write(data,0, data.Length);
            logstream.Flush();

        }
        /*public static void Write(byte[] a)
        {
            string data = Encoding.UTF8.GetString(a);
            logstream.Write(a, 0, a.Length);
            logstream.Flush();
            Console.WriteLine(data);
        }
        public static void Write(UserInfo userInfo)
        {
            string data;
            data = string.Format("Username:{0} PassWord:{1} LoadData:{2} Salt{3} Info:{4}", userInfo.username, userInfo.password, userInfo.LoadData.ToString(), userInfo.Salt, userInfo.info);
            logstream.Write(Encoding.Default.GetBytes(data), 0, data.Length);
            logstream.Flush();
            Console.WriteLine(data);
        }*/
        public static void FileWirte(ref byte[] data, UserInfo userInfo)
        {

            string file = AppDomain.CurrentDomain.BaseDirectory + userInfo.host + "/";
            string[] t = Mysql.Config.GetConfig().File[userInfo.lastFile].Replace('\\', '/').Split('/');
            string dataname = t[t.Length - 1];
            Directory.CreateDirectory(file);
            FileStream datastream = new FileStream(file + dataname, FileMode.Append, FileAccess.Write);
            datastream.Write(data, 0, data.Length);
            datastream.Flush();
            datastream.Dispose();
        }
        public static void FileWirte(UserInfo userInfo)
        {

            string file = AppDomain.CurrentDomain.BaseDirectory + userInfo.host + "/";
            string[] t = Mysql.Config.GetConfig().File[userInfo.lastFile].Replace('\\', '/').Split('/');
            string dataname = t[t.Length - 1];
            Directory.CreateDirectory(file);
            FileStream datastream = new FileStream(file + dataname, FileMode.Append, FileAccess.Write);
            datastream.Write(userInfo.FileDataS.ToArray(), 0, userInfo.FileDataS.Count);
            datastream.Flush();
            datastream.Dispose();
        }
        public static void BkFileWirte(ref byte[] data,UserInfo userInfo)
        {

            string file = AppDomain.CurrentDomain.BaseDirectory + userInfo.host + "/";
            string[] t = Mysql.Config.GetConfig().File[userInfo.lastFile].Replace('\\', '/').Split('/');
            string dataname = t[t.Length - 1];
            Directory.CreateDirectory(file);
            FileStream datastream = new FileStream(file + "bk_" + dataname, FileMode.Append, FileAccess.Write);
            datastream.Write(data, 0, data.Length);
            datastream.Flush();
            datastream.Dispose();
        }
        public static void BkFileWirte(ref UserInfo userInfo)
        {

            string file = AppDomain.CurrentDomain.BaseDirectory + userInfo.host + "/";
            string[] t = Mysql.Config.GetConfig().File[userInfo.lastFile].Replace('\\', '/').Split('/');
            string dataname = t[t.Length - 1];
            Directory.CreateDirectory(file);
            FileStream datastream = new FileStream(file + "bk_" + dataname, FileMode.Append, FileAccess.Write);
            datastream.Write(userInfo.FileDataS.ToArray(), 0, userInfo.FileDataS.Count);
            datastream.Flush();
            datastream.Dispose();
        }
        public static string[] FileRead(string file)
        {

            FileStream stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read);

            byte[] t = new byte[stream.Length];
            string data = string.Empty;
            stream.Read(t, 0, Convert.ToInt32(stream.Length));
            data = Encoding.Default.GetString(t, 0, (int)stream.Length);
            stream.Dispose();
            return data.Replace("\r", "").Replace("\t", "").Replace(" ", "").Split('\n');
        }
    }
}
