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
        public static void FileWirte(ref byte[] data, UserInfo userInfo,bool allData)
        {
            userInfo.file.Write(data, 0, data.Length);
            userInfo.file.Flush();
        }
        public static void FileWirteInit(UserInfo userInfo) {
            string file = AppDomain.CurrentDomain.BaseDirectory + userInfo.host + "/";
            string[] t = Mysql.Config.GetConfig().File[userInfo.lastFile].Replace('\\', '/').Split('/');
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + userInfo.host + "/");
            string filename = t[t.Length - 1];
            userInfo.file = new FileStream(file + filename, FileMode.Append, FileAccess.Write);
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
