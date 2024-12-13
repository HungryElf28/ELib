using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using DAL.Repository;
using Interfaces.Repository;
using Interfaces.Services;
using Ninject;
using Ninject.Modules;

namespace ELib.Util
{
    public class NinjectRegistrations: NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IUserSession>().To<UserSession>().InSingletonScope();
            Bind<IBookService>().To<BookService>().InSingletonScope();
            Bind<IReadingService>().To<ReadingService>().InSingletonScope();
            Bind<ITariffService>().To<TariffService>().InSingletonScope();

        }
    }
}
