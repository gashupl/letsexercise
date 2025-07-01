using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Domain.Repositories;
using Pg.LetsExercise.Infrastructure.Repositories;
using System;
using TinyIoC;
using static Pg.LetsExercise.Plugins.PluginBase;

namespace Pg.LetsExercise.Plugins.Core
{
    public class DependencyLoaderBase : IDependencyLoader
    {
        protected static TinyIoCContainer container = new TinyIoCContainer(); 

        public void RegisterDefaults(LocalPluginContext localContext)
        {
            localContext.Trace("Registering default dependencies...");
            var userOrganizationService = localContext.OrgSvcFactory.CreateOrganizationService(Guid.Empty);
            var dataRepository = new DataRepository(localContext.OrgSvcFactory);

            container.Register(localContext);
            container.Register(localContext.Logger);
            container.Register(localContext.OrgSvcFactory);
            container.Register(userOrganizationService);
            container.Register(localContext.PluginUserService);
            container.Register(localContext.TracingService);
            container.Register<IRepository>(dataRepository); 
        }

        public I Get<I>() where I : class
        {
            try
            {
                return container.Resolve<I>();
            }
            catch (TinyIoCResolutionException ex)
            {
                throw new InvalidPluginExecutionException(ex.InnerException?.Message ?? "An error occurred while resolving the dependency.", ex);
            }
            
        }

        public void Register<I, C>() where I : class where C : class, I
        {
            container.Register<I, C>();
        }
    }
}
