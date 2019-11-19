using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BundleDebugger.Helpers
{
    public class BundleRequestContainer
    {
        public static void Attach<T>(T instance)
        {
            if (HttpContext.Current.Items[typeof(T)] == null)
            {
                HttpContext.Current.Items.Add(typeof(T), instance);
            }
            else
            {
                HttpContext.Current.Items[typeof(T)] = instance;
            }
        }
        public static T Resolve<T>() where T : class
        {
            if (HttpContext.Current.Items[typeof(T)] == null)
            {
                HttpContext.Current.Items[typeof(T)] = Activator.CreateInstance<T>();
            }
            return HttpContext.Current.Items[typeof(T)] as T;
        }

        public static T Get<T>() where T : class
        {
            if (HttpContext.Current.Items[typeof(T)] != null)
            {
                return HttpContext.Current.Items[typeof(T)] as T;
            }
            else
            {
                HttpContext.Current.Items[typeof(T)] = Activator.CreateInstance<T>();
                return HttpContext.Current.Items[typeof(T)] as T;
            }
        }
        public static bool Exist<T>() where T : class
        {
            return HttpContext.Current.Items[typeof(T)] != null;
        }
    }
}
