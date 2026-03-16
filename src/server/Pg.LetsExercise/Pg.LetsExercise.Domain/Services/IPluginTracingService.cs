using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace Pg.LetsExercise.Domain.Services
{
    public interface IPluginTracingService
    {
        void Trace(LogLevel logLevel, string format, params object[] args);
    }
}
