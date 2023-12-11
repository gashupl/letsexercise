using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Core;
using Pg.LetsExercise.Plugins.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsExercise.Plugins
{
    internal class ExerciseGoalRetrieveMultipleHandler : PluginBase
    {
        public ExerciseGoalRetrieveMultipleHandler(Type pluginClassName) : base(pluginClassName)
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
    }
}
