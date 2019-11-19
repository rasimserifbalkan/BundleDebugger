using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Hosting;
using BundleDebugger.Abstract;
using BundleDebugger.Concrete;
using BundleDebugger.Helpers;
using System.Linq;

namespace BundleDebugger.HttpModule
{
    public class BundleHttpHandler : IHttpHandler
    {
        private VirtualPathProvider _virtualPathProvider;

        public BundleHttpHandler()
            : this((VirtualPathProvider)null)
        {
        }

        public BundleHttpHandler(VirtualPathProvider virtualPathProvider)
        {
            this._virtualPathProvider = virtualPathProvider ?? HostingEnvironment.VirtualPathProvider;
        }

        public void ProcessRequest(HttpContext context)
        {
            var controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            var url = HttpContext.Current.Request.RequestContext.RouteData.Route.GetPrivatePropertyValue<string>("Url");

            IBundleRepository bundleDebugRepository = BundleRequestContainer.Get<IBundleRepository>();

            var bundles = bundleDebugRepository.List(x => x.Controller.Equals(controller) && x.View.Equals(action));
            string html = GenerateHtml(url, bundles);
            context.Response.Write(html);

        }

        private string GenerateHtml(string route, List<BundleModel> bundles)
        {

            StringBuilder bundleNames = new StringBuilder();
            foreach (var bundleItem in bundles)
            {
                bundleNames.Append($"<li>{bundleItem.Path}<ul>");
                foreach (var file in bundleItem.Files)
                {
                    if (bundles.Count(x => x.Files.Any(t => t.Equals(file))) > 1)
                    {
                        bundleNames.Append($"<li style='color:red'>{file}</li>");
                        continue;
                    }
                    bundleNames.Append($"<li>{file}</li>");

                }
                bundleNames.Append($"</ul></li>");
            }
            string html = "<style>.bundle-content{font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;}.bundle-header{border: 1px solid gray; background-color: royalblue; color: white;}.bundle-header h1{margin: 0; padding: 3px; font-size: 15px; font-weight: bold;}.bundle-route{font-size: 14px; padding: 3px;}.bundle-route>:first-child{font-weight: bold;}.bundle-details{border: 1px solid gray; background-color: royalblue; color: white;}.bundle-details h1{margin: 0; padding: 3px; font-size: 15px; font-weight: bold;}.bundle-detail-content{float: left;}.bundle-detail-content{float: left;}.bundleTable{display: table; width: 100%; font-size: 14px;}.bundleTableRow{display: table-row;}.bundleTableHeading{background-color: #EEE; display: table-header-group;}.bundleTableCell, .bundleTableHead{border: 1px solid #eaeaea; display: table-cell; padding: 3px 10px;}.bundleTableHeading{background-color: #EEE; display: table-header-group; font-weight: bold;}.bundleTableFoot{background-color: #EEE; display: table-footer-group; font-weight: bold;}.bundleTableBody{display: table-row-group;}</style> <div> <div class=\"bundle-header\"> <h1>Bundle Debugerx</h1> </div><div class=\"bundle-route\"> <span>Route : </span><span>" + route + "</span> </div><div class=\"bundle-details\"> <h1>Bundles</h1> </div><div class=\"bundleTable\"> <div class=\"bundleTableBody\"> <div class=\"bundleTableRow\"> <div class=\"bundleTableCell\"><b>Bundle Names</b></div></div><div class=\"bundleTableRow\"> <div class=\"bundleTableCell\"> <ul>" + bundleNames.ToString() + "</ul> </div></div></div></div></div>";
            return html;

        }




        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
