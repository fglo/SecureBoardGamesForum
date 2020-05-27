using System.Text;

namespace BBDProject.Shared.Utils.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString().ToLower();
        }
    }
}
