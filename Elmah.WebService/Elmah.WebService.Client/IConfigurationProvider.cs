namespace Elmah.WebService.Client.Interfaces
{
    public interface IConfigurationProvider
    {
        string GetWebServiceUrl();
        int GetWebServiceUrlTimeout();
        bool GetWebServiceUseCompression();
    }
}