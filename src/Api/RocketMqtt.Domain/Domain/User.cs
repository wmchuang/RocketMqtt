using RocketMqtt.Util.Security;
using RocketMqtt.Util.Extension;
namespace RocketMqtt.Domain.Domain;

public class User : EntityBase
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 加密盐
    /// </summary>
    public string Salt { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    public User()
    {
    }

    public User(string userName, string password, string remark) : this()
    {
        Salt = StringExtensions.GetRandomString();
        UserName = userName;
        Password = CreatePassword(password, Salt);
        Remark = remark;
    }

    /// <summary>
    /// 生成密码
    /// </summary>
    /// <param name="password">用户输入密码</param>
    /// <param name="salt">加密盐</param>
    /// <returns></returns>
    private string CreatePassword(string password, string salt)
    {
        return string.Concat(password, salt ?? "").ToMd5String();
    }
}