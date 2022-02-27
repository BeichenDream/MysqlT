using Mysql.MysqlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysqlT.App
{
    sealed class Function
    {
        /// <summary>
        /// 获取一个字符串子串的出现次数
        /// </summary>
        /// <param name="data">原字符串</param>
        /// <param name="str">子串</param>
        /// <returns></returns>
        public static int GetStrAriseNum(string data, string str)
        {

            return (data.Length - data.Replace(str, "").Length) / str.Length;
        }
        public static string GetSalt() {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null;
            string str = "";
            str += "0123456789!abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\\//*";
            for (int i = 0; i < 20; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
        public static string change(List<string> v) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("");
            foreach (var item in v)
            {
                stringBuilder.AppendLine("\t\t"+item);
            }
            return stringBuilder.ToString();
        }
        public static string change(Dictionary<string, string> v) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("");
            foreach (var item in v)
            {
                stringBuilder.AppendLine("\t\t" + item.Key+" : "+item.Value);
            }
            return stringBuilder.ToString();

        }
        public static string change(UserInfo userInfo)
        {
           // string data = string.Format("", userInfo.host, userInfo.username, userInfo.password, userInfo.Qpassword, userInfo.LoadData.ToString(), userInfo.Salt, userInfo.info);
            string data = $"Host:{userInfo.host} Username:{userInfo.username} PassWord:{userInfo.password} Qpassword:{userInfo.Qpassword} LoadData:{userInfo.LoadData.ToString()} Salt:{ userInfo.Salt} Info:{userInfo.info}";
            return data;
        }

    }
}
