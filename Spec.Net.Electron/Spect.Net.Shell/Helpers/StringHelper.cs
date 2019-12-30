﻿using System.Text.RegularExpressions;

namespace Spect.Net.Shell.Helpers
{
    /// <summary>
    /// This class implements helper functions for strings
    /// </summary>
    public static class StringHelper
    {
        public static (string Pre, string Access, string Post) SplitMenuText(this string text)
        {
            var pre = string.Empty;
            var access = string.Empty;
            var post = string.Empty;

            var m = Regex.Match(text, "^(.*?)?(?:&([^&]))(.*)?$");
            if (!m.Success)
            {
                return (text, string.Empty, string.Empty);
            }

            if (m.Groups.Count > 1)
            {
                pre = Unescape(m.Groups[1].Value);
            }

            if (m.Groups.Count > 2)
            {
                access = m.Groups[2].Value;
            }

            if (m.Groups.Count > 3)
            {
                post = Unescape(m.Groups[3].Value);
            }
            return (pre, access, post);

            string Unescape(string accessText) => accessText.Replace("&&", "&");
        }
    }
}