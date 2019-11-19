using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BundleDebugger.Concrete;

namespace BundleDebugger.Abstract
{
    public interface IBundleRepository
    {
        List<BundleModel> List(Func<BundleModel, bool> predicate);
        void Add(List<string> files, string path, string view, string controller, string routeUrl);
    }
}
