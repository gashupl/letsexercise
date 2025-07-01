using Pg.LetsExercise.Model;
using System;

namespace Pg.LetsExercise.Plugins.CustomApi
{
    public class MonthlyResultsHandler : PluginBase
    {
        public MonthlyResultsHandler(Type pluginClassName) : base(pluginClassName)
        {
        }

        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            var inputs = localPluginContext.PluginExecutionContext.InputParameters;
            if(inputs.TryGetValue(pg_monthlyresultsRequest.Fields.startmonth, out var startMonth) 
                && inputs.TryGetValue(pg_monthlyresultsRequest.Fields.endmonth, out var endMonth))
            {
                localPluginContext.PluginExecutionContext.OutputParameters[pg_monthlyresultsResponse.Fields.Results_1] = String.Empty; 
            }
        }
    }
}
