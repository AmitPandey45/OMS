namespace OMS.Common
{
    public class UserContext
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ClientIp { get; set; }
        public bool IsTestUser { get; set; }
    }
}
