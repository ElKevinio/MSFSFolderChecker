using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCheckerMSFS
{
    public static class stringcompare
    {
        public static double Compare(this string text1, string text2)
        {
            double length1 = text1.Length;
            double length2 = text2.Length;

            string common1 = commonChars(text1, text2);
            string common2 = commonChars(text2, text1);

            if (common1.Length != common2.Length)
                return 0;

            double common = common1.Length;

            int transpositions = calcTranspositions(common1, common2);

            double similarity =
                   (common / length1 + common / length2 +
                   (common - transpositions) / common)
                    / 3.0;

            return similarity;
        }


        private static string commonChars(string text1, string text2)
        {
            int length1 = text1.Length, length2 = text2.Length;

            int halflen = Math.Min(length1, length2) / 2;

            var common = "";

            for (int x = 0; x < length1; x++)
            {
                for (int y = Math.Max(0, x - halflen);
                     y < Math.Min(x + halflen, length2); y++)
                {
                    if (text1[x] == text2[y])
                    {
                        common += text1[x];
                        break;
                    }
                }
            }

            return common;
        }

        private static int calcTranspositions(string common1, string common2)
        {
            int transpositions = 0;

            for (int pos = 0; pos < Math.Min(common1.Length, common2.Length); pos++)
                if (common1[pos] != common2[pos])
                    transpositions++;

            return transpositions / 2;
        }
    }
}
