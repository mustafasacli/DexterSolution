using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Dexter.Extensions
{
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

    }
}
