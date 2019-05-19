using Mysql.MysqlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MysqlT
{
    class Program
    {
        /* static string key = "";
         static object sate = new object();
         static string h = "Beichen:";
         static List<string> cmds = new List<string>();
         static int cmdkeynum = 0;
         static int cmdline = Console.CursorTop;
         static void statar()
         {
             while (true)
             {
                 write("aaaa","cs", ConsoleColor.Blue);
                 Thread.Sleep(3000);
             }

         }
         public static void Main(string[] age)
         {
             Thread thread = new Thread(statar);
             thread.Start();
             // Console.CursorVisible = false;
             string info = "Read";
             while (true)
             {
                 ConsoleColor consoleColor = ConsoleColor.DarkGreen;
                 ConsoleKeyInfo aa = Console.ReadKey();
                 switch (aa.Key)
                 {
                     case ConsoleKey.Backspace:
                         key = key.Length == 0 ? "" : key.Substring(0, key.Length - 1); write("",info, consoleColor);
                         break;
                     case ConsoleKey.Delete:
                         key = key.Length == 0 ? "" : key.Substring(0, key.Length - 1);
                         write("",info, consoleColor);
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
                         write("",info, consoleColor);
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
                         write("",info, consoleColor);
                         break;
                     case ConsoleKey.Enter:
                         cmdkeynum = 0;
                         if (key.Trim().Length > 0)
                         {
                             cmds.Add(key);
                         }

                         string t = key;
                         key = "";
                         write(h + t,info, consoleColor);
                         write(App.Command.CommandExecution(t),info,consoleColor);

                         break;
                     default:
                         key = aa.KeyChar == 0 ? key : (key.Length + h.Length + 1) < Console.BufferWidth ? key + aa.KeyChar : key;
                         write("",info,consoleColor);
                         break;
                 }

             }
         }
             static void write(string data,string info, ConsoleColor color)
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
                     if (data != "" && data != null) { Console.Write(string.Format("[{0}:{1}:{2}][{3}]   {4}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, info, data)); }
                     Console.ForegroundColor = ConsoleColor.Red;
                     Console.Write(h + key);
                     cmdline += App.Function.GetStrAriseNum(data, "\n");
                     Console.SetCursorPosition((h + key).Length, cmdline);
                 }

             }*/
        public static void Main(string[] age) {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(@"
___.          .__       .__                   
\_ |__   ____ |__| ____ |  |__   ____   ____  
 | __ \_/ __ \|  |/ ___\|  |  \_/ __ \ /    \ 
 | \_\ \  ___/|  \  \___|   Y  \  ___/|   |  \
 |___  /\___  >__|\___  >___|  /\___  >___|  /
     \/     \/        \/     \/     \/     \/ 

            by:BeiChen In BeichenDream");
            Console.WriteLine();
            Console.WriteLine();
            App.Command.Write("","",ConsoleColor.DarkMagenta);
            Mysql.Config.init();
            App.Command.Run();
            
        }
        }
}
