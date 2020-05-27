using System.Globalization;
using System.Text;

namespace BBDProject.Shared.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveDiactritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string SoftNormalize(this string text)
        {
            return text.RemoveDiactritics().ToUpper();
        }
    }
}
