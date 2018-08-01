namespace Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;

    public static class GenericExtensions
    {
        public static bool IsMember<T>(this T t, params T[] tArr)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            bool r = false;

            foreach (var item in tArr)
            {
                r = t.Equals(item);

                if (r)
                    break;
            }

            return r;
        }

        //Not Completed
        public static bool IsDifferent<T>(this T t1, T t2)
        {
            if (true)
            {
            }

            return false;
        }

        public static PropertyInfo[] GetValidProperties<T>(this T t) where T : class
        {
            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true).ToArray();

            return props;
        }

        public static IDictionary<string, object> GetProperties<T>(this T t) where T : class
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();

            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true).ToArray();

            foreach (var prp in props)
            {
                dict.Add(prp.Name, prp.GetValue(t));
            }

            return dict;
        }

        public static IDictionary<string, string> GetColumnsReverse<T>(this T t) where T : class
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => p.PropertyType.IsSimpleTypeV2() == true).ToArray();

            foreach (var prp in props)
            {
                var attributes = prp.GetCustomAttributes(typeof(ColumnAttribute), true);
                attributes = attributes ?? new object[] { };
                if (attributes.Length > 0)
                {
                    ColumnAttribute ca = (ColumnAttribute)attributes[0];
                    string can = ca == null ? prp.Name :
                        string.IsNullOrWhiteSpace(ca.Name) ?
                        prp.Name : ca.Name;

                    dict.Add(can, prp.Name);
                }
                else
                {
                    dict.Add(prp.Name, prp.Name);
                }
            }

            return dict;
        }

        public static IDictionary<string, string> GetColumns<T>(this T t) where T : class
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true).ToArray();

            foreach (var prp in props)
            {
                var attributes = prp.GetCustomAttributes(typeof(ColumnAttribute), true);
                attributes = attributes ?? new object[] { };
                if (attributes.Length > 0)
                {
                    ColumnAttribute ca = (ColumnAttribute)attributes[0];
                    string can = ca == null ? prp.Name :
                        string.IsNullOrWhiteSpace(ca.Name) ?
                        prp.Name : ca.Name;

                    dict.Add(prp.Name, can);
                }
                else
                {
                    dict.Add(prp.Name, prp.Name);
                }
            }

            return dict;
        }

        public static string GetKey<T>(this T t, bool isFirstPropKey = false) where T : class
        {
            string key = string.Empty;

            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true).ToArray();
            var prps = props.AsQueryable().Where(p => (p.GetCustomAttributes(typeof(KeyAttribute), true) ?? new object[] { }).Length > 0).ToArray();
            prps = prps ?? new PropertyInfo[] { };
            if (prps.Length > 0)
            {
                key = prps[0].Name;
            }
            else
            {
                if (isFirstPropKey)
                    key = props[0].Name;
            }

            return key;
        }

        public static string GetTableName<T>(this T t) where T : class
        {
            string s = typeof(T).Name;

            TableAttribute tt = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tt != null)
            {
                s = string.IsNullOrWhiteSpace(tt.Name) ? s : tt.Name;
            }

            return s;
        }

        public static string GetSchemaName<T>(this T t) where T : class
        {
            string s = string.Empty;

            TableAttribute tt = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tt != null)
            {
                s = tt.Schema ?? string.Empty;
            }

            return s;
        }

        public static object GetPropValue<T>(this T t, string propName)
        {
            object val = null;

            var p = t.GetType().GetProperty(propName);
            val = p.GetValue(t, null);

            return val;
        }

        public static void SetPropValue<T>(this T t, string propName, object value)
        {
            var p = t.GetType().GetProperty(propName);
            p.SetValue(t, value);
        }
    }
}