using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace BBDProject.Shared.Utils.Extensions
{
    public static class FormFileExtensions
    {
        public static List<string> ReadAsList(this IFormFile file)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Add(reader.ReadLine());
            }
            return result;
        }

        public static byte[] ComputeHash(this IFormFile file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = file.OpenReadStream())
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        public static byte[] ConvertToBytes(this IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}