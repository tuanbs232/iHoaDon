[assembly: WebActivator.PreApplicationStartMethod(typeof(iHoaDon.Web.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(iHoaDon.Web.App_Start.NinjectMVC3), "Stop")]
namespace iHoaDon.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using System.Configuration;
    using System.Security.Principal;
    using System.Web;
    using Ninject;
    using Ninject.Web.Mvc;
    using iHoaDon.Infrastructure;

    public static class NinjectMVC3
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var iHoaDonConn = ConfigurationManager.ConnectionStrings["iHoaDon"].ConnectionString;
            kernel.Bind<IUnitOfWork>()
                    .To<iHoaDon.DataAccess.EWhiteHatContext>()
                    .WithConstructorArgument("connectionString", iHoaDonConn);
            kernel.Bind<IPrincipal>()
                    .ToMethod(ctx => HttpContext.Current == null
                                        ? null
                                        : HttpContext.Current.User).InRequestScope();
        }
    }
}