using System.Web;
using System.Web.Mvc;

namespace WebService.Elmah.Server.Sample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
