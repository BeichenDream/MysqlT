using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysql.MysqlData
{
    [Serializable]
    public class MysqlClient
    {
        public MysqlClient(string host)
        {
            Host = host;
            Sate = 0;
            FileNumber = 0;
            ClientNumber = 1;
        }
        public string Host { get; }
        public int Sate { get; set; }
        public int FileNumber { get; set; }
        public int ClientNumber { get; set; }

        public override string ToString()
        {
            return $"Host:{Host} Sate:{Sate} FileNumber:{FileNumber} ClientNumber:{ClientNumber}";
        }
    }
}
