namespace RocketMqtt.Util.Exception
{
    public class BaseCustomException : System.Exception
    {
        public BaseCustomException(string errorMessage, string errorCode) : base(errorMessage)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 异常编码
        /// </summary>
        public string ErrorCode { get; }
    }
}