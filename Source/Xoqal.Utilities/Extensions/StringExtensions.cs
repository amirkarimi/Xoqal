namespace Xoqal.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the specified text from end of the string if it exists.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveIfEndsWith(this string source, string text)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(text))
            {
                return source;
            }

            if (!source.EndsWith(text))
            {
                return source;
            }

            return source.Remove(source.Length - text.Length);
        }
    }
}
