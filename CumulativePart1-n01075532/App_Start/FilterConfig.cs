using System.Web;
using System.Web.Mvc;

namespace CumulativePart1_n01075532
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
