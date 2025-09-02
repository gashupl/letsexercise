using Pg.LetsExercise.Model;
using System;
using System.Collections.Generic;

namespace Pg.LetsExercise.Domain.Repositories
{
    public interface IRepository
    {
        pg_exercisegoal GetGoal(Guid goalId);
        IList<pg_exerciserecord> GetCurrentDayRecords(DateTime now, Guid ownerId, pg_ExerciseSet exercise);

        IList<pg_exerciserecord> GetCurrentWeekRecords(DateTime now, Guid ownerId, pg_ExerciseSet exercise);

        IList<pg_exerciserecord> GetCurrentMonthRecords(DateTime now, Guid ownerId, pg_ExerciseSet exercise);

        IList<pg_exerciserecord> GetCurrentYearRecords(DateTime now, Guid ownerId, pg_ExerciseSet exercise);

        IList<pg_exerciserecord> GetSelectedMonthRecords(int year, int month, Guid ownerId, pg_ExerciseSet exercise);
    }
}
