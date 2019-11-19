using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BundleDebugger.Abstract;
using BundleDebugger.Concrete;

namespace BundleDebugger.Repository
{
    public class BundleRepositoryBase : IBundleRepositoryBase
    {
        private readonly ConcurrentQueue<BundleModel> _concurrentQueue;

        public BundleRepositoryBase()
        {
            _concurrentQueue = new ConcurrentQueue<BundleModel>();
        }

        public void Add(List<string> files, string path, string view, string controller, string routeUrl)
        {
            _concurrentQueue.Enqueue(new BundleModel()
            {
                Files = files,
                Path = path,
                Controller = controller,
                RouteUrl = routeUrl,
                View = view

            });
        }

        public IEnumerable<BundleModel> List()
        {
            return _concurrentQueue.ToList();
        }
    }
}
