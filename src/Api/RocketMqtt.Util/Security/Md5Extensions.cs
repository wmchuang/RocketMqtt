using System.Text;

namespace RocketMqtt.Util.Security
{
    public static class Md5Extensions
    {
        /// <summary>
        /// 检查MD5是否匹配
        /// </summary>
        /// <param name="input">用来比较的明文</param>
        /// <param name="hash">密文</param>
        /// <returns></returns>
        public static bool CompareMd5Hash(this string input, string hash)
        {
            using System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create();
            var hashOfInput = GetMd5Hash(md5Hash, input);
            var comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            return false;
        }

        public static string ToMd5String(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            using var md5 = System.Security.Cryptography.MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "");
        }

        private static string GetMd5Hash(System.Security.Cryptography.MD5 md5Hash, string input)
        {
            var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var item in bytes)
                sb.Append(item.ToString("x2"));

            return sb.ToString();
        }
    }
}