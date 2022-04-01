using Mysql.MysqlData;
using MysqlT.App;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mysql
{
    [Serializable]
    public  class Config
    {
        public  Dictionary<string, MysqlClient> ClientInfo = new Dictionary<string, MysqlClient>();
        public  List<string> File = new List<string>();
        public  Dictionary<string, string> MysqlUser = new Dictionary<string, string>();
        public  bool LoginCheck = true;
        public string Host = "0.0.0.0";
        public int Port = 3306;
        public string Version = "5.7.54-log";
        private static Config config;
        private static object lockObj = new object();
        public static Config GetConfig() {
            if (config==null)
            {
                lock (lockObj)
                {
                    if (config==null)
                    {
                        config = new Config();
                    }
                }
            };
            return config;

        }
        private Config() {

        }


        public static void init() {
            using (FileStream stream = new FileStream("MysqlT.bin", FileMode.OpenOrCreate))
            {
                if (stream.Length > 0)
                {
                    BinaryFormatter binary = new BinaryFormatter();
                    config = (Config)binary.Deserialize(stream);
                }
            }
            GetConfig();
   
        }
        public static void SaveConfig() {
           
            using (FileStream stream = new FileStream("MysqlT.bin", FileMode.OpenOrCreate))
            {
                BinaryFormatter binary = new BinaryFormatter();
                binary.Serialize(stream, config);
                stream.Flush();
            }
            
        }
    }
}
