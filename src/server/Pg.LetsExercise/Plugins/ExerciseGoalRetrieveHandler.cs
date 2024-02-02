using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Core;
using Pg.LetsExercise.Plugins.Model;
using System;

namespace Pg.LetsExercise.Plugins
{
    public class ExerciseGoalRetrieveHandler : PluginBase
    {
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
                    goal.pg_completedpercentage = GoalCompletionService.GetCompletionPercentage(goal.Id);
                }
            }
            else
            {
                localPluginContext.TracingService.Trace("Cannot read BusinessEntity output param or param is not an Entity");
            }

        }
    }
}
