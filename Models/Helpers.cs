using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public static class Helpers
    {
        const string chars = "QWERRTUYIOPASDFGHKJLXZCVBNM1324567890";
        static Random rnd = new Random();

        public static int ShortURLLength { get; set; } = 10;

        public static string GetAlphaNumString(int length)
        {
            var strBuilder = new StringBuilder();
            for (int i = 0; i < length; i++) AppendChar(strBuilder);
            return strBuilder.ToString();
        }

        static void AppendChar(StringBuilder stringBuilder)
        {
            var ch = chars[rnd.Next(0, chars.Length)];
            if (rnd.Next(0, 100) % 2 == 0) ch = char.ToLower(ch);
            stringBuilder.Append(ch);
        }
    }
}
