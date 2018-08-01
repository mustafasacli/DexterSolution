namespace Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    public static class DynamicExtensions
    {
        public static T ConvertTo<T>(this ExpandoObject dyn) where T : class
        {
            if (dyn == null)
            {
                throw new ArgumentNullException(nameof(dyn));
            }

            T tt = Activator.CreateInstance<T>();

            IDictionary<string, object> dict = dyn;

            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite == true).ToArray();
            props = props.AsQueryable().Where(p => dict.ContainsKey(p.Name) == true).ToArray();

            foreach (var prp in props)
            {
                prp.SetValue(tt, dict[prp.Name]);
            }

            return tt;
        }

        public static T ConvertToV2<T>(this ExpandoObject dyn) where T : class
        {
            if (dyn == null)
            {
                throw new ArgumentNullException(nameof(dyn));
            }

            IDictionary<string, string> columns = typeof(T).GetColumnsOfTypeAsReverse();

            T tt = Activator.CreateInstance<T>();

            IDictionary<string, object> dict = dyn;

            PropertyInfo[] pInfos = typeof(T).GetValidPropertiesOfType();

            pInfos = pInfos.AsQueryable().Where(q => columns.Values.Contains(q.Name) == true).ToArray();

            foreach (var prp in pInfos)
            {
                prp.SetValue(tt, dict[prp.Name]);
            }

            return tt;
        }

        public static List<T> ConvertToList<T>(this List<dynamic> dynList) where T : class
        {
            if (dynList == null)
            {
                throw new ArgumentNullException(nameof(dynList));
            }

            List<T> list = new List<T>();

            IDictionary<string, string> columns = typeof(T).GetColumnsOfType();

            PropertyInfo[] pInfos = typeof(T).GetValidPropertiesOfType();
            pInfos = pInfos.AsQueryable().Where(q => columns.Keys.Contains(q.Name) == true).ToArray();

            IDictionary<string, object> dict;
            T tt;
            string col;

            foreach (var dyn in dynList)
            {
                dict = dyn;
                tt = Activator.CreateInstance<T>();

                foreach (var prp in pInfos)
                {
                    col = null;
                    col = columns[prp.Name];
                    prp.SetValue(tt, dict[col]);
                }
                list.Add(tt);
            }
            return list;
        }
    }
}