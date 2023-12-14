using Pg.LetsExercise.Plugins.Model;
using System;
using System.Collections.Generic;

namespace Pg.LetsExercise.Plugins.Infrastructure
{
    public interface IRepository
    {
        pg_exercisegoal GetGoal(Guid goalId);
        IList<pg_exerciserecord> GetCurrentDayRecords(DateTime now, Guid ownerId, pg_exerciseset exercise);

        IList<pg_exerciserecord> GetCurrentWeekRecords(DateTime now, Guid ownerId, pg_exerciseset exercise);

        IList<pg_exerciserecord> GetCurrentMonthRecords(DateTime now, Guid ownerId, pg_exerciseset exercise);

        IList<pg_exerciserecord> GetCurrentYearRecords(DateTime now, Guid ownerId, pg_exerciseset exercise);
    }
}
