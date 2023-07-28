using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Zyre
{
    internal class InterfaceConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        // From display name to NetworkInterface
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var casted = value as string;
            return casted != null
                ? NetworkInterface.GetAllNetworkInterfaces().Where(x => x.Name == casted).FirstOrDefault()
                : base.ConvertFrom(context, culture, value);
        }

        // From NetworkInterface to display name
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var casted = value as NetworkInterface;
            return destinationType == typeof(string) && casted != null
                ? casted.Name
                : base.ConvertTo(context, culture, value, destinationType);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(n => n.OperationalStatus == OperationalStatus.Up).ToArray();

            return new StandardValuesCollection(networkInterfaces);
        }
    }
}
