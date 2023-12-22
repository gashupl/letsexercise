using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Plugins.Infrastructure;
using Pg.LetsExercise.Plugins.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Pg.LetsExercise.Plugins.Tests.Infrastructure
{
    public class DataRepositoryTests
    {
        private readonly IOrganizationService _service;
        private readonly Guid _ownerId = Guid.NewGuid(); 
        public DataRepositoryTests()
        {
            _service = InitDataService();
        }
        
        [Fact]
        public void GetCurrentDayRecords_ReturnsList()
        {
            var repo = new DataRepository(_service); 
            var actual = repo.GetCurrentDayRecords(new DateTime(2023, 01, 01), _ownerId, pg_exerciseset.Pushups);
            Assert.Single(actual); 
        }

        [Fact]
        public void GetCurrentDayRecords_ReturnsEmptyList()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetCurrentDayRecords(new DateTime(2024, 01, 01), _ownerId, pg_exerciseset.Pushups);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCurrentWeekRecords_ReturnsList()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetCurrentMonthRecords_ReturnsList()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetCurrentYearRecords_ReturnsList()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetGoal_ReturnGoal()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetGoal_MissingGoal_ThrowsException()
        {
            throw new NotImplementedException();
        }

        public IOrganizationService InitDataService()
        {
            var context = new XrmFakedContext(); 
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(pg_exerciserecord));
    
            var record = new pg_exerciserecord
            {
                Id = Guid.NewGuid(),
                pg_date = new DateTime(2023, 1, 1),
                pg_exercise = pg_exerciseset.Pushups,
                OwnerId = new EntityReference(SystemUser.EntityLogicalName, _ownerId)
            }; 

            context.Initialize(new List<Entity>() { record });
            return context.GetFakedOrganizationService();

        }

    }
}
