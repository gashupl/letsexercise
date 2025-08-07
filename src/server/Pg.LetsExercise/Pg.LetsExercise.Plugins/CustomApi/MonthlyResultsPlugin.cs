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

    public class MonthlyResultsPlugin : PluginBase<MonthlyResultHandler>
    {
        public override IDependencyLoader DependencyLoader => new MonthlyResultsDependencyLoader();

        public MonthlyResultsPlugin(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ExerciseGoalRetrievePlugin))
        {
        }
    }

    public class MonthlyResultHandler : PluginHandlerBase
    {
        private readonly IInputParameterToDateService _inputParameterToDateService;
        private readonly IParseToJsonService _parseToJsonService;
        private readonly ISumResultsService _sumResultsService; 

        public MonthlyResultHandler(IInputParameterToDateService inputParameterToDateService, IParseToJsonService parseToJsonService,
            ISumResultsService sumResultsService)
        {
            _inputParameterToDateService = inputParameterToDateService; 
            _parseToJsonService = parseToJsonService;
            _sumResultsService = sumResultsService; 
        }
        public override bool CanExecute() => true; 

        public override void Execute()
        {
            if (localPluginContext == null)
            {
                throw new InvalidOperationException(nameof(localPluginContext));
            }

            var inputs = localPluginContext.PluginExecutionContext.InputParameters;
            if (inputs.TryGetValue(pg_monthlyresultsRequest.Fields.startmonth, out var startMonth)
                && inputs.TryGetValue(pg_monthlyresultsRequest.Fields.endmonth, out var endMonth))
            {

                var startMonthDate = _inputParameterToDateService.GetDate(startMonth.ToString());
                var endMonthDate = _inputParameterToDateService.GetDate(endMonth.ToString());

                localPluginContext.TracingService.Trace($"Parsed input params: {startMonthDate} & {endMonthDate}");

                var monthlyResults = _sumResultsService.GetMonthlyResults(startMonthDate, endMonthDate, localPluginContext.PluginExecutionContext.InitiatingUserId);

                localPluginContext.TracingService.Trace($"Parsing {monthlyResults?.Count} monthly results...");
                var parsedMonthyResults = _parseToJsonService.Parse(monthlyResults);
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
