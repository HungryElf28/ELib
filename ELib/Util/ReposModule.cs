using BLL.Services;
using DAL.Repository;
using Interfaces.Repository;
using Interfaces.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.Util
{
    public class ReposModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbRepos>().To<DbReposSQL>().InSingletonScope();
        }
    }
}
