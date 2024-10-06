namespace OMS.Common.MessageProviders
{
    public abstract class BaseResponseMessageProvider : IResponseMessageProvider
    {
        public abstract string GetMessage(int code);
    }
}
