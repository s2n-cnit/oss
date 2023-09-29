
using System.ComponentModel;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var chars = str.ToCharArray();
            chars[0] = chars[0].ToString().ToUpper().First();

            return new string(chars);
        }

        public static string EncodeBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        public static string DecodeBase64(this string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }

        public static bool IsAnyOf(this string value, params string[] values)
        {
            foreach (var v in values)
            {
                if (value == v)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsNoneOf(this string value, params string[] values)
        {
            return !value.IsAnyOf(values);
        }

        public static T As<T>(this string str)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));

                if (converter.CanConvertFrom(typeof(string)))
                {
                    return (T)converter.ConvertFrom(str);
                }

                converter = TypeDescriptor.GetConverter(typeof(string));

                if (converter.CanConvertTo(typeof(T)))
                {
                    return (T)converter.ConvertTo(str, typeof(T));
                }
            }
            catch
            {
                // Eat all exceptions and return the defaultValue, assumption is that its always a parse/format exception
            }

            return default(T);
        }
    }
}
