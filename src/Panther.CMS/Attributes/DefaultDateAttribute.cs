using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Panther.CMS.Attributes
{
    public class DefaultDateAttribute : DefaultValueAttribute
    {
        public DefaultDateAttribute(Type type, string value) : base(type, value)
        {
        }

        public DefaultDateAttribute(char value) : base(value)
        {
        }

        public DefaultDateAttribute(byte value) : base(value)
        {
        }

        public DefaultDateAttribute(short value) : base(value)
        {
        }

        public DefaultDateAttribute(int value) : base(value)
        {
        }

        public DefaultDateAttribute(long value) : base(value)
        {
        }

        public DefaultDateAttribute(float value) : base(value)
        {
        }

        public DefaultDateAttribute(double value) : base(value)
        {
        }

        public DefaultDateAttribute(bool value) : base(value)
        {
        }

        public DefaultDateAttribute(string value) : base(value)
        {
        }

        public DefaultDateAttribute(object value) : base(value)
        {
        }

        public override object Value { get { return DateTime.Now.Date; } }
    }
}
