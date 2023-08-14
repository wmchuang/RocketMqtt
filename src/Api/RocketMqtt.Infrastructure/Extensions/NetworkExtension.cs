using System.Net;

namespace RocketMqtt.Infrastructure.Extensions;

public static class NetworkExtension
{
    public static IPAddress IpAddress { get; set; }

    /// <summary>
    /// 获取ip
    /// </summary>
    /// <returns></returns>
    public static IPAddress GetIp()
    {
        try
        {
            if (IpAddress != null)
                return IpAddress;

            string ipAddress = string.Empty;
            // 使用 Dns.GetHostName 方法获取本地主机名称
            string hostName = Dns.GetHostName();

            // 使用 Dns.GetHostEntry 方法获取本地主机地址信息
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            // 选择 IPv4 地址
            var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            IpAddress = ipv4Address;
            return ipv4Address;
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            return IPAddress.Loopback;
        }
    }
}