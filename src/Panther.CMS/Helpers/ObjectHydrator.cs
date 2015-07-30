using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Panther.CMS.Helpers
{
    public class ObjectHydrator
    {
        public static T Build<T>(Dictionary<string, string> values, string genericName = "")
        {
            var type = typeof(T);
            var typeName = type.Name.Replace(genericName, string.Empty);
            var obj = Activator.CreateInstance<T>();

            foreach (var prop in type.GetProperties())
            {
                var key = typeName + "." + prop.Name;
                if (values.ContainsKey(key))
                {
                    var value = values[key];
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                    var valueObj = converter.ConvertFromString(value);
                    prop.SetValue(obj, valueObj);
                }
                else
                {
                    var attribute = prop.GetCustomAttributes<DefaultValueAttribute>().FirstOrDefault();
                    if (attribute != null)
                        prop.SetValue(obj, attribute.Value);
                }
            }

            return obj;
        }

        public static void Into<T>(T obj, Dictionary<string, string> values, string genericName = "")
        {
            var type = typeof(T);
            var typeName = type.Name.Replace(genericName, string.Empty);

            foreach (var prop in type.GetProperties())
            {
                var value = prop.GetValue(obj);
                var key = typeName + prop.Name;
                if (values.ContainsKey(key))
                {
                    values[key] = value.ToString();
                }
                else
                {
                    values.Add(key, value.ToString());
                }
            }
        }
    }
}
