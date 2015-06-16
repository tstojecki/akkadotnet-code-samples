using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebCrawler.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
