namespace Dexter.Extensions
{
    using System;

    public static class StringExtension
    {
        public static bool IsValid(this string s)
        {
            bool result = false;

            result = !string.IsNullOrWhiteSpace(s);

            return result;
        }

        public static int Len(this string s)
        {
            int len = -1;

            if (s != null)
            {
                len = s.Length;
            }

            return len;
        }

        public static string TrimAll(this string s)
        {
            string result = string.Empty;

            if (s != null)
            {
                result = s;
                result = result.Replace(" ", "");
            }

            return result;
        }

        public static int FirstIndexOf(this string str, char ch)
        {
            int _index = -1;

            try
            {
                if (str.IsNullOrEmpty())
                    return _index;

                if (ch.IsNull())
                    return _index;

                char[] chs = str.ToCharArray();

                for (int charCounter = 0; charCounter < chs.Length; charCounter++)
                {
                    if (string.Format("{0}", ch).Equals(string.Format("{0}", chs[charCounter])))
                    {
                        _index = charCounter;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return _index;
        }
    }
}