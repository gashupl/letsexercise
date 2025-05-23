using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Domain.Repositories;
using Pg.LetsExercise.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using TinyIoC;

namespace Pg.LetsExercise.Plugins.Core
{
    public class DependencyLoaderBase : IDependencyLoader
    {
        protected TinyIoCContainer container = new TinyIoCContainer(); 

        public void RegisterDefaults(ILocalPluginContext context)
        {
            container.Register(context);
            container.Register(context.OrgSvcFactory); 
            container.Register(context.TracingService);
            container.Register<IRepository, DataRepository>(); 
        }

        public I Get<I>() where I : class
        {
            return container.Resolve<I>();
        }

        public void Register<I, C>() where I : class where C : class, I
        {
            container.Register<I, C>();
        }

    }
}
