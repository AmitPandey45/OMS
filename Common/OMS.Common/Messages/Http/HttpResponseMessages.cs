using OMS.Common.Extensions;
using OMS.Common.MessageProviders;
using System.Net;

namespace OMS.Common.Messages.Http
{
    public class HttpResponseMessages : BaseResponseMessageProvider
    {
        public static readonly Dictionary<int, string> Codes = new Dictionary<int, string>
        {
            { (int)HttpStatusCode.OK, "OK" },
            { (int)HttpStatusCode.Created, "Created" },
            { (int)HttpStatusCode.NoContent, "No Content" },
            { (int)HttpStatusCode.BadRequest, "Bad Request" },
            { (int)HttpStatusCode.Unauthorized, "Unauthorized" },
            { (int)HttpStatusCode.Forbidden, "Forbidden" },
            { (int)HttpStatusCode.NotFound, "Not Found" },
            { (int)HttpStatusCode.InternalServerError, "Internal Server Error" },
            { (int)HttpStatusCode.BadGateway, "Bad Gateway" },
            { (int)HttpStatusCode.ServiceUnavailable, "Service Unavailable" },
        };

        public override string GetMessage(int code) => Codes.GetMessage(code, "Unknown HTTP status code");
    }
}
