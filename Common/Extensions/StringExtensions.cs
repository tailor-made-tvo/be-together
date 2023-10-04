// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     StringExtensions.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-02-15   12:35
// 
// Last changed: 2014-02-15   12:37
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace TMS.Common.Extensions
{
    /// <summary>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Trims the clean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string TrimClean(this string value)
        {
            return value.Trim(new[] {'\r', '\n', '\t', ' '});
        }

        ///// <summary>
        /////     Cleans the XML.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns></returns>
        //public static string CleanXml(this string value)
        //{
        //    return AntiXss.XmlEncode(value.TrimClean());
        //    //return AntiXss.XmlEncode(value.Replace(new Dictionary<string, string>
        //    //                         {
        //    //                             {"ä", "%26auml;"},
        //    //                             {"ö", "%26ouml;"},
        //    //                             {"ü", "%26uuml;"},
        //    //                             {"Ä", "%26Auml;"},
        //    //                             {"Ö", "%26Ouml;"},
        //    //                             {"Ü", "%26Uuml;"},
        //    //                             {"ß", "%26szlig;"},
        //    //                             {"ó", "%26oacute;"},
        //    //                             {"é", "%26eacute;"},
        //    //                             {"§", "%26sect;"},
        //    //                             {"\"", "&quot;"},
        //    //                             {"&", "&amp;"},
        //    //                             {"<", "&lt;"},
        //    //                             {">", "&gt;"},
        //    //                             {"/", "&frasl;"},
        //    //                             {"\r", ""},
        //    //                             {"\n", ""},
        //    //                             {"\t", ""},
        //    //                         }));
        //}

        ///// <summary>
        /////     Secures the name of a file.
        ///// </summary>
        ///// <param name="fileName">Name of the file to secure.</param>
        ///// <returns></returns>
        //private static string SecureFileName(this string fileName)
        //{
        //    var invalidFileNameCharsreplacements = Path.GetInvalidFileNameChars().ToDictionary(f => f, f => '_');
        //    return
        //        AntiXss.GetSafeHtmlFragment(
        //            Path.GetFileName(fileName.TrimClean().Replace(invalidFileNameCharsreplacements)));
        //    //return AntiXss.GetSafeHtmlFragment(fileName.Replace(new Dictionary<string, string>
        //}

        ///// <summary>
        /////     Make the filename secure.
        /////     Allows only characters which are given by <seealso cref="blacklist" />.
        /////     <seealso cref="blacklist" /> must be a regular expression which allows to find not allowed characters.
        /////     Not allowed characters are changed to underline ("_") character.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="blacklist">The blacklist must be a regular expression. Eg. @"[^0-9a-zA-Z_\.aöüAÖÜß]".</param>
        ///// <returns></returns>
        //public static string SecureFileName(this string fileName, string blacklist)
        //{
        //    if (blacklist.IsNullOrWhiteSpace())
        //        return SecureFileName(fileName);

        //    var replacements = new Dictionary<string, object>
        //    {
        //        {blacklist, @"_"}
        //    };
        //    var invalidFileNameCharsreplacements = Path.GetInvalidPathChars().ToDictionary(f => f, f => '_');
        //    return
        //        AntiXss.GetSafeHtmlFragment(
        //            Path.GetFileName(
        //                fileName.TrimClean().Replace(invalidFileNameCharsreplacements).ReplaceRegEx(replacements,
        //                    RegexOptions.CultureInvariant)));
        //}

        /// <summary>
        ///     Replaces the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replacements">The replacements.</param>
        /// <returns></returns>
        public static string Replace(this string value,
            Dictionary<char, char> replacements)
        {
            var ret = new StringBuilder(value);

            foreach (var data in replacements)
            {
                ret.Replace(data.Key,
                    data.Value);
            }

            return ret.ToString();
        }

        /// <summary>
        ///     Replaces the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replacements">The replacements.</param>
        /// <param name="useBraces">if set to <c>true</c> braces are used for key comparison.</param>
        /// <returns></returns>
        public static string Replace(this string value, Dictionary<string, string> replacements, bool useBraces = false)
        {
            var ret = new StringBuilder(value);

            foreach (var data in replacements)
            {
                ret.Replace(useBraces
                    ? string.Format("{0}{1}{2}",
                        "{",
                        data.Key,
                        "}")
                    : data.Key,
                    data.Value ?? string.Empty);
            }

            return ret.ToString();
        }

        /// <summary>
        ///     Replaces the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replacements">The replacements.</param>
        /// <param name="useBraces">if set to <c>true</c> [use braces].</param>
        /// <returns></returns>
        public static string Replace(this string value, Dictionary<string, object> replacements, bool useBraces = false)
        {
            var ret = new StringBuilder(value);

            foreach (var data in replacements)
            {
                ret.Replace(useBraces
                    ? string.Format("{0}{1}{2}",
                        "{",
                        data.Key,
                        "}")
                    : data.Key,
                    string.Format("{0}", data.Value));
            }

            return ret.ToString();
        }

        /// <summary>
        ///     Replaces the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replacements">The replacements.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="useBraces">if set to <c>true</c> braces are used for key comparison.</param>
        /// <returns></returns>
        public static string Replace(this string value, Dictionary<string, string> replacements,
            StringComparison comparisonType, bool useBraces = false)
        {
            return replacements.Aggregate(value,
                (current, data) => current.Replace(useBraces
                    ? string.Format("{0}{1}{2}",
                        "{",
                        data.Key,
                        "}")
                    : data.Key,
                    data.Value ?? string.Empty,
                    comparisonType));
        }

        public static string Replace(this string value, Dictionary<string, object> replacements,
            StringComparison comparisonType, bool useBraces = false)
        {
            return replacements.Aggregate(value,
                (current, data) => current.Replace(useBraces
                    ? string.Format("{0}{1}{2}",
                        "{",
                        data.Key,
                        "}")
                    : data.Key,
                    string.Format("{0}",
                        data.Value),
                    comparisonType));
        }

        ///// <summary>
        ///// Replaces the specified value.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <param name="replacements">The replacements.</param>
        ///// <param name="comparisonType">Type of the comparison.</param>
        ///// <param name="useBraces">if set to <c>true</c> braces are used for key comparison.</param>
        ///// <returns></returns>
        //public static string Replace( this string value, SortedDictionary<string, string> replacements, StringComparison comparisonType, bool useBraces = false )
        //{
        //    return replacements.Aggregate( value,
        //                                   ( current, data ) => current.Replace( useBraces
        //                                       ? string.Format( "{0}{1}{2}",
        //                                                        "{",
        //                                                        data.Key,
        //                                                        "}" )
        //                                       : data.Key,
        //                                                                         data.Value ?? string.Empty,
        //                                                                         comparisonType ) );
        //}

        //public static string Replace( this string value, SortedDictionary<string, object> replacements, StringComparison comparisonType, bool useBraces = false )
        //{
        //    return replacements.Aggregate( value,
        //                                   ( current, data ) => current.Replace( useBraces
        //                                       ? string.Format( "{0}{1}{2}",
        //                                                        "{",
        //                                                        data.Key,
        //                                                        "}" )
        //                                       : data.Key,
        //                                                                         string.Format( "{0}",
        //                                                                                        data.Value ),
        //                                                                         comparisonType ) );
        //}

        /// <summary>
        ///     Replaces the specified value.
        /// </summary>
        /// <param name="value">The string in which a value should be replaced.</param>
        /// <param name="oldValue">The value which should be replaced.</param>
        /// <param name="newValue">The value to replace with.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns></returns>
        public static string Replace(this string value,
            string oldValue,
            string newValue,
            StringComparison comparisonType)
        {
            var startIndex = 0;
            var pos = value.IndexOf(oldValue,
                startIndex,
                comparisonType);

            if (pos == -1)
                return value;

            var ret = new StringBuilder();
            while (pos > -1)
            {
                ret.AppendFormat("{0}{1}",
                    value.Substring(startIndex,
                        pos - startIndex),
                    newValue);

                startIndex = pos + oldValue.Length;
                pos = value.IndexOf(oldValue,
                    startIndex,
                    comparisonType);

                if (pos == -1 && value.Length > startIndex)
                    ret.Append(value.Substring(startIndex));
            }

            return ret.ToString();
        }

        /// <summary>
        ///     Trims a string to single line without tabs, line feeds and carriage returns.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns></returns>
        public static string TrimToSingleLine(this string value)
        {
            return value.Trim(new[] {' ', '\t', '\r', '\n'});
        }

        /// <summary>
        ///     Converts the first character to upper and all others to lower case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToUpperFirst(this string value)
        {
            return string.Format("{0}{1}",
                value.Length == 0
                    ? string.Empty
                    : value.Substring(0,
                        1).ToUpper(),
                value.Length <= 1 ? string.Empty : value.Substring(1).ToLower());
        }

        /// <summary>
        ///     Converts from Url encoding to Ascii encoding.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Url2AsciiEncoding(this string value)
        {
            return
                value.Replace(
                    new Dictionary<string, string>
                    {
                        {"%20", " "},
                        {"%21", "!"},
                        {"%22", "\""},
                        {"%23", "#"},
                        {"%24", "$"},
                        //{"%25", "%"},
                        {"%26", "&"},
                        {"%27", "'"},
                        {"%28", "("},
                        {"%29", ")"},
                        {"%2A", "*"},
                        {"%2B", "+"},
                        {"%2C", ","},
                        {"%2D", "-"},
                        {"%2E", "."},
                        {"%2F", "/"},
                        //{"%30", "0"},
                        //{"%31", "1"},
                        //{"%32", "2"},
                        //{"%33", "3"},
                        //{"%34", "4"},
                        //{"%35", "5"},
                        //{"%36", "6"},
                        //{"%37", "7"},
                        //{"%38", "8"},
                        //{"%39", "9"},
                        {"%3A", ":"},
                        {"%3B", ";"},
                        {"%3C", "<"},
                        {"%3D", "="},
                        {"%3E", ">"},
                        {"%3F", "?"},
                        {"%40", "@"},
                        //{"%41", "A"},
                        //{"%42", "B"},
                        //{"%43", "C"},
                        //{"%44", "D"},
                        //{"%45", "E"},
                        //{"%46", "F"},
                        //{"%47", "G"},
                        //{"%48", "H"},
                        //{"%49", "I"},
                        //{"%4A", "J"},
                        //{"%4B", "K"},
                        //{"%4C", "L"},
                        //{"%4D", "M"},
                        //{"%4E", "N"},
                        //{"%4F", "O"},
                        //{"%50", "P"},
                        //{"%51", "Q"},
                        //{"%52", "R"},
                        //{"%53", "S"},
                        //{"%54", "T"},
                        //{"%55", "U"},
                        //{"%56", "V"},
                        //{"%57", "W"},
                        //{"%58", "X"},
                        //{"%59", "Y"},
                        //{"%5A", "Z"},
                        {"%5B", "["},
                        {"%5C", "\\"},
                        {"%5D", "]"},
                        {"%5E", "^"},
                        {"%5F", "_"},
                        {"%60", "`"},
                        //{"%61", "a"},
                        //{"%62", "b"},
                        //{"%63", "c"},
                        //{"%64", "d"},
                        //{"%65", "e"},
                        //{"%66", "f"},
                        //{"%67", "g"},
                        //{"%68", "h"},
                        //{"%69", "i"},
                        //{"%6A", "j"},
                        //{"%6B", "k"},
                        //{"%6C", "l"},
                        //{"%6D", "m"},
                        //{"%6E", "n"},
                        //{"%6F", "o"},
                        //{"%70", "p"},
                        //{"%71", "q"},
                        //{"%72", "r"},
                        //{"%73", "s"},
                        //{"%74", "t"},
                        //{"%75", "u"},
                        //{"%76", "v"},
                        //{"%77", "w"},
                        //{"%78", "x"},
                        //{"%79", "y"},
                        //{"%7A", "z"},
                        {"%7C", "|"},
                        {"%7E", "~"},
                        {"%25", "%"},
                        {"%7B", "{"},
                        {"%7D", "}"},
                    },
                    StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        ///     Url2s the ASCII encoding2.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Url2AsciiEncoding2(this string value)
        {
            return value.Replace(new Dictionary<string, string>
            {
                {"%20", " "},
                {"%21", "!"},
                //////{"%22", "\""},
                {"%23", "#"},
                {"%24", "$"},
                //{"%25", "%"},
                {"%26", "&"},
                {"%27", "'"},
                {"%28", "("},
                {"%29", ")"},
                {"%2A", "*"},
                {"%2B", "+"},
                {"%2C", ","},
                {"%2D", "-"},
                {"%2E", "."},
                {"%2F", "/"},
                //{"%30", "0"},
                //{"%31", "1"},
                //{"%32", "2"},
                //{"%33", "3"},
                //{"%34", "4"},
                //{"%35", "5"},
                //{"%36", "6"},
                //{"%37", "7"},
                //{"%38", "8"},
                //{"%39", "9"},
                {"%3A", ":"},
                {"%3B", ";"},
                //////{"%3C", "<"},
                {"%3D", "="},
                //////{"%3E", ">"},
                {"%3F", "?"},
                {"%40", "@"},
                //{"%41", "A"},
                //{"%42", "B"},
                //{"%43", "C"},
                //{"%44", "D"},
                //{"%45", "E"},
                //{"%46", "F"},
                //{"%47", "G"},
                //{"%48", "H"},
                //{"%49", "I"},
                //{"%4A", "J"},
                //{"%4B", "K"},
                //{"%4C", "L"},
                //{"%4D", "M"},
                //{"%4E", "N"},
                //{"%4F", "O"},
                //{"%50", "P"},
                //{"%51", "Q"},
                //{"%52", "R"},
                //{"%53", "S"},
                //{"%54", "T"},
                //{"%55", "U"},
                //{"%56", "V"},
                //{"%57", "W"},
                //{"%58", "X"},
                //{"%59", "Y"},
                //{"%5A", "Z"},
                {"%5B", "["},
                {"%5C", "\\"},
                {"%5D", "]"},
                {"%5E", "^"},
                {"%5F", "_"},
                {"%60", "`"},
                //{"%61", "a"},
                //{"%62", "b"},
                //{"%63", "c"},
                //{"%64", "d"},
                //{"%65", "e"},
                //{"%66", "f"},
                //{"%67", "g"},
                //{"%68", "h"},
                //{"%69", "i"},
                //{"%6A", "j"},
                //{"%6B", "k"},
                //{"%6C", "l"},
                //{"%6D", "m"},
                //{"%6E", "n"},
                //{"%6F", "o"},
                //{"%70", "p"},
                //{"%71", "q"},
                //{"%72", "r"},
                //{"%73", "s"},
                //{"%74", "t"},
                //{"%75", "u"},
                //{"%76", "v"},
                //{"%77", "w"},
                //{"%78", "x"},
                //{"%79", "y"},
                //{"%7A", "z"},
                //////{"%7C", "|"},
                {"%7E", "~"},
                {"%25", "%"},
                {"%7B", "{"},
                {"%7D", "}"},
            },
                StringComparison.CurrentCultureIgnoreCase);
        }

        ///// <summary>
        /////     Encode a string to Url usable format by using <seealso cref="AntiXss" />.
        ///// </summary>
        ///// <param name="value">The value to encode.</param>
        ///// <returns></returns>
        //public static string UrlEncode(this string value)
        //{
        //    return AntiXss.UrlEncode(value);
        //}

        /// <summary>
        ///     Converts from Ascii encoding to URL encoding.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Ascii2UrlEncoding(this string value)
        {
            return
                value.Replace(
                    new Dictionary<string, string>
                    {
                        {"%", "%25"},
                        {" ", "%20"},
                        {"!", "%21"},
                        {"\"", "%22"},
                        {"#", "%23"},
                        {"$", "%24"},
                        {"&", "%26"},
                        {"'", "%27"},
                        {"(", "%28"},
                        {")", "%29"},
                        {"*", "%2A"},
                        {"+", "%2B"},
                        {",", "%2C"},
                        {"-", "%2D"},
                        {".", "%2E"},
                        {"/", "%2F"},
                        //{"0", "%30"},
                        //{"1", "%31"},
                        //{"2", "%32"},
                        //{"3", "%33"},
                        //{"4", "%34"},
                        //{"5", "%35"},
                        //{"6", "%36"},
                        //{"7", "%37"},
                        //{"8", "%38"},
                        //{"9", "%39"},
                        {":", "%3A"},
                        {";", "%3B"},
                        {"<", "%3C"},
                        {"=", "%3D"},
                        {">", "%3E"},
                        {"?", "%3F"},
                        {"@", "%40"},
                        //{"A", "%41"},
                        //{"B", "%42"},
                        //{"C", "%43"},
                        //{"D", "%44"},
                        //{"E", "%45"},
                        //{"F", "%46"},
                        //{"G", "%47"},
                        //{"H", "%48"},
                        //{"I", "%49"},
                        //{"J", "%4A"},
                        //{"K", "%4B"},
                        //{"L", "%4C"},
                        //{"M", "%4D"},
                        //{"N", "%4E"},
                        //{"O", "%4F"},
                        //{"P", "%50"},
                        //{"Q", "%51"},
                        //{"R", "%52"},
                        //{"S", "%53"},
                        //{"T", "%54"},
                        //{"U", "%55"},
                        //{"V", "%56"},
                        //{"W", "%57"},
                        //{"X", "%58"},
                        //{"Y", "%59"},
                        //{"Z", "%5A"},
                        {"[", "%5B"},
                        {"\\", "%5C"},
                        {"]", "%5D"},
                        {"^", "%5E"},
                        {"_", "%5F"},
                        {"`", "%60"},
                        //{"a", "%61"},
                        //{"b", "%62"},
                        //{"c", "%63"},
                        //{"d", "%64"},
                        //{"e", "%65"},
                        //{"f", "%66"},
                        //{"g", "%67"},
                        //{"h", "%68"},
                        //{"i", "%69"},
                        //{"j", "%6A"},
                        //{"k", "%6B"},
                        //{"l", "%6C"},
                        //{"m", "%6D"},
                        //{"n", "%6E"},
                        //{"o", "%6F"},
                        //{"p", "%70"},
                        //{"q", "%71"},
                        //{"r", "%72"},
                        //{"s", "%73"},
                        //{"t", "%74"},
                        //{"u", "%75"},
                        //{"v", "%76"},
                        //{"w", "%77"},
                        //{"x", "%78"},
                        //{"y", "%79"},
                        //{"z", "%7A"},
                        {"{", "%7B"},
                        {"|", "%7C"},
                        {"}", "%7D"},
                        {"~", "%7E"},
                        //{"", "%7F"},
                        //{"€", "%80"},
                        ////{"", "%81"},
                        //{"‚", "%82"},
                        //{"ƒ", "%83"},
                        //{"„", "%84"},
                        //{"…", "%85"},
                        //{"†", "%86"},
                        //{"‡", "%87"},
                        //{"ˆ", "%88"},
                        //{"‰", "%89"},
                        //{"Š", "%8A"},
                        //{"‹", "%8B"},
                        //{"Œ", "%8C"},
                        ////{"", "%8D"},
                        //{"Ž", "%8E"},
                        ////{"", "%8F"},
                        ////{"", "%90"},
                        //{"‘", "%91"},
                        //{"’", "%92"},
                        //{"“", "%93"},
                        //{"”", "%94"},
                        //{"•", "%95"},
                        //{"–", "%96"},
                        //{"—", "%97"},
                        //{"˜", "%98"},
                        //{"™", "%99"},
                        //{"š", "%9A"},
                        //{"›", "%9B"},
                        //{"œ", "%9C"},
                        ////{"", "%9D"},
                        //{"ž", "%9E"},
                        //{"Ÿ", "%9F"},
                        ////{"", "%A0"},
                        //{"¡", "%A1"},
                        //{"¢", "%A2"},
                        //{"£", "%A3"},
                        ////{"", "%A4"},
                        //{"¥", "%A5"},
                        ////{"|", "%A6"},
                        //{"§", "%A7"},
                        //{"¨", "%A8"},
                        //{"©", "%A9"},
                        //{"ª", "%AA"},
                        //{"«", "%AB"},
                        //{"¬", "%AC"},
                        //{"¯", "%AD"},
                        //{"®", "%AE"},
                        ////{"¯", "%AF"},
                        //{"°", "%B0"},
                        //{"±", "%B1"},
                        //{"²", "%B2"},
                        //{"³", "%B3"},
                        //{"´", "%B4"},
                        //{"µ", "%B5"},
                        //{"¶", "%B6"},
                        //{"·", "%B7"},
                        //{"¸", "%B8"},
                        //{"¹", "%B9"},
                        //{"º", "%BA"},
                        //{"»", "%BB"},
                        //{"¼", "%BC"},
                        //{"½", "%BD"},
                        //{"¾", "%BE"},
                        //{"¿", "%BF"},
                        //{"À", "%C0"},
                        //{"Á", "%C1"},
                        //{"Â", "%C2"},
                        //{"Ã", "%C3"},
                        //{"Ä", "%C4"},
                        //{"Å", "%C5"},
                        //{"Æ", "%C6"},
                        //{"Ç", "%C7"},
                        //{"È", "%C8"},
                        //{"É", "%C9"},
                        //{"Ê", "%CA"},
                        //{"Ë", "%CB"},
                        //{"Ì", "%CC"},
                        //{"Í", "%CD"},
                        //{"Î", "%CE"},
                        //{"Ï", "%CF"},
                        //{"Ð", "%D0"},
                        //{"Ñ", "%D1"},
                        //{"Ò", "%D2"},
                        //{"Ó", "%D3"},
                        //{"Ô", "%D4"},
                        //{"Õ", "%D5"},
                        //{"Ö", "%D6"},
                        ////{"", "%D7"},
                        //{"Ø", "%D8"},
                        //{"Ù", "%D9"},
                        //{"Ú", "%DA"},
                        //{"Û", "%DB"},
                        //{"Ü", "%DC"},
                        //{"Ý", "%DD"},
                        //{"Þ", "%DE"},
                        //{"ß", "%DF"},
                        //{"à", "%E0"},
                        //{"á", "%E1"},
                        //{"â", "%E2"},
                        //{"ã", "%E3"},
                        //{"ä", "%E4"},
                        //{"å", "%E5"},
                        //{"æ", "%E6"},
                        //{"ç", "%E7"},
                        //{"è", "%E8"},
                        //{"é", "%E9"},
                        //{"ê", "%EA"},
                        //{"ë", "%EB"},
                        //{"ì", "%EC"},
                        //{"í", "%ED"},
                        //{"î", "%EE"},
                        //{"ï", "%EF"},
                        //{"ð", "%F0"},
                        //{"ñ", "%F1"},
                        //{"ò", "%F2"},
                        //{"ó", "%F3"},
                        //{"ô", "%F4"},
                        //{"õ", "%F5"},
                        //{"ö", "%F6"},
                        //{"÷", "%F7"},
                        //{"ø", "%F8"},
                        //{"ù", "%F9"},
                        //{"ú", "%FA"},
                        //{"û", "%FB"},
                        //{"ü", "%FC"},
                        //{"ý", "%FD"},
                        //{"þ", "%FE"},
                        //{"ÿ", "%FF"},
                    },
                    StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        ///     Ascii2s the URL encoding simple.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Ascii2XmlEncoding(this string value)
        {
            return
                value.Replace(
                    new Dictionary<string, string>
                    {
                        {"<", "&lt;"},
                        {">", "&gt;"},
                    },
                    StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        ///     Replaces the reg ex.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replacements">The replacements.</param>
        /// <param name="options">The <seealso cref="RegexOptions" />.</param>
        /// <returns></returns>
        public static string ReplaceRegEx(this string value,
            IEnumerable<KeyValuePair<string, object>> replacements,
            RegexOptions options)
        {
            return replacements.Aggregate(value,
                (current, replacement) =>
                    Regex.Replace(current, replacement.Key, string.Format("{0}", replacement.Value), options));
        }

        ///// <summary>
        ///// Cleans a string from all cr, lf and tab and deletes all spaces between '&gt;' and '&lt;' for preparing XML.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns></returns>
        //public static string CleanXml(this string value)
        //{
        //    var ret = Regex.Replace(value, "[\r\n\t]+", "");

        //    ret = Regex.Replace(ret, "(>)[ ]+(<)", "><");

        //    return ret.Trim();
        //}

        /// <summary>
        ///     Toes the XML key string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToXsltArgumentKey(this string value)
        {
            return Regex.Replace(value,
                "[\r\n\t ()&;:,.]+",
                "");
        }

        /// <summary>
        ///     Determines whether string is Null or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <c>true</c> if string is Null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Determines whether [is null or empty value] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <c>true</c> if [is null or empty value] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmptyValue(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            UInt64 v;
            if (UInt64.TryParse(value,
                out v))
                return v == 0;

            return true;
        }

        /// <summary>
        ///     Determines whether string is Null or white space.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <c>true</c> if string is Null or white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <c>true</c> if the specified value is numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumeric(this string value)
        {
            Int64 num;
            return Int64.TryParse(value,
                out num);
        }

        /// <summary>
        ///     Returns a string with maximim length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GetLeft(this string value,
            int length)
        {
            if (value.IsNullOrEmpty() || length >= value.Length)
                return value;

            return value.Substring(0,
                length);
        }

        /// <summary>
        ///     Cuts the left.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GetRight(this string value,
            int length)
        {
            if (value.IsNullOrEmpty() || length >= value.Length)
                return value;

            return value.Substring(value.Length - length);
        }

        /// <summary>
        ///     Compares a string to a list of strings.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="stringList">The string list.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns>true if any string is in list, otherwise false.</returns>
        public static bool Equals(this string value,
            List<string> stringList,
            StringComparison comparisonType)
        {
            if (value == null || stringList == null || !stringList.Any())
                return false;

            return stringList.Any(s => string.Equals(value,
                s,
                comparisonType));
        }

        /// <summary>
        ///     Determines whether the specified value is matching by regular expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="matchStringRegex">The match string regex.</param>
        /// <param name="regexOptions">The regex options.</param>
        /// <returns>
        ///     <c>true</c> if the specified value is match; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMatch(this string value,
            string matchStringRegex,
            RegexOptions regexOptions)
        {
            if (value.IsNullOrEmpty() || matchStringRegex.IsNullOrEmpty())
                return false;

            // Use internally cached regular expressions to reduce overhead
            return Regex.IsMatch(value, matchStringRegex, regexOptions);
        }

        /// <summary>
        ///     Gives a ToInt-Functionality.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            var idString = value.Trim();
            if (idString.StartsWith("0x"))
            {
                idString = idString.Substring(2);
                return int.Parse(idString,
                    NumberStyles.AllowHexSpecifier,
                    CultureInfo.InvariantCulture);
            }

            return int.Parse(idString,
                NumberStyles.Any,
                CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Translates the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="data">The data.</param>
        /// <param name="useBraces">if set to <c>true</c> [use braces].</param>
        /// <param name="xmlEncode">if set to <c>true</c> [URL encode].</param>
        /// <returns></returns>
        public static string Translate(this string text, Dictionary<string, object> data = null, bool useBraces = false,
            bool xmlEncode = false)
        {
            if (text.IsNullOrEmpty() || data == null)
                return text;

            var ret = text.Replace(data, StringComparison.OrdinalIgnoreCase, useBraces);
            return xmlEncode ? ret.Ascii2XmlEncoding() : ret;
        }

        /////// <summary>
        /////// Zips the string.
        /////// </summary>
        /////// <param name="value">The value.</param>
        /////// <returns></returns>
        ////public static string ZipString( this string value )
        ////{
        ////    using ( var stream = new MemoryStream() )
        ////    {
        ////        using ( var gZipStream = new GZipStream( stream,
        ////                                                 CompressionMode.Compress,
        ////                                                 true ) )
        ////        {
        ////            gZipStream.Write( value );
        ////        }
        ////        stream.Position = 0;

        ////        return stream.ReadToEnd(false);
        ////    }
        ////}

        /////// <summary>
        /////// Unzips the string.
        /////// </summary>
        /////// <param name="value">The value.</param>
        /////// <returns></returns>
        ////public static string UnzipString(this string value)
        ////{
        ////    using ( var stream = new MemoryStream() )
        ////    {
        ////        using ( var gZipStream = new GZipStream( stream,
        ////                                                 CompressionMode.Decompress,
        ////                                                 true ) )
        ////        {
        ////            gZipStream.Write( value );
        ////        }
        ////        stream.Position = 0;

        ////        return ( stream.ReadToEnd( true ) );
        ////    }
        ////    return string.Empty;
        ////}
    }
}