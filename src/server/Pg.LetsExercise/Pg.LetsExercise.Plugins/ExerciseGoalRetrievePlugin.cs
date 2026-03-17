using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Core;
using Pg.LetsExercise.Model;
using System;
using Pg.LetsExercise.Domain.Services;
using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace Pg.LetsExercise.Plugins
{
    public class ExerciseGoalRetrieveDependencyLoader : DependencyLoaderBase
    {
        public ExerciseGoalRetrieveDependencyLoader()
        {
            Register<IGoalCompletionService, GoalCompletionService>();
        }
    }

    public class ExerciseGoalRetrievePlugin : PluginBase<ExerciseGoalRetrieveHandler>
    {

        public override IDependencyLoader DependencyLoader => new ExerciseGoalRetrieveDependencyLoader();

        public ExerciseGoalRetrievePlugin(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ExerciseGoalRetrievePlugin))
        {
        }
    }

    public class ExerciseGoalRetrieveHandler : PluginHandlerBase
    {
        private readonly IGoalCompletionService _goalCompletionService; 
        public ExerciseGoalRetrieveHandler(IGoalCompletionService goalCompletionService)
        {
            _goalCompletionService = goalCompletionService ?? throw new ArgumentNullException(nameof(goalCompletionService));
        }

        public override bool CanExecute() => true; 

        public override void Execute()
        {
            if (localPluginContext == null)
            {
                localPluginContext.Trace(LogLevel.Error, "LocalPluginContext is null.");
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            if (context.Depth == 1 && context.OutputParameters.Contains(OutputParameters.BusinessEntity) && context.OutputParameters[OutputParameters.BusinessEntity] is Entity)
            {
                var outputEntity = (Entity)context.OutputParameters[OutputParameters.BusinessEntity];

                if (outputEntity != null)
                {
                    var goal = outputEntity.ToEntity<pg_exercisegoal>();

                    var goalCompletionService = _goalCompletionService; 
                    goal.pg_completedpercentage = goalCompletionService.GetCompletionPercentage(goal.Id);
                }
            }
            else
            {
                localPluginContext.Trace(LogLevel.Error, 
                    "Invalid conditions for ExerciseGoalRetrieveHandler logic execution");
            }
        }
    }
}
