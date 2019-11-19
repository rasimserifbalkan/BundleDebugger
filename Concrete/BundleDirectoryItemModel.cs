using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BundleDebugger.Concrete
{
    public class BundleDirectoryItemModel
    {
        public PatternType PatternType { get; set; }
        public string SearchPattern { get; set; }
        public bool SearchSubdirectories { get; set; }
        public string VirtualPats { get; set; }
        public List<object> Transforms { get; set; }
    }
}
