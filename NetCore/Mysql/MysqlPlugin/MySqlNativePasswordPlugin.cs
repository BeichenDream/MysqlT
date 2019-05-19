using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mysql.MysqlPlugin
{
    sealed class MySqlNativePasswordPlugin
    {
        private static byte[] Get411Password(string password, byte[] seedBytes)
        {
            if (password.Length == 0)
            {
                return new byte[1];
            }
            SHA1 sha = SHA1.Create();
            byte[] array = null;
            array = sha.ComputeHash(Encoding.Default.GetBytes(password));
            byte[] array2 = sha.ComputeHash(array);
            byte[] array3 = new byte[seedBytes.Length + array2.Length];
            Array.Copy(seedBytes, 0, array3, 0, seedBytes.Length);
            Array.Copy(array2, 0, array3, seedBytes.Length, array2.Length);
            byte[] array4 = sha.ComputeHash(array3);
            byte[] array5 = new byte[array4.Length + 1];
            array5[0] = 20;
            Array.Copy(array4, 0, array5, 1, array4.Length);
            for (int i = 1; i < array5.Length; i++)
            {
                array5[i] ^= array[i - 1];
            }
            Array.Copy(array5, 1, array4, 0, 20);
            return array4;
        }
        /// <summary>
        /// Mysql密码加密
        /// </summary>
        /// <param name="password">Mysql密码明文</param>
        /// <param name="salt">20位密钥</param>
        /// <returns>加密后的20位hash</returns>
        public static string GetPassword(string password, string salt)
        {
            password = password == null || password.Length == 0 ? "root" : password;
            byte[] buffer = Get411Password(password, Encoding.Default.GetBytes(salt));
            password = "";
            foreach (var item in buffer)
            {
                string _titem = Convert.ToString(item, 16);
                _titem = _titem.Length == 1 ? "0" + _titem : _titem;
                password += _titem;
            }
            return password;
        }
        private MySqlNativePasswordPlugin() {

        }
    }
}
