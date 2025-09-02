using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Core;
using Pg.LetsExercise.Model;
using System;
using Pg.LetsExercise.Domain.Services;

namespace Pg.LetsExercise.Plugins
{

    public class ExerciseGoalRetrieveMultipleDependencyLoader : DependencyLoaderBase
    {
        public ExerciseGoalRetrieveMultipleDependencyLoader()
        {
            Register<IGoalCompletionService, GoalCompletionService>();
        }
    }
    public class ExerciseGoalRetrieveMultiplePlugin : PluginBase<ExerciseGoalRetrieveMultipleHandler>
    {
        public override IDependencyLoader DependencyLoader => new ExerciseGoalRetrieveMultipleDependencyLoader();

        public ExerciseGoalRetrieveMultiplePlugin(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ExerciseGoalRetrieveMultiplePlugin))
        {
        }
    }

    public class ExerciseGoalRetrieveMultipleHandler : PluginHandlerBase
    {
        private readonly IGoalCompletionService _goalCompletionService;
        public ExerciseGoalRetrieveMultipleHandler(IGoalCompletionService goalCompletionService)
        {
            _goalCompletionService = goalCompletionService ?? throw new ArgumentNullException(nameof(goalCompletionService));
        }


        public override bool CanExecute() => true; 

        public override void Execute()
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

                    localPluginContext.TracingService.Trace("Get IGoalCompletionService service");

                    foreach (Entity entity in businessEntityCollection.Entities)
                    {
                        var goal = entity.ToEntity<pg_exercisegoal>();
                        localPluginContext.TracingService.Trace($"Set completed percentage for {goal.Id}");
                        goal.pg_completedpercentage = _goalCompletionService.GetCompletionPercentage(goal.Id);
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
