using API.Auth;

namespace API
{
    public class AppSettings
    {
        public string ConString { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public string EmailFrom { get; set; }
        public string EmailPassword { get; set; }
    }
}
