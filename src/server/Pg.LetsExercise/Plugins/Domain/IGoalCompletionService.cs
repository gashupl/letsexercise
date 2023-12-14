using Pg.LetsExercise.Plugins.Model;
using System;
using System.Collections.Generic;

namespace Pg.LetsExercise.Plugins.Domain
{
    public interface IGoalCompletionService
    {
        int GetCompletionPercentage(Guid goalId);
        int GetPercentage(IList<pg_exerciserecord> records, int expectedScore);
    }
}
