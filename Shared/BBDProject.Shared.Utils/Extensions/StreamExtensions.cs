using System;
using System.IO;
using System.Text;

namespace BBDProject.Shared.Utils.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ConvertFromStreamToBytes(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return bytes;
        }

        public static string ConvertFromStreamToBase64String(this Stream stream)
        {
            return Convert.ToBase64String(stream.ConvertFromStreamToBytes());
        }

        public static byte[] ConvertFromBase64StringToStream(String myString)
        {
            byte[] byteData = Convert.FromBase64String(myString);
            return byteData;
        }


        public static Stream ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            string base64 = Convert.ToBase64String(bytes);
            return new MemoryStream(Encoding.UTF8.GetBytes(base64));
        }
    }
}
