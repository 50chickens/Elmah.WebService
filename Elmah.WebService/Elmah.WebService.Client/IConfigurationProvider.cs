namespace Elmah.WebService.Client.Interfaces
{
    public interface IConfigurationProvider
    {
        string GetWebServiceUrl();
        string ApplicationName { get; set; }
        int GetWebServiceUrlTimeout();
        bool GetWebServiceUseCompression();
    }
}