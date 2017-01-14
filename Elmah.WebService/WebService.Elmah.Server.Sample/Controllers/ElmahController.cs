using Elmah;
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

            HttpContext current = HttpContext.Current;
            ErrorLog.GetDefault(current).Log(e);
            

        }
        
    }
}
