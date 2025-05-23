using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Core;
using Pg.LetsExercise.Model;
using System;
using Pg.LetsExercise.Domain.Services;

namespace Pg.LetsExercise.Plugins
{
    public  class ExerciseGoalRetrieveDependencyLoader: DependencyLoaderBase
    {
        public ExerciseGoalRetrieveDependencyLoader()
        {
            Register<IGoalCompletionService, GoalCompletionService>();
        }
    }

    public class ExerciseGoalRetrieveHandler : PluginBase
    {

        public override IDependencyLoader DependencyLoader => new ExerciseGoalRetrieveDependencyLoader();

        public ExerciseGoalRetrieveHandler(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ExerciseGoalRetrieveHandler))
        {
        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                localPluginContext.TracingService.Trace("LocalPluginContext is null.");
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            if(context.Depth == 1 && context.OutputParameters.Contains(OutputParameters.BusinessEntity) && context.OutputParameters[OutputParameters.BusinessEntity] is Entity)
            {
                var outputEntity = (Entity)context.OutputParameters[OutputParameters.BusinessEntity];

                if (outputEntity != null)
                {
                    var goal = outputEntity.ToEntity<pg_exercisegoal>();

                    var goalCompletionService = DependencyLoader.Get<IGoalCompletionService>();
                    goal.pg_completedpercentage = goalCompletionService.GetCompletionPercentage(goal.Id);
                }
            }
            else
            {
                localPluginContext.TracingService.Trace("Cannot read BusinessEntity output param or param is not an Entity");
            }

        }
    }
}
