using Elmah.WebService.Client;
using Elmah.WebService.Client.Interfaces;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Elmah
{
    public class WebServiceErrorLog : ErrorLog
    {

        ElmahWSClient elmahWsClient;
        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceErrorLog"/> class
        /// using a dictionary of configured settings.
        /// </summary>
        public WebServiceErrorLog(IDictionary config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            var appName = (string)config["applicationName"] ?? string.Empty;

            string url;
            int urltimeout;
            bool useCompression;
            string configProviderType = GetElmahWSClientProperty<string>(config, "ConfigurationProviderType", false, "");

            if (configProviderType != "")

            {


                var configurationProvider = GetConfigurationProvider(configProviderType);

                if (configurationProvider != null)
                {

                    IConfigurationProvider configurationProviderInstance = (IConfigurationProvider)configurationProvider;

                    url = configurationProviderInstance.GetWebServiceUrl();
                    urltimeout = configurationProviderInstance.GetWebServiceUrlTimeout();
                    useCompression = configurationProviderInstance.GetWebServiceUseCompression();
                    elmahWsClient = new ElmahWSClient(url, urltimeout, useCompression);

                    return;

                }




            }

            url = GetElmahWSClientProperty<string>(config, "WebServiceUrl", false, "");
            urltimeout = GetElmahWSClientProperty<int>(config, "WebServiceUrlTimeout", true, 5); //use a 5 second timeout as elmah 1.2 does not log asynchronously and logging this error will block the client until it completes or times out. 
            useCompression = GetElmahWSClientProperty<bool>(config, "WebServiceUseCompression", true, true);
            elmahWsClient = new ElmahWSClient(url, urltimeout, useCompression);


            //
            // Set the application name as this implementation provides
            // per-application isolation over a single store.
            //



        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlErrorLog"/> class
        /// to use a specific connection string for connecting to the database.
        /// </summary>

        private object GetConfigurationProvider(string configProviderTypeName)
        {


            Type configProvider = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                  from type in assembly.GetTypes()
                                  where type.FullName == configProviderTypeName
                                  select type).FirstOrDefault<Type>();

            return Activator.CreateInstance(configProvider);

            
        }



        public void ThrowClientException()
        {

            throw new NotImplementedException("You should not be trying to view elmah logs on this front facing webserver.");

        }



        /// <summary>
        /// Gets the name of this error log implementation.
        /// </summary>
        public override string Name
        {
            get { return "Elmah Web Service Error Log"; }
        }


        /// <summary>
        /// Logs an error in log for the application.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public override string Log(Error error)
        {
            if (error == null)
                throw new ArgumentNullException("error");

            error.ApplicationName = ApplicationName;

            elmahWsClient.SendToServer(error);

            return "";
        }

        /// <summary>
        /// Retrieves a single application error from log given its
        /// identifier, or null if it does not exist.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ErrorLogEntry GetError(string id)
        {
            ThrowClientException();
            return null;
        }

        /// <summary>
        /// Retrieves a page of application errors from the log in
        /// descending order of logged time.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="errorEntryList"></param>
        /// <returns></returns>
        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            ThrowClientException();
            return 0;
        }



        public virtual T GetElmahWSClientProperty<T>(IDictionary config, string configKey, bool optional, T defaultValue)
        {


            if (config.Contains(configKey))
            {

                return (T)(Convert.ChangeType(config[configKey], typeof(T)));
            }
            else
            {
                if (optional)
                {
                    return (T)(defaultValue);
                }
                else
                {
                    throw new Exception("web.config missing " + configKey + " value");
                }

            }

        }
    }
}
