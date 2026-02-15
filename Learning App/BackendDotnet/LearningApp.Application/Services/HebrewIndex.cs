using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningApp.Application.Services
{
    public static class HebrewIndex
    {
        private static readonly Dictionary<char, int> HebrewValues = new Dictionary<char, int>()
        {
            ['א'] = 1,
            ['ב'] = 2,
            ['ג'] = 3,
            ['ד'] = 4,
            ['ה'] = 5,
            ['ו'] = 6,
            ['ז'] = 7,
            ['ח'] = 8,
            ['ט'] = 9,
            ['י'] = 10,
            ['כ'] = 20,
            ['ל'] = 30,
            ['מ'] = 40,
            ['נ'] = 50,
            ['ס'] = 60,
            ['ע'] = 70,
            ['פ'] = 80,
            ['צ'] = 90,
            ['ק'] = 100,
            ['ר'] = 200,
            ['ש'] = 300,
            ['ת'] = 400
        };

        // עברית → מספר
        public static bool TryParse(string input, out int index)
        {
            index = 0;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();

            foreach (char ch in input)
            {
                if (!HebrewValues.TryGetValue(ch, out int value))
                    return false;

                index += value;
            }

            return index > 0;
        }

        // מספר → עברית (כולל טו / טז)
        public static string FromIndex(int index)
        {
            if (index <= 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            var sb = new StringBuilder();

            foreach (var kvp in HebrewValues.OrderByDescending(x => x.Value))
            {
                // טיפול מיוחד ב־15 ו־16
                if (index == 15)
                {
                    sb.Append("טו");
                    index = 0;
                    break;
                }

                if (index == 16)
                {
                    sb.Append("טז");
                    index = 0;
                    break;
                }

                while (index >= kvp.Value)
                {
                    sb.Append(kvp.Key);
                    index -= kvp.Value;
                }
            }

            return sb.ToString();
        }

        public static bool TryParseNumericRange(string from, string to, out IEnumerable<int> range)
        {
            range = null;

            if (!int.TryParse(from, out int start) ||
                !int.TryParse(to, out int end) ||
                end < start)
                return false;

            range = Enumerable.Range(start, end - start + 1);
            return true;
        }

        public static bool TryParseHebrewRange(string from, string to, out IEnumerable<string> range)
        {
            range = null;

            if (!TryParse(from, out int start) ||
                !TryParse(to, out int end) ||
                end < start)
                return false;

            range = Enumerable
                .Range(start, end - start + 1)
                .Select(FromIndex);

            return true;
        }
    }
}
