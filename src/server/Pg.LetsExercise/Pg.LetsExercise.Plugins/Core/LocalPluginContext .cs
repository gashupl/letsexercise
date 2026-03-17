using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsExercise.Plugins.Core
{
    /// <summary>
    /// Plug-in context object.
    /// </summary>
    public class LocalPluginContext : ILocalPluginContext
    {
        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Current Executing user.
        /// </summary>
        public IOrganizationService InitiatingUserService { get; }

        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Account that was registered to run this plugin, This could be the same user as InitiatingUserService.
        /// </summary>
        public IOrganizationService PluginUserService { get; }

        /// <summary>
        /// IPluginExecutionContext contains information that describes the run-time environment in which the plug-in executes, information related to the execution pipeline, and entity business information.
        /// </summary>
        public IPluginExecutionContext4 PluginExecutionContext { get; }

        /// <summary>
        /// Synchronous registered plug-ins can post the execution context to the Microsoft Azure Service Bus. <br/>
        /// It is through this notification service that synchronous plug-ins can send brokered messages to the Microsoft Azure Service Bus.
        /// </summary>
        public IServiceEndpointNotificationService NotificationService { get; }

        /// <summary>
        /// General Service Provider for things not accounted for in the base class.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// OrganizationService Factory for creating connection for other then current user and system.
        /// </summary>
        public IOrganizationServiceFactory OrgSvcFactory { get; }

        public IPluginTracingService PluginTracingService { get; }

        /// <summary>
        /// Helper object that stores the services available in this plug-in.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public LocalPluginContext(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new InvalidPluginExecutionException(nameof(serviceProvider));
            }

            ServiceProvider = serviceProvider;
            PluginExecutionContext = serviceProvider.Get<IPluginExecutionContext4>();

            var logger = serviceProvider.Get<ILogger>();
            var tracingService = serviceProvider.GetService(typeof(ITracingService)) as ITracingService;

            PluginTracingService = new PluginTracingService(tracingService, logger);

            NotificationService = serviceProvider.Get<IServiceEndpointNotificationService>();

            OrgSvcFactory = GetServiceFactory(serviceProvider);

            PluginUserService = serviceProvider.GetOrganizationService(PluginExecutionContext.UserId); // User that the plugin is registered to run as, Could be same as current user.

            InitiatingUserService = serviceProvider.GetOrganizationService(PluginExecutionContext.InitiatingUserId); //User who's action called the plugin.

        }

        public IOrganizationServiceFactory GetServiceFactory(IServiceProvider serviceProvider)
        {
            var serviceFactory = serviceProvider.Get<IOrganizationServiceFactory>();
            var proxyProvider = serviceFactory as IProxyTypesAssemblyProvider;
            if (proxyProvider != null)
            {
                proxyProvider.ProxyTypesAssembly = typeof(DataverseContext).Assembly;
            }
            return serviceFactory;
        }

        /// <summary>
        /// Writes a trace message to the trace log.
        /// </summary>
        public void Trace(LogLevel logLevel, string format, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(format) || PluginTracingService == null)
            {
                return;
            }
            PluginTracingService.Trace(logLevel, format, args);
        }
    }
}
