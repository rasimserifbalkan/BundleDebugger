using System;
using System.Collections.Generic;
using BundleDebugger.Abstract;
using BundleDebugger.Concrete;

namespace BundleDebugger.Repository
{
    public class BundleRepository : IBundleRepository
    {
        private readonly IBundleRepositoryBase _repositoryBase;


        public BundleRepository(IBundleRepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public void Add(List<string> files, string path, string view, string controller, string routeUrl)
        {
            _repositoryBase.Add(files, path, view, controller, routeUrl);
        }

        public List<BundleModel> List(Func<BundleModel, bool> predicate)
        {
            List<BundleModel> result = new List<BundleModel>();
            foreach (var bundle in _repositoryBase.List())
            {
                if (predicate.Invoke(bundle))
                {
                    result.Add(bundle);
                }
            }

            return result;

        }
    }
}
