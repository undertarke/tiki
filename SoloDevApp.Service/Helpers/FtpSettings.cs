namespace SoloDevApp.Service.Helpers
{
    public interface IFtpSettings
    {
        string Ip { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string UrlServer { get; set; }
    }
    
    public class FtpSettings : IFtpSettings
    {
        public string Ip { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UrlServer { get; set; }
    }
}