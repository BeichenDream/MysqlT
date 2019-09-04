using MysqlT.App;
using System;
using System.Collections.Generic;
using System.IO;
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
        public int packet_length { get; set; }
        public bool NextPacker { get; set; }
        public byte[] __packet  { get;set; }


        public bool Is_OneFilePacket { get; set; }
        public bool FileSate { get; set; }
        public int LoginSate { get; set; }
        public string Salt { get; set; }
        public int lastFile;
        public string Qpassword { get; set; }
        public Socket client { get; set; }
        public FileStream file { get; set; }
        public UserInfo()
        {
            __packet = new byte[4];
            NextPacker = true;
            packet_length = 1024;
            Qpassword = "Null";
            lastFile = 0;
            Salt = Function.GetSalt();
        }
    }
}
