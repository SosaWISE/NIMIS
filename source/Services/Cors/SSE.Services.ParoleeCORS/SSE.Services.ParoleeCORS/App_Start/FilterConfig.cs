using System.Web;
using System.Web.Mvc;

namespace SSE.Services.ParoleeCORS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}