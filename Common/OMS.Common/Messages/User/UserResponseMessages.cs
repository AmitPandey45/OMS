using OMS.Common.Extensions;
using OMS.Common.MessageProviders;

namespace OMS.Common.Messages.User
{
    public class UserResponseMessages : BaseResponseMessageProvider
    {
        public static readonly Dictionary<int, string> Codes = new Dictionary<int, string>
        {
            { 1000, "User created successfully." },
            { 1001, "User already exists." },
            { 1002, "User not found." },
            { 1003, "Failed to create user." },
            { 1004, "Invalid user data." }
        };

        public override string GetMessage(int code) => Codes.GetMessage(code, "Unknown user code");
    }
}
