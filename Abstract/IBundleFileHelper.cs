using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BundleDebugger.Abstract
{
    public interface IBundleFileHelper
    {
        List<string> List(string path);
    }
}
