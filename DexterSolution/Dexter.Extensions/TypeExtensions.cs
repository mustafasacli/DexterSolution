namespace Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    public static class TypeExtensions
    {
        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc)
        /// or complex (i.e. custom class with public properties and methods).
        /// source code: https://gist.github.com/jonathanconway/3330614
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[] {
                typeof(String),
                typeof(Decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        public static bool IsSimpleTypeV2(this Type type)
        {
            return
                type.IsPrimitive ||
                new Type[] {
            typeof(byte[]),
            typeof(Enum),
            typeof(String),
            typeof(Decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(XmlDocument),
            typeof(XmlNode),
            typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleTypeV2(type.GetGenericArguments()[0]))
                ;
        }

        public static DbType ToDbType(this Type myType)
        {
            Type t = Nullable.GetUnderlyingType(myType) ?? myType;
            DbType dbt = DbType.Object;

            if (t.IsEnum)
            {
                dbt = DbType.Int16;
                return dbt;
            }

            if (t == typeof(bool))
            {
                dbt = DbType.Boolean;
                return dbt;
            }

            if (t == typeof(byte[]))
            {
                dbt = DbType.Binary;
                return dbt;
            }

            if (t == typeof(byte))
            {
                dbt = DbType.Byte;
                return dbt;
            }

            if (t == typeof(sbyte))
            {
                dbt = DbType.SByte;
                return dbt;
            }

            if (t == typeof(DateTime))
            {
                dbt = DbType.DateTime;
                return dbt;
            }

            if (t == typeof(decimal))
            {
                dbt = DbType.Decimal;
                return dbt;
            }

            if (t == typeof(double))
            {
                dbt = DbType.Double;
                return dbt;
            }

            if (t == typeof(float))
            {
                dbt = DbType.Single;
                return dbt;
            }

            if (t == typeof(Guid))
            {
                dbt = DbType.Guid;
                return dbt;
            }

            if (t == typeof(Int16))
            {
                dbt = DbType.Int16;
                return dbt;
            }

            if (t == typeof(Int32))
            {
                dbt = DbType.Int32;
                return dbt;
            }

            if (t == typeof(Int64))
            {
                dbt = DbType.Int64;
                return dbt;
            }

            if (t == typeof(string))
            {
                dbt = DbType.String;
                return dbt;
            }

            if (t == typeof(UInt16))
            {
                dbt = DbType.UInt16;
                return dbt;
            }

            if (t == typeof(UInt32))
            {
                dbt = DbType.UInt32;
                return dbt;
            }

            if (t == typeof(UInt64))
            {
                dbt = DbType.UInt64;
                return dbt;
            }

            if (t == typeof(XmlDocument) || t == typeof(XmlNode))
            {
                dbt = DbType.Xml;
                return dbt;
            }

            return dbt;
        }

        public static IDictionary<string, string> GetColumnsOfTypeAsReverse(this Type t)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            var props = t.GetProperties();

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
                    dict.Add(ca.Name, prp.Name);
                }
                else
                {
                    dict.Add(prp.Name, prp.Name);
                }
            }

            return dict;
        }

        public static string GetKeyOfType(this Type t, bool isFirstPropKey = false)
        {
            string key = string.Empty;

            var props = t.GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => p.PropertyType.IsSimpleTypeV2() == true).ToArray();
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

        public static string GetKeyColumnOfType(this Type t, bool isFirstPropKey = false)
        {
            string idCol = string.Empty;

            var props = t.GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => p.PropertyType.IsSimpleTypeV2() == true).ToArray();
            var prps = props.AsQueryable().Where(p => (p.GetCustomAttributes(typeof(KeyAttribute), true) ?? new object[] { }).Length > 0).ToArray();
            prps = prps ?? new PropertyInfo[] { };

            PropertyInfo pinfo = null;

            if (prps.Length > 0)
            {
                pinfo = prps[0];
            }
            else
            {
                if (isFirstPropKey)
                    pinfo = props[0];
            }

            if (pinfo != null)
            {
                ColumnAttribute ca = pinfo.GetCustomAttribute<ColumnAttribute>(inherit: true);
                idCol = ca != null ? ca.Name : pinfo.Name;
            }

            return idCol;
        }

        public static string GetPropertyColumnOfType(this Type t, string propName)
        {
            if (string.IsNullOrWhiteSpace(propName))
                throw new Exception("Property Name should be defined.");

            PropertyInfo pinfo = t.GetProperty(propName);

            if (pinfo == null)
                throw new Exception("Property Name with given name could not be found.");

            string col = string.Empty;
            ColumnAttribute ca = pinfo.GetCustomAttribute<ColumnAttribute>(inherit: true);
            col = ca != null ? ca.Name : pinfo.Name;

            if (string.IsNullOrWhiteSpace(col))
                col = pinfo.Name;

            return col;
        }

        public static string GetTableNameOfType(this Type t)
        {
            string s = t.Name;

            TableAttribute tt = t.GetCustomAttribute<TableAttribute>();
            if (tt != null)
            {
                s = string.IsNullOrWhiteSpace(tt.Name) ? s : tt.Name;
            }

            return s;
        }

        public static string GetSchemaNameOfType(this Type t)
        {
            string s = string.Empty;

            TableAttribute tt = t.GetCustomAttribute<TableAttribute>();
            if (tt != null)
            {
                s = tt.Schema ?? string.Empty;
            }

            return s;
        }

        public static IDictionary<string, string> GetColumnsOfType(this Type t)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            var props = t.GetProperties();

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

        public static bool IsKeyColumnColumnNumeric(this Type t)
        {
            bool isIdColNumeric = false;

            string key = t.GetKeyOfType();

            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("Key Property must be defined.");

            isIdColNumeric = TypeHelper.IsNumeric(t.GetProperty(key).PropertyType);

            return isIdColNumeric;
        }

        public static PropertyInfo[] GetValidPropertiesOfType(this Type t)
        {
            var props = t.GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true).ToArray();

            return props;
        }

        public static IDictionary<string, Type> GetPropertyTypesOfType(this Type t)
        {
            IDictionary<string, Type> types = null;

            var props = t.GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true).ToArray();

            props.ToList().ForEach(p => types[p.Name] = p.PropertyType);

            return types;
        }
    }

    public static class TypeHelper
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),  typeof(decimal), typeof(long),
            typeof(short),   typeof(sbyte),  typeof(byte),
            typeof(ulong),   typeof(ushort), typeof(uint)
            //, typeof(float),  typeof(double)
        };

        public static bool IsNumeric(Type myType)
        {
            return NumericTypes.Contains(Nullable.GetUnderlyingType(myType) ?? myType);
        }
    }
}