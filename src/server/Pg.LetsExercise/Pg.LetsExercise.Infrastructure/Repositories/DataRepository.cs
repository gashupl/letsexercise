using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Pg.LetsExercise.Domain.Repositories;

namespace Pg.LetsExercise.Infrastructure.Repositories
{
    public class DataRepository : IRepository
    {
        private readonly IOrganizationService _service;

        public DataRepository(IOrganizationServiceFactory orgSvcFactory)
        {
            _service = orgSvcFactory.CreateOrganizationService(null); 
        }

        public IList<pg_exerciserecord> GetCurrentDayRecords(DateTime now, Guid ownerId, pg_ExerciseSet exercise)
        {
            var entities = this._service.RetrieveMultiple(
                new QueryExpression(pg_exerciserecord.EntityLogicalName)
                    {
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression(pg_exerciserecord.Fields.OwnerId, ConditionOperator.Equal, ownerId),
                                new ConditionExpression(pg_exerciserecord.Fields.pg_exercise, ConditionOperator.Equal, (int)exercise),
                                new ConditionExpression(pg_exerciserecord.Fields.pg_date, ConditionOperator.On, now.Date)
                            }
                        }
                    }); 

            return entities.Entities.Select(e => e.ToEntity<pg_exerciserecord>()).ToList();
        }
        public IList<pg_exerciserecord> GetCurrentWeekRecords(DateTime now, Guid ownerId, pg_ExerciseSet exercise)
        {
            var startOfWeek = now.AddDays(-(int)now.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            using (var context = new DataverseContext(_service))
            {
                var query = context.pg_exerciserecordSet
                    .Where(e => e.OwnerId.Id == ownerId && e.pg_exercise == exercise 
                        && e.pg_date.Value >= startOfWeek && e.pg_date.Value <= endOfWeek)
                    .Select(ep => ep);

                return query.ToList<pg_exerciserecord>();
            }
        }

        public IList<pg_exerciserecord> GetCurrentMonthRecords(DateTime now, Guid ownerId, pg_ExerciseSet exercise)
        {
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            using (var context = new DataverseContext(_service))
            {
                var query = context.pg_exerciserecordSet
                    .Where(e => e.OwnerId.Id == ownerId && e.pg_exercise == exercise 
                        && e.pg_date.Value >= firstDayOfMonth && e.pg_date.Value <= lastDayOfMonth)
                    .Select(ep => ep);

                return query.ToList<pg_exerciserecord>();
            }
        }

        public IList<pg_exerciserecord> GetCurrentYearRecords(DateTime now, Guid ownerId, pg_ExerciseSet exercise)
        {
            DateTime firstDayOfYear = new DateTime(now.Year, 1, 1);
            DateTime lastDayOfYear = new DateTime(now.Year, 12, 31);

            using (var context = new DataverseContext(_service))
            {
                var query = context.pg_exerciserecordSet
                    .Where(e => e.OwnerId.Id == ownerId && e.pg_exercise == exercise 
                        && e.pg_date.Value >= firstDayOfYear && e.pg_date.Value <= lastDayOfYear)
                    .Select(ep => ep);

                return query.ToList<pg_exerciserecord>();
            }
        }

        public pg_exercisegoal GetGoal(Guid goalId)
        {
            try
            {
                var entity = _service.Retrieve(
                    pg_exercisegoal.EntityLogicalName, goalId, new ColumnSet(true));
                return entity.ToEntity<pg_exercisegoal>();

            }
            catch (Exception ex)
            {
                var message = ex.Message; 
                if (ex.InnerException != null)
                {
                    message += $" Inner Exception: {ex.InnerException.Message}";
                }
                throw new InvalidPluginExecutionException($"Cannot retrieve goal with ID {goalId}: {ex.Message}", ex); 
            }
        }
    }
}
