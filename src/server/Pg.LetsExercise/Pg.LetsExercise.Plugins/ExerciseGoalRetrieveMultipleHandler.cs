using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Core;
using Pg.LetsExercise.Model;
using System;

namespace Pg.LetsExercise.Plugins
{
    internal class ExerciseGoalRetrieveMultipleHandler : PluginBase
    {
        public ExerciseGoalRetrieveMultipleHandler(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ExerciseGoalRetrieveMultipleHandler))
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

            if (context.Depth == 1 && 
                context.OutputParameters.Contains(OutputParameters.BusinessEntityCollection) && context.OutputParameters[OutputParameters.BusinessEntityCollection] is EntityCollection)
            {
                if (context.OutputParameters.Contains(OutputParameters.BusinessEntityCollection))
                {
                    var businessEntityCollection
                        = (EntityCollection)context.OutputParameters[OutputParameters.BusinessEntityCollection];

                    foreach (Entity entity in businessEntityCollection.Entities)
                    {
                        var goal = entity.ToEntity<pg_exercisegoal>();
                        goal.pg_completedpercentage = GoalCompletionService.GetCompletionPercentage(goal.Id);
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("Error");
                }
            }
            else
            {
                localPluginContext.TracingService.Trace("Cannot read BusinessEntityCollection output param or param is not an EntityCollection");
            }



        }
    }
}
