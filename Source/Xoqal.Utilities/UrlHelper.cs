namespace Xoqal.Utilities
{
    using System.Text.RegularExpressions;

    /// <summary>
    ///     provide some helpers static method toward of url
    /// </summary>
    public static class UrlHelper
    {
        private static readonly Regex Feet = new Regex(@"([0-9]\s?)'([^'])", RegexOptions.Compiled);
        private static readonly Regex Inch1 = new Regex(@"([0-9]\s?)''", RegexOptions.Compiled);
        private static readonly Regex Inch2 = new Regex(@"([0-9]\s?)""", RegexOptions.Compiled);
        private static readonly Regex Num = new Regex(@"#([0-9]+)", RegexOptions.Compiled);
        private static readonly Regex Dollar = new Regex(@"[$]([0-9]+)", RegexOptions.Compiled);
        private static readonly Regex Percent = new Regex(@"([0-9]+)%", RegexOptions.Compiled);
        private static readonly Regex Sep = new Regex(@"[\s_/\\+:.]", RegexOptions.Compiled);

        // latin char(s)
        //private static readonly Regex Empty = new Regex(@"[^-A-Za-z0-9]", RegexOptions.Compiled);

        // included persian char(s)
        private static readonly Regex Empty = new Regex(@"[^-A-Za-z0-9\u0600-\u06FF]", RegexOptions.Compiled);
        private static readonly Regex Extra = new Regex(@"[-]+", RegexOptions.Compiled);

        /// <summary>
        ///     get a string, prepare(clean) it for using in url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks>persian characters included</remarks>
        public static string PrepareUrl(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim().ToLower();
                str = str.Replace("&", "and");

                str = Feet.Replace(str, "$1-ft-");
                str = Inch1.Replace(str, "$1-in-");
                str = Inch2.Replace(str, "$1-in-");
                str = Num.Replace(str, "num-$1");

                str = Dollar.Replace(str, "$1-dollar-");
                str = Percent.Replace(str, "$1-percent-");

                str = Sep.Replace(str, "-");

                str = Empty.Replace(str, string.Empty);
                str = Extra.Replace(str, "-");

                str = str.Trim('-');
            }

            return str;
        }
    }
}