using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Model;
using System;
using System.Collections.Generic;

namespace Pg.LetsExercise.Plugins.Infrastructure
{
    public class DataRepository : IRepository
    {
        private readonly IOrganizationService _service;

        public DataRepository(IOrganizationService service)
        {
            _service = service;
        }

        public IList<pg_exerciserecord> GetCurrentDayRecords(DateTime now, Guid ownerId, pg_exerciseset exercise)
        {
            throw new NotImplementedException();
        }
        public IList<pg_exerciserecord> GetCurrentWeekRecords(DateTime now, Guid ownerId, pg_exerciseset exercise)
        {
            throw new NotImplementedException();
        }

        public IList<pg_exerciserecord> GetCurrentMonthRecords(DateTime now, Guid ownerId, pg_exerciseset exercise)
        {
            throw new NotImplementedException();
        }

        public IList<pg_exerciserecord> GetCurrentYearRecords(DateTime now, Guid ownerId, pg_exerciseset exercise)
        {
            throw new NotImplementedException();
        }

        public pg_exercisegoal GetGoal(Guid goalId)
        {
            var entity = _service.Retrieve(
                pg_exercisegoal.EntityLogicalName, goalId, new ColumnSet(true));

            if (entity == null)
            {
                throw new InvalidPluginExecutionException("Goal not found");
            }
            else
            {
                return entity.ToEntity<pg_exercisegoal>();
            }
        }
    }
}
