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
        private readonly Guid _goalId = Guid.NewGuid();

        public DataRepositoryTests()
        {
            _service = InitDataService();
        }
        
        [Fact]
        public void GetCurrentDayRecords_ReturnsSingleRecord()
        {
            var repo = new DataRepository(_service); 
            var actual = repo.GetCurrentDayRecords(new DateTime(2024, 01, 01), _ownerId, pg_exerciseset.Pushups);
            Assert.Single(actual); 
        }

        [Fact]
        public void GetCurrentDayRecords_ReturnsEmptyList()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetCurrentDayRecords(new DateTime(2023, 01, 01, 11, 22,33), _ownerId, pg_exerciseset.Pushups);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCurrentWeekRecords_ReturnsSingleRecord()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetCurrentWeekRecords(new DateTime(2024, 01, 02), _ownerId, pg_exerciseset.Pushups);
            Assert.Single(actual);
        }

        [Fact]
        public void GetCurrentWeekRecords_ReturnsEmptyList()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetCurrentWeekRecords(new DateTime(2024, 01, 8), _ownerId, pg_exerciseset.Pushups);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCurrentMonthRecords_ReturnsSingleRecord()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetCurrentMonthRecords(new DateTime(2024, 01, 8), _ownerId, pg_exerciseset.Pushups);
            Assert.Single(actual);
        }

        [Fact]
        public void GetCurrentMonthRecords_ReturnsEmptyList()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetCurrentMonthRecords(new DateTime(2024, 02, 01), _ownerId, pg_exerciseset.Pushups);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCurrentYearRecords_ReturnsSingleRecord()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetCurrentYearRecords(new DateTime(2024, 06, 8), _ownerId, pg_exerciseset.Pushups);
            Assert.Single(actual);
        }

        [Fact]
        public void GetCurrentYearRecords_ReturnsEmptyList()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetCurrentYearRecords(new DateTime(2025, 01, 01), _ownerId, pg_exerciseset.Pushups);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetGoal_ReturnGoal()
        {
            var repo = new DataRepository(_service);
            var actual = repo.GetGoal(_goalId); 
            Assert.Equal(_goalId, actual.Id);
        }

        [Fact]
        public void GetGoal_MissingGoal_ThrowsException()
        {
            var repo = new DataRepository(_service);
            Assert.Throws<InvalidPluginExecutionException>(() => repo.GetGoal(Guid.NewGuid()));
        }

        public IOrganizationService InitDataService()
        {
            var context = new XrmFakedContext(); 
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(pg_exerciserecord));
    
            var record = new pg_exerciserecord
            {
                Id = Guid.NewGuid(),
                pg_date = new DateTime(2024, 1, 1),
                pg_exercise = pg_exerciseset.Pushups,
                OwnerId = new EntityReference(SystemUser.EntityLogicalName, _ownerId)
            }; 

            var goal = new pg_exercisegoal
            {
                Id = _goalId,
                pg_Exercise = pg_exerciseset.Pushups,
                pg_goaltype = pg_exercisegoaltypeset.Daily,
                pg_scorenumber = 10,
                OwnerId = new EntityReference(SystemUser.EntityLogicalName, _ownerId)
            };

            context.Initialize(new List<Entity>() { record, goal });
            return context.GetFakedOrganizationService();

        }

    }
}
