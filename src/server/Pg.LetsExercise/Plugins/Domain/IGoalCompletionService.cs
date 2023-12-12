using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Model;
using System;

namespace Pg.LetsExercise.Plugins.Domain
{
    public interface IGoalCompletionService
    {
        int GetCompletionPercentage(Guid goalId);
        int GetDailyCompletionPercentage(Guid ownerId, int? expectedValue, pg_exerciseset? exercise);
        int GetMonthlyCompletionPercentage(Guid ownerId, int? expectedValue, pg_exerciseset? exercise);
        int GetAnnualCompletionPercentage(Guid ownerId, int? expectedValue, pg_exerciseset? exercise);
    }
}
