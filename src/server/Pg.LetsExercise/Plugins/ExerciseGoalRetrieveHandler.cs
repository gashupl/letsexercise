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
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            var outputEntity = (Entity)context.OutputParameters[OutputParameters.BusinessEntity];

            if(outputEntity != null)
            {
                var goal = outputEntity.ToEntity<pg_exercisegoal>();
                goal.pg_completedpercentage = GoalCompletionService.GetCompletionPercentage(goal.Id);
            }

        }
    }
}
