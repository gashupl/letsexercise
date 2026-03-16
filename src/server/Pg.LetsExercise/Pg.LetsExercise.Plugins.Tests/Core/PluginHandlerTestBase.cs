using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Moq;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Plugins.Core;

namespace Pg.LetsExercise.Plugins.Tests.Core
{
    public abstract class PluginHandlerTestBase
    {

        public ILocalPluginContext CreateLocalPluginContext()
        {
            var localPluginContextMock = CreateLocalPluginHandlerMock();    
            localPluginContextMock.Setup(c => c.PluginExecutionContext.InputParameters)
                .Returns(new ParameterCollection());

            return localPluginContextMock.Object;
        }

        public ILocalPluginContext CreateLocalPluginContext(ParameterCollection inputParameters)
        {
            var localPluginContextMock = CreateLocalPluginHandlerMock();
            localPluginContextMock.Setup(c => c.PluginExecutionContext.InputParameters)
                .Returns(inputParameters);

            return localPluginContextMock.Object;
        }

        private Mock<ILocalPluginContext> CreateLocalPluginHandlerMock()
        {
            var tracingServiceMock = new Mock<IPluginTracingService>();
            tracingServiceMock.Setup(ts => ts.Trace(It.IsAny<LogLevel>(), It.IsAny<string>()));

            var localPluginContextMock = new Mock<ILocalPluginContext>();
            localPluginContextMock.Setup(c => c.TracingService)
                .Returns(tracingServiceMock.Object);

            localPluginContextMock.Setup(c => c.PluginExecutionContext.OutputParameters)
    .           Returns(new ParameterCollection());

            return localPluginContextMock;
        }
    }
}
