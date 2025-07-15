using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veritas.Service.Server.Encryption
{
    public class HashGenerator
    {
        public string CreateMD5(string input, bool lower = false)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return lower ? Convert.ToHexString(hashBytes).ToLower() : Convert.ToHexString(hashBytes);
        }
    }
}
