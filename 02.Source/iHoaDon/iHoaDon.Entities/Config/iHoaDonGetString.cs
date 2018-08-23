using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace iHoaDon.Entities.Config
{

    public class iHoaDonGetString
    {
        private static readonly string[] VietnameseSigns = new string[]

                                                               {

                                                                   "aAeEoOuUiIdDyY",

                                                                   "áàạảãâấầậẩẫăắằặẳẵ",

                                                                   "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

                                                                   "éèẹẻẽêếềệểễ",

                                                                   "ÉÈẸẺẼÊẾỀỆỂỄ",

                                                                   "óòọỏõôốồộổỗơớờợởỡ",

                                                                   "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

                                                                   "úùụủũưứừựửữ",

                                                                   "ÚÙỤỦŨƯỨỪỰỬỮ",

                                                                   "íìịỉĩ",

                                                                   "ÍÌỊỈĨ",

                                                                   "đ",

                                                                   "Đ",

                                                                   "ýỳỵỷỹ",

                                                                   "ÝỲỴỶỸ"

                                                               };
        public static string GetString(string st)
        {
            var str = Regex.Replace(st, @"<(.|\n)*?>", string.Empty);
            str = RemoveSign4VietnameseString(str.ToLower()).Replace("/", "");
            const string pattern = "[^a-zA-Z_0-9]";
            var myRegex = new Regex(pattern);
            var temp = myRegex.Split(str);
            var result = "";
            for (var i = 0; i < temp.Length; i++)
            {
                result += temp[i].Trim();
                if (i < temp.Length - 1)
                    result += "-";
            }
            return result;
        }
        public static string RemoveSign4VietnameseString(string str)
        {
            for (var i = 1; i < VietnameseSigns.Length; i++)
                for (var j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            return str;
        }
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        public static string RemoveHtmlImageTag(string val)
        {
            return Regex.Replace(val, "<.*?>", string.Empty);
        }
        public static string SubString(string val, int length)
        {
            if (string.IsNullOrEmpty(RemoveHtmlImageTag(val.Trim()))) return string.Empty;
            var arrVal = RemoveHtmlImageTag(val.Trim()).Split(' ');
            if (arrVal.Length < length)
                return val;
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
                sb.AppendFormat("{0} ", arrVal[i]);
            sb.Append("..");
            return sb.ToString();
        }
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

    }
}

