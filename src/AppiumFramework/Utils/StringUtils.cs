using System.Text.RegularExpressions;

namespace AppiumFramework.Utils
{
    public static class StringUtils
    {
        public static string ExtractCityName(string input)
        {
            string pattern = @"^([А-Яа-яЁёs-]+)?";

            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return null;
        }
    }
}
