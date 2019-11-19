using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BundleDebugger.HttpModule
{
    public class BundleHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(this.OnEndRequest);
            context.BeginRequest += new EventHandler(this.context_BeginRequest);
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
           
        }
        private void OnEndRequest(object sender, EventArgs e)
        {
            new BundleHttpHandler().ProcessRequest(HttpContext.Current);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
