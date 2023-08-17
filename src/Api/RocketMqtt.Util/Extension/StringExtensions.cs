namespace RocketMqtt.Util.Extension;

public static class StringExtensions
{
    public static string GetRandomString(int len = 5)
    {
        if (len > 32) len = 32;
        if (len < 0) len = 5;
        return Guid.NewGuid().ToString("N").Substring(0, len);
    }

}