using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BundleDebugger.Concrete
{
    public class BundleModel
    {
        public string Path { get; set; }
        public List<string> Files { get; set; }

        public string View { get; set; }

        public string Controller { get; set; }
        public string RouteUrl { get; set; }



    }
}
