namespace Dexter.Extensions
{
    using System;

    public static class ObjectExtension
    {
        public static bool IsNull(this object o)
        {
            return o == null;
        }

        public static bool IsNullOrDbNull(this object obj)
        {
            return (null == obj || obj == DBNull.Value);
        }

        public static string ToStr(this object obj)
        {
            string str = string.Empty;

            try
            {
                str = string.Format("{0}", obj);
            }
            catch (Exception e)
            {
            }

            return str;
        }

        public static int ToInt(this object obj)
        {
            int result = 0;

            try
            {
                result = Convert.ToInt32(obj);
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public static decimal ToDecimal(this object obj)
        {
            decimal result = 0;

            try
            {
                result = Convert.ToDecimal(obj);
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public static int Char2Int(this char ch)
        {
            int i = 0;

            try
            {
                i = Convert.ToInt32(ch);
            }
            catch (Exception e)
            {
            }

            return i;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Length == 0;
            }
        }

        public static bool IsNullOrSpace(this string str)
        {
            if (str != null)
            {
                return str.Replace(" ", "").Length == 0;
            }

            return true;
        }
    }
}
