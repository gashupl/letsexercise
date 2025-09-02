using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Domain.Repositories;
using Pg.LetsExercise.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pg.LetsExercise.Domain.Services
{
    public class GoalCompletionService : ServiceBase, IGoalCompletionService
    {
        public GoalCompletionService(IRepository repository, ITracingService tracingService) : base(repository, tracingService)
        {
        }

        public int GetCompletionPercentage(Guid goalId)
        {
            tracingService.Trace($"GetCompletionPercentage executed with ID: {goalId}");

            var goal = repository.GetGoal(goalId);
            if(goal.pg_Exercise == null || goal.pg_scorenumber == null)
            {
                throw new InvalidPluginExecutionException("Not enough data to calculate goal completion");
            }

            IList<pg_exerciserecord> records = null;
            var now = DateTime.Now.ToUniversalTime(); 
            tracingService.Trace($"Date now: {now}");

            if(goal.pg_goaltype == pg_ExerciseGoalTypeSet.Daily)
            {
                records = repository.GetCurrentDayRecords(now, goal.OwnerId.Id, goal.pg_Exercise.Value);            
            }
            else if (goal.pg_goaltype == pg_ExerciseGoalTypeSet.Weekly)
            {
                records = repository.GetCurrentWeekRecords(now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else if(goal.pg_goaltype == pg_ExerciseGoalTypeSet.Monthly)
            {
                records = repository.GetCurrentMonthRecords(now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else if(goal.pg_goaltype == pg_ExerciseGoalTypeSet.Yearly)
            {
                records = repository.GetCurrentYearRecords(now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else
            {
                throw new InvalidPluginExecutionException("Invalid goal type");
            }

            tracingService.Trace($"Records count: {records.Count} for {pg_ExerciseGoalTypeSet.Daily} type");

            return GetPercentage(records, goal.pg_scorenumber.Value);
        }

        public int GetPercentage(IList<pg_exerciserecord> records, int expectedScore)
        {
            if(expectedScore <= 0)
            {
                throw new ArgumentException("Expected score must be greater than 0");
            }

            var completionSum = records.Sum(r => r.pg_scorenumber.Value);
            int percentage = (completionSum * 100 / expectedScore);
            return percentage;
        }
    }
}
