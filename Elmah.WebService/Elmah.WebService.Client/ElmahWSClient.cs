using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Elmah.WebService.Client
{
    class ElmahWSClient
    {


        string _url = "";

        private static HttpClient Client;
        private static HttpClientHandler httpClientHandler;

        public ElmahWSClient(string url, int timeout, bool useCompression)
        {
            if (String.IsNullOrEmpty(url))

            {
                throw new NullReferenceException("Url can't be blank");

            }
            if (Client == null)
            {
                if (useCompression)
                {
                    httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };

                    Client = new HttpClient(httpClientHandler) { Timeout = new TimeSpan(0, 0, timeout) };
                }
                else
                {
                    Client = new HttpClient() { Timeout = new TimeSpan(0, 0, timeout) };
                }

            }
            

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this._url = url;

        }


        public void SendToServer(Error error)

        {

            if (error == null) throw new NullReferenceException();


            try
            {

                string errorXml = ErrorXml.EncodeString(error);

                string jsonError = JsonConvert.SerializeObject(error, Formatting.Indented);

                HttpResponseMessage response = Client.PostAsXmlAsync(_url, errorXml).Result;

                return;

            }
            catch (Exception ex)
            {
                return;
            }



        }




    }
}
