using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Domain.Repositories;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pg.LetsExercis.Domain.Services
{
    public class GoalCompletionService : IGoalCompletionService
    {
        private readonly IRepository _repository;
        private readonly ITracingService _tracingService;

        public GoalCompletionService(IRepository repository, ITracingService tracingService)
        {
            _repository = repository;
            _tracingService = tracingService;
        }

        public int GetCompletionPercentage(Guid goalId)
        {
            _tracingService.Trace($"GetCompletionPercentage executed with ID: {goalId}");

            var goal = _repository.GetGoal(goalId);
            if(goal.pg_Exercise == null || goal.pg_scorenumber == null)
            {
                throw new InvalidPluginExecutionException("Not enough data to calculate goal completion");
            }

            IList<pg_exerciserecord> records = null;
            var now = DateTime.Now.ToUniversalTime(); 
            _tracingService.Trace($"Date now: {now}");

            if(goal.pg_goaltype == pg_exercisegoaltypeset.Daily)
            {
                records 
                    = _repository.GetCurrentDayRecords(now, goal.OwnerId.Id, goal.pg_Exercise.Value);            
            }
            else if (goal.pg_goaltype == pg_exercisegoaltypeset.Weekly)
            {
                records
                    = _repository.GetCurrentWeekRecords(now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else if(goal.pg_goaltype == pg_exercisegoaltypeset.Monthly)
            {
                records
                    = _repository.GetCurrentMonthRecords(now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else if(goal.pg_goaltype == pg_exercisegoaltypeset.Yearly)
            {
                records
                    = _repository.GetCurrentYearRecords(now, goal.OwnerId.Id, goal.pg_Exercise.Value);
            }
            else
            {
                throw new InvalidPluginExecutionException("Invalid goal type");
            }

            _tracingService.Trace($"Records count: {records.Count} for {pg_exercisegoaltypeset.Daily} type");

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
