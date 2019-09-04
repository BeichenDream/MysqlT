using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MysqlT.App
{
  public  class Command
    {
      private  static string key = "";
      private  static object sate = new object();
      private  static string h = "MysqlT:";
      private  static List<string> cmds = new List<string>();
      private  static int cmdkeynum = 0;
      private  static int cmdline = Console.CursorTop;
        public static string CommandExecution(string cmdtext) {
            Type type = typeof(Mysql.Command);
            Regex regex = new Regex("\\S+");
            MatchCollection match = regex.Matches(cmdtext);
            if (match.Count<2)
            {
                return "操作不存在";
            }
            
            string MethodName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(match[0].Value) + Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(match[1].Value);
            object[] value = new object[match.Count-2];
            MethodInfo  methodInfo= type.GetMethod(MethodName,BindingFlags.Static|BindingFlags.Public);
            if (methodInfo != null)
            {
                MethodBase methodBase = methodInfo.GetBaseDefinition();
                ParameterInfo[] parameterInfos = methodBase.GetParameters();
                if (parameterInfos.Length == value.Length) {
                    for (int i = 0; i < value.Length; i++)
                    {
                        try
                        {
                           
                            value[i] = Convert.ChangeType(match[i + 2].Value, parameterInfos[i].ParameterType);
                        }
                        catch (Exception e)
                        {

                            return "参数类型不匹配 E:"+e.Message;
                        }
                    }
                    try
                    {
                        return (string)methodInfo.Invoke(type, value);
                        
                    }
                    catch (Exception e)
                    {
                        return "方法返回值有误 E:"+e.Message;
                    }
                  

                }
                else
                {
                    return "参数不满足";
                }

            }
            else {
                return "方法不存在";
            }
            
        }

      public  static void Write(string data, string info, ConsoleColor color)
        {
            data = data != null && data.Length > 0 ? data + "\n" : data;
            lock (sate)
            {
                Console.SetCursorPosition(0, cmdline);
                Console.ForegroundColor = color;
                for (int i = 0; i < Console.BufferWidth; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, cmdline == 0 ? 0 : cmdline);
                string str = string.Format("[{0}:{1}:{2}][{3}] {4}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, info, data);
                if (data != "" && data != null) { Console.Write(str);log.Write(str); }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(h + key);
                cmdline += App.Function.GetStrAriseNum(data, "\n");
                Console.SetCursorPosition((h + key).Length, cmdline);
            }

        }
        public static void Run()
        {
            string info = "Read";
            // Console.CursorVisible = false;
            while (true)
            {
                ConsoleColor consoleColor = ConsoleColor.DarkGreen;
                ConsoleKeyInfo aa = Console.ReadKey();
                switch (aa.Key)
                {
                    case ConsoleKey.Backspace:
                        key = key.Length == 0 ? "" : key.Substring(0, key.Length - 1); Write("",info, consoleColor);
                        break;
                    case ConsoleKey.Delete:
                        key = key.Length == 0 ? "" : key.Substring(0, key.Length - 1);
                        Write("",info, consoleColor);
                        break;
                    case ConsoleKey.UpArrow:
                        cmdkeynum = cmdkeynum == 0 ? cmds.Count : cmdkeynum - 1;
                        if (cmdkeynum <= 0)
                        {

                        }
                        else
                        {

                            key = cmds[cmdkeynum - 1];
                        }
                        Write("",info, consoleColor);
                        break;
                    case ConsoleKey.DownArrow:
                        cmdkeynum = cmdkeynum >= 0 && cmds.Count > 0 && cmdkeynum < cmds.Count ? cmdkeynum + 1 : 0;
                        if (cmdkeynum <= 0)
                        {

                        }
                        else
                        {

                            key = cmds[cmdkeynum - 1];
                        }
                        Write("",info, consoleColor);
                        break;
                    
                    case ConsoleKey.Enter:
                        cmdkeynum = 0;
                        if (key.Trim().Length > 0)
                        {
                            cmds.Add(key);
                        }

                        string t = key;
                        key = "";
                        Write(h + t,info, consoleColor);
                        Write(App.Command.CommandExecution(t),"Return", consoleColor);

                        break;
                    default:
                        key = aa.KeyChar == 0 ? key : (key.Length + h.Length + 1) < Console.BufferWidth ? key + aa.KeyChar : key;
                        Write("",info, consoleColor);
                        break;
                }

            }
        }
    }
}
