namespace OMS.Common.MessageProviders
{
    public interface IResponseMessageProvider
    {
        string GetMessage(int code);
    }
}
