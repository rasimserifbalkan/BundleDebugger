using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BundleDebugger.Abstract;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;
namespace BundleDebugger.Helpers
{
    public class BundleFileHelper : IBundleFileHelper
    {
        public List<string> List(string path)
        {
            List<string> result = new List<string>();

            //StyleBundle
            //(ScriptBundle)
            var pathDetail = BundleTable.Bundles.FirstOrDefault(x => x.Path.Equals(path));

            var items = pathDetail.GetPrivatePropertyValue<dynamic>("Items");
            foreach (var item in items)
            {
                var virtualPath = ((object)item).GetPrivatePropertyValue<string>("VirtualPath");
                var currentHttpContext = HttpContext.Current;
                var httpContext = new HttpContextWrapper(currentHttpContext);
                var bundleContext = new BundleContext(httpContext, BundleTable.Bundles, virtualPath);
                var files = BundleTable.Bundles.GetBundleFor(path).EnumerateFiles(bundleContext);
                foreach (var file in files)
                {
                    result.Add(file.IncludedVirtualPath);
                }
                
            }

            return result;
        }


    }
}
