using Pg.LetsExercise.Plugins.Model;
using System;

namespace Pg.LetsExercise.Plugins.Domain
{
    public interface IGoalCompletionService
    {
        int GetCompletionPercentage(Guid goalId);
        int GetDailyCompletionPercentage(Guid ownerId, int expectedValue, pg_exercisegoaltypeset type);
        int GetMonthlyCompletionPercentage(Guid ownerId, int expectedValue, pg_exercisegoaltypeset type);
        int GetAnnualCompletionPercentage(Guid ownerId, int expectedValue, pg_exercisegoaltypeset type);
    }
}
