using System;
using System.ComponentModel;

namespace lights.api.TypeConverters
{
    public class IdTypeConverter<TUid> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value)
        {
            if (value is string str)
            {
                return Activator.CreateInstance(typeof(TUid), str);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
