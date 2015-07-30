using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Reflection;

namespace Panther.CMS
{
    public abstract class DictionaryObject
    {
        private readonly Dictionary<string, string> values;

        protected DictionaryObject()
        {
            values = new Dictionary<string, string>();
        }

        protected DictionaryObject(Dictionary<string, string> values)
        {
            this.values = values;
        }


        protected virtual void SetField<T>(T value, [CallerMemberName]string key = null)
        {
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            if(value == null)
                throw new ArgumentNullException(nameof(value));

            key = CreateKey(key);
            var valueString = GetValueString(value);

            if (values.ContainsKey(key))

                values[key] = valueString;
            else
            {
                values.Add(key, valueString);
            }
        }

        protected virtual string GetValueString<T>(T value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return converter.ConvertToInvariantString(value);
        }

        protected virtual T GetField<T>([CallerMemberName]string key = null)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            key = CreateKey(key);

            return values.ContainsKey(key) ? GetFromDictionary<T>(key) : GetDefaultValue<T>(key);
        }

        protected virtual T GetDefaultValue<T>(string key)
        {
            var propName = key.Replace(GetType().Name + ".", "");
            var prop = GetType().GetProperty(propName);
            var attribute = prop.GetCustomAttributes<DefaultValueAttribute>().FirstOrDefault();
            if (attribute != null)
                return (T)attribute.Value;
            return default(T);
        }

        protected virtual T GetFromDictionary<T>(string key)
        {
            var value = values[key];
            var converter = TypeDescriptor.GetConverter(typeof(T));
            var valueObj = converter.ConvertFromString(value);
            return (T)valueObj;
        }

        protected virtual string CreateKey(string key)
        {
            return key;
        }

        public Dictionary<string, string> GetDictionary()
        {
            return values;
        }
    }
}
