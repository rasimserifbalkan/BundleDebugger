using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BundleDebugger.Abstract;
using BundleDebugger.Helpers;
using BundleDebugger.HttpModule;
using BundleDebugger.Repository;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace BundleDebugger
{
    public static class BundleDebug
    {
        public static void Start()
        {
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["BundleDebugger:Enabled"]))
                return;
            DynamicModuleUtility.RegisterModule(typeof(BundleHttpModule));
        }

        public static IHtmlString RenderScript(params string[] paths)
        {

            IBundleRepositoryBase bundleDebugRepositoryBase =
                BundleRequestContainer.Resolve<BundleRepositoryBase>();

            IBundleRepository bundleDebugRepository = new BundleRepository(bundleDebugRepositoryBase);
            BundleRequestContainer.Attach(bundleDebugRepository);

            IBundleFileHelper bundleFileHelper = new BundleFileHelper();
            var controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            var url = HttpContext.Current.Request.RequestContext.RouteData.Route.GetPrivatePropertyValue<string>("Url");

            foreach (var path in paths)
            {
                bundleDebugRepository.Add(bundleFileHelper.List(path), path, action, controller, url);
            }

            return System.Web.Optimization.Scripts.RenderFormat(System.Web.Optimization.Scripts.DefaultTagFormat, paths);

        }
        public static IHtmlString RenderCss(params string[] paths)
        {
            IBundleRepositoryBase bundleDebugRepositoryBase =
                BundleRequestContainer.Resolve<BundleRepositoryBase>();

            IBundleRepository bundleDebugRepository = new BundleRepository(bundleDebugRepositoryBase);
            BundleRequestContainer.Attach(bundleDebugRepository);

            IBundleFileHelper bundleFileHelper = new BundleFileHelper();
            var controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            var url = HttpContext.Current.Request.RequestContext.RouteData.Route.GetPrivatePropertyValue<string>("Url");

            foreach (var path in paths)
            {
                bundleDebugRepository.Add(bundleFileHelper.List(path), path, action, controller, url);
            }

            //@Styles.Render("~/Content/css")
            return System.Web.Optimization.Styles.Render(paths);
        }
    }
}
