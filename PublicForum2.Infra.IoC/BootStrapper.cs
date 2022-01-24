using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PublicForum2.Application.Interfaces;
using PublicForum2.Application.Services;
using PublicForum2.Domain.Interfaces.Repository;
using PublicForum2.Infra.Data.Context;
using PublicForum2.Infra.Data.Interfaces;
using PublicForum2.Infra.Data.Repository;
using PublicForum2.Infra.Identity.Configuration;
using PublicForum2.Infra.Identity.Context;
using PublicForum2.Infra.Identity.Model;
using SimpleInjector;

namespace PublicForum2.Infra.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<Identity.Context.AuthDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new Identity.Context.AuthDbContext()), Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            container.Register<EFContext>(Lifestyle.Scoped);

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            container.Register<ITopicService, TopicService>(Lifestyle.Scoped);

            container.Register<ITopicRepository, TopicRepository>(Lifestyle.Scoped);
        }
    }
}
