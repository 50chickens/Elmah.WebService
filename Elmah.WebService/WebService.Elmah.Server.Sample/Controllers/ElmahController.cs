using Elmah;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace WebService.Elmah.Server.Sample.Controllers
{
    public class ElmahController : ApiController
    {
        public void Post([FromBody]string xmlError)
        {


            Error e = ErrorXml.DecodeString(xmlError);

            Dictionary<string, string> configDictionary = new Dictionary<string, string>();

            configDictionary.Add("applicationName", e.ApplicationName);

            string connStr = @"Server=myServerAddress;Database=myDataBase;User Id=xxx;Password=xxx;";

            configDictionary.Add("connectionString", connStr);
            ErrorLog errorLog = new SqlErrorLog(configDictionary);
            errorLog.Log(e);


        }
        
    }
}
