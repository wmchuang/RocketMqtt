namespace RocketMqtt.Util.Exception
{
    public class OperationException : BaseCustomException
    {
        public OperationException(string errorMessage) : base(errorMessage, string.Empty)
        {
        }

        public OperationException(string errorMessage, string errorCode) : base(errorMessage, errorCode)
        {
        }
    }
}