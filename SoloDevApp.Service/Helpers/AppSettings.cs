namespace SoloDevApp.Service.Helpers
{
    public interface IAppSettings
    {
        string Secret { get; set; }
        string UrlMain { get; set; }
    }

    public class AppSettings : IAppSettings
    {
        // Thuộc tính Secret sẽ mapping với 
        // key Secret khai báo ở appsettings.json
        public string Secret { get; set; }
        public string UrlMain { get; set; }
    }
}