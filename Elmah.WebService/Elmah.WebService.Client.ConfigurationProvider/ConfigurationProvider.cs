using Elmah.WebService.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WebService.Client
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProvider()
        {

        }
        public string GetWebServiceUrl()
        
        {
         return "http://localhost/api/elmah";
        }
        public int GetWebServiceUrlTimeout()

        {
            return 30;
        }
        public bool GetWebServiceUseCompression()

        {
            return true;
        }
    }
}
