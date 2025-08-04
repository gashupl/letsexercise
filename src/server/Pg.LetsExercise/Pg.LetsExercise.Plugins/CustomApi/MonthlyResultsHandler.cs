using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Model;
using Pg.LetsExercise.Plugins.Core;
using System;

namespace Pg.LetsExercise.Plugins.CustomApi
{
    public class MonthlyResultsDependencyLoader : DependencyLoaderBase
    {
        public MonthlyResultsDependencyLoader()
        {
            Register<IInputParameterToDateService, InputParameterToDateService>();
            Register<IParseToJsonService, ParseToJsonService>();
            Register<ISumResultsService, SumResultsService> ();
        }
    }

    public class MonthlyResultsHandler : PluginBase
    {
        public override IDependencyLoader DependencyLoader => new MonthlyResultsDependencyLoader();

        public MonthlyResultsHandler(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ExerciseGoalRetrieveHandler))
        {
        }

        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {

            if (localPluginContext == null)
            {
                localPluginContext.TracingService.Trace("LocalPluginContext is null.");
                throw new ArgumentNullException(nameof(localPluginContext));
            }
            var inputs = localPluginContext.PluginExecutionContext.InputParameters;
            if (inputs.TryGetValue(pg_monthlyresultsRequest.Fields.startmonth, out var startMonth)
                && inputs.TryGetValue(pg_monthlyresultsRequest.Fields.endmonth, out var endMonth))
            {

                var inputParameterToDateService = DependencyLoader.Get<IInputParameterToDateService>();
                var parseToJsonService = DependencyLoader.Get<IParseToJsonService>();
                var sumResultsService = DependencyLoader.Get<ISumResultsService>();

                var startMonthDate = inputParameterToDateService.GetDate(startMonth.ToString());
                var endMonthDate = inputParameterToDateService.GetDate(endMonth.ToString());

                localPluginContext.TracingService.Trace($"Parsed input params: {startMonthDate} & {endMonthDate}");

                var monthlyResults = sumResultsService.GetMonthlyResults(startMonthDate, endMonthDate, localPluginContext.PluginExecutionContext.InitiatingUserId);
                var parsedMonthyResults = parseToJsonService.Parse(monthlyResults);

                localPluginContext.TracingService.Trace($"Parsed monthly results: {parsedMonthyResults}");
                localPluginContext.PluginExecutionContext.OutputParameters[pg_monthlyresultsResponse.Fields.Results_1] = parsedMonthyResults;
            }
            else
            {
                localPluginContext.TracingService.Trace("Invalid input parameters for MonthlyResultsHandler logic execution");
                throw new InvalidPluginExecutionException("Invalid input parameters for MonthlyResultsHandler logic execution");
            }


        }
    }
}
