using System;
using System.ComponentModel;

namespace BBDProject.Shared.Utils.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            if (enumValue == null)
            {
                return string.Empty;
            }
            var attribute = Attribute.GetCustomAttribute(enumValue.GetType().GetField(enumValue.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? enumValue.ToString() : attribute.Description;
        }

        public static int ToInt(this Enum enumValue)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException(nameof(enumValue));
            }
            object enumObject = enumValue;
            return (int)enumObject;
        }
    }
}
