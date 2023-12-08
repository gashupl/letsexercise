using Pg.LetsExercise.Plugins.Model;
using System;

namespace Pg.LetsExercise.Plugins.Domain
{
    public class GoalCompletionService : IGoalCompletionService
    {
        public int GetAnnualCompletionPercentage(Guid ownerId, int expectedValue, pg_exercisegoaltypeset type)
        {
            throw new NotImplementedException();
        }

        public int GetDailyCompletionPercentage(Guid ownerId, int expectedValue, pg_exercisegoaltypeset type)
        {
            throw new NotImplementedException();
        }

        public int GetMonthlyCompletionPercentage(Guid ownerId, int expectedValue, pg_exercisegoaltypeset type)
        {
            throw new NotImplementedException();
        }
    }
}
