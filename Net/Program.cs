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
        public static void Main(string[] age) {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(@"
___.          .__       .__                   
\_ |__   ____ |__| ____ |  |__   ____   ____  
 | __ \_/ __ \|  |/ ___\|  |  \_/ __ \ /    \ 
 | \_\ \  ___/|  \  \___|   Y  \  ___/|   |  \
 |___  /\___  >__|\___  >___|  /\___  >___|  /
     \/     \/        \/     \/     \/     \/ 

            by:BeiChen In BeichenDream

            Mail:rroort@qq.com");
            Console.WriteLine();
            Console.WriteLine();
            App.Command.Write("","",ConsoleColor.DarkMagenta);
            Mysql.Config.init();
            App.Command.Run();
            
        }
        }
}
