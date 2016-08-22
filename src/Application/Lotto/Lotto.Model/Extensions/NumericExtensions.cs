using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto.Model.Extensions
{
    /// <summary>
    /// </summary>
    public static class NumericExtensions
    {
        public static string ToOctalString(this int integer)
        {
            byte[] bytes = BitConverter.GetBytes(integer);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            int idx = bytes.Length - 1;

            // Create a StringBuilder having appropriate capacity.
            var base8 = new StringBuilder(((bytes.Length / 3) + 1) * 8);

            // Calculate how many bytes are extra when byte array is split
            // into three-byte (24-bit) chunks.
            int extra = bytes.Length % 3;

            // If no bytes are extra, use three bytes for first chunk.
            if (extra == 0)
            {
                extra = 3;
            }

            // Convert first chunk (24-bits) to integer value.
            int int24 = 0;
            for (; extra != 0; extra--)
            {
                int24 <<= 8;
                int24 += bytes[idx--];
            }

            // Convert 24-bit integer to octal without adding leading zeros.
            string octal = Convert.ToString(int24, 8);

            // Append first converted chunk to StringBuilder.
            base8.Append(octal);

            // Convert remaining 24-bit chunks, adding leading zeros.
            for (; idx >= 0; idx -= 3)
            {
                int24 = (bytes[idx] << 16) + (bytes[idx - 1] << 8) + bytes[idx - 2];
                if (int24 > 0)
                    base8.Append(Convert.ToString(int24, 8));
            }

            return base8.ToString();
        }

        public static int FromOctalString(this string octal)
        {
            var number = int.Parse(octal);
            var decnum = 0;
            var i = 0;
            while (number != 0)
            {
                var r = number % 10;
                decnum = decnum + (r * (int)Math.Pow(8, i++));
                number = number / 10;
            }
            return decnum;
        }

        public static string CalculateUniqueString(this IList<int> numbers, string separator, bool order = true)
        {
            if (numbers == null || !numbers.Any())
            {
                return string.Empty;
            }

            var computedCollection = order ? numbers.OrderBy(x => x).ToList() : numbers;

            return computedCollection.Select(x => x.ToOctalString())
                .Aggregate(string.Empty, (s, ele) => s + ele + separator, s => s.Remove(s.Length - 1));
        }

        public static string CalculateUniqueString(this IList<int> numbers, bool order = true)
        {
            return CalculateUniqueString(numbers, "9", order);
        }

        public static List<int> CalculateCombination(this string uniqueString, string splitString)
        {
            if (string.IsNullOrEmpty(uniqueString))
            {
                return new List<int>();
            }

            var octals = uniqueString.Split(new[] { splitString }, StringSplitOptions.RemoveEmptyEntries);
            return octals.Select(o => o.FromOctalString()).ToList();
        }

        public static List<int> CalculateCombination(this string uniqueString)
        {
            return CalculateCombination(uniqueString, "9");
        }
    }
}