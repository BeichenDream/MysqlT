using MysqlT.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mysql.MysqlData
{
  public  class UserInfo
    {
        public string username { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public string info { get; set; }
        public bool LoadData { get; set; }


        public bool IsFileDataS { get; set; }
        public bool FileSate { get; set; }
        public int LoginSate { get; set; }
        public List<byte> FileDataS = new List<byte>();
        public string Salt { get; set; }
        public int lastFile;
        public string Qpassword { get; set; }
        public Socket client { get; set; }
        public UserInfo()
        {
            Qpassword = "Null";
            lastFile = 0;
            Salt = Function.GetSalt();
        }
    }
}
