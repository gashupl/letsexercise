using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Pg.LetsExercise.Domain.Repositories;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Model;
using Pg.LetsExercise.Plugins.Core;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Services;
using System.ServiceModel;

namespace Pg.LetsExercise.Plugins
{
    public abstract class PluginBase<T> : IPlugin where T : PluginHandlerBase
    {
        protected string PluginClassName { get; }
        public virtual IDependencyLoader DependencyLoader { get; set; }
        public IRepository DataRepository { get; set; }

        internal PluginBase(Type pluginClassName)
        {
            PluginClassName = pluginClassName.ToString();
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new InvalidPluginExecutionException(nameof(serviceProvider));
            }


            var localPluginContext = new LocalPluginContext(serviceProvider);
            DependencyLoader.RegisterDefaults(localPluginContext);

            localPluginContext.Trace(LogLevel.Trace, "Entered {PluginClassName}.Execute() " +
                "Correlation Id: {CorrelationId}, " +
                "Initiating User: {InitiatingUserId}",
                PluginClassName, 
                localPluginContext.PluginExecutionContext.CorrelationId,
                localPluginContext.PluginExecutionContext.InitiatingUserId);

            try
            {
                localPluginContext.Trace(LogLevel.Trace, "Resolving dependencies before plugin execution...");
                var pluginHandler = DependencyLoaderBase.Container.Resolve<T>();
                pluginHandler.Init(localPluginContext);

                localPluginContext.Trace(LogLevel.Trace, "Check if plugin can be executed...");
                if (pluginHandler.CanExecute())
                {
                    localPluginContext.Trace(LogLevel.Trace, "Executing plugin handler...");
                    pluginHandler.Execute();
                    localPluginContext.Trace(LogLevel.Trace, "Plugin handler executed");
                }
                else
                {
                    localPluginContext.Trace(LogLevel.Trace, 
                        "Skipping execution of {PluginClassName} as CanExecute returned false.",
                        PluginClassName);
                }

                // Now exit - if the derived plugin has incorrectly registered overlapping event registrations, guard against multiple executions.
                return;
            }
            catch (FaultException<OrganizationServiceFault> orgServiceFault)
            {
                localPluginContext.Trace(LogLevel.Trace, $"Exception: {orgServiceFault.ToString()}");

                throw new InvalidPluginExecutionException($"OrganizationServiceFault: {orgServiceFault.Message}", orgServiceFault);
            }
            catch (Exception ex)
            {
                localPluginContext.Trace(LogLevel.Trace, $"Exception: {ex.ToString()}");
                // Log the exception to the tracing service
                localPluginContext.Trace(LogLevel.Trace, $"Exception: {ex.Message}");
                localPluginContext.Trace(LogLevel.Trace, $"Stack Trace: {ex.StackTrace}");
            }
            finally
            {
                localPluginContext.Trace(LogLevel.Trace, $"Exiting {PluginClassName}.Execute()");
            }
        }
    }

}
