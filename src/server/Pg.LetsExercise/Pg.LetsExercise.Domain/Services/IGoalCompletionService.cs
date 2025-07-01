using Pg.LetsExercise.Model;
using System;
using System.Collections.Generic;

namespace Pg.LetsExercise.Domain.Services
{
    public interface IGoalCompletionService
    {
        int GetCompletionPercentage(Guid goalId);
        int GetPercentage(IList<pg_exerciserecord> records, int expectedScore);
    }
}
