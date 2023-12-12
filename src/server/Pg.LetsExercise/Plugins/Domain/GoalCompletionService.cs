using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Pg.LetsExercise.Plugins.Model;
using System;

namespace Pg.LetsExercise.Plugins.Domain
{
    public class GoalCompletionService : IGoalCompletionService
    {
        private readonly IOrganizationService _service;

        public GoalCompletionService(IOrganizationService service)
        {
            _service = service;
        }

        public int GetCompletionPercentage(Guid goalId)
        {
            var entity = _service.Retrieve(
                pg_exercisegoal.EntityLogicalName, goalId, new ColumnSet(true));

            if(entity == null)
            {
                throw new InvalidPluginExecutionException("Goal not found");
            }
            else
            {
                var goal = entity.ToEntity<pg_exercisegoal>();
                if(goal.pg_goaltype == pg_exercisegoaltypeset.Daily)
                {
                    return GetDailyCompletionPercentage(goal.OwnerId.Id, goal.pg_scorenumber, goal.pg_Exercise);
                }
                else if(goal.pg_goaltype == pg_exercisegoaltypeset.Monthly)
                {
                    return GetMonthlyCompletionPercentage(goal.OwnerId.Id, goal.pg_scorenumber, goal.pg_Exercise);
                }
                else if(goal.pg_goaltype == pg_exercisegoaltypeset.Yearly)
                {
                    return GetAnnualCompletionPercentage(goal.OwnerId.Id, goal.pg_scorenumber, goal.pg_Exercise);
                }
                else
                {
                    throw new InvalidPluginExecutionException("Invalid goal type");
                }
            }

        }

        public int GetAnnualCompletionPercentage(Guid ownerId, int? expectedValue, pg_exerciseset?  exercise)
        {
            throw new NotImplementedException();
        }

        public int GetDailyCompletionPercentage(Guid ownerId, int? expectedValue, pg_exerciseset?  exercise)
        {
            throw new NotImplementedException();
        }

        public int GetMonthlyCompletionPercentage(Guid ownerId, int? expectedValue, pg_exerciseset?  exercise)
        {
            throw new NotImplementedException();
        }
    }
}
