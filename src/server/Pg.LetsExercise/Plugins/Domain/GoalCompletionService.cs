using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Infrastructure;
using Pg.LetsExercise.Plugins.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pg.LetsExercise.Plugins.Domain
{
    public class GoalCompletionService : IGoalCompletionService
    {
        private readonly IRepository _repository;

        public GoalCompletionService(IRepository repository)
        {
            _repository = repository;
        }

        public int GetCompletionPercentage(Guid goalId)
        {
            var goal = _repository.GetGoal(goalId);
            if(goal.pg_Exercise == null || goal.pg_scorenumber == null)
            {
                throw new InvalidPluginExecutionException("Not enough data to calculate goal completion");
            }

            IList<pg_exerciserecord> records = null;
            if(goal.pg_goaltype == pg_exercisegoaltypeset.Daily)
            {
                records 
                    = _repository.GetCurrentDayRecords(DateTime.Now, goal.OwnerId.Id, goal.pg_Exercise.Value);            
            }
            else if (goal.pg_goaltype == pg_exercisegoaltypeset.Weekly)
            {
                records
                    = _repository.GetCurrentWeekRecords(DateTime.Now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else if(goal.pg_goaltype == pg_exercisegoaltypeset.Monthly)
            {
                records
                    = _repository.GetCurrentMonthRecords(DateTime.Now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else if(goal.pg_goaltype == pg_exercisegoaltypeset.Yearly)
            {
                records
                    = _repository.GetCurrentYearRecords(DateTime.Now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else
            {
                throw new InvalidPluginExecutionException("Invalid goal type");
            }

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
