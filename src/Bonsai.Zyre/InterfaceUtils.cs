using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Zyre
{
    public static class InterfaceUtils
    {
        public static string GetIPAddress(NetworkInterface networkInterface)
        {
            var address = networkInterface.GetIPProperties().UnicastAddresses.Where(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            return address != null
                ? address.Address.ToString()
                : "";
        }
    }
}
