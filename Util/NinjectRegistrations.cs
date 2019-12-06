using WebApplication3.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Web.Mvc;

namespace WebApplication3.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Unbind<ModelValidatorProvider>();

            Bind<AppContext>().ToSelf()
                .InRequestScope()
                .WithConstructorArgument("dbContext", new AppContext());

            Bind<IUserRepository>().To<UserRepository>()
                .InRequestScope()
                .WithConstructorArgument("repository", new UserRepository());
        }
    }
}