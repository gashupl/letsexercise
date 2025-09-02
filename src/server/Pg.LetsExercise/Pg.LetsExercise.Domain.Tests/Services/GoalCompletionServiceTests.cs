using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsExercise.Domain.Repositories;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Domain.Tests.Core;
using Pg.LetsExercise.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pg.LetsExercise.Domain.Tests.Services
{
    public class GoalCompletionServiceTests : ServiceTestBase
    {

        [Fact]
        public void GetPercentage_ExpectedScoreIsZero_ThrowsArgumentException()
        {
            // Arrange
            var service = new GoalCompletionService(null, null);
            var records = new List<pg_exerciserecord>();
            var expectedScore = 0;

            // Act
            var exception = Assert.Throws<ArgumentException>(() => service.GetPercentage(records, expectedScore));

            // Assert
            Assert.Equal("Expected score must be greater than 0", exception.Message);
        }

        [Fact]
        public void GetPercentage_EmptyList_ReturnsZero()
        {
            // Arrange
            var service = new GoalCompletionService(null, tracingService);
            var records = new List<pg_exerciserecord>();
            var expectedScore = 100;
            var expected = 0; // 0%

            // Act
            var actual = service.GetPercentage(records, expectedScore);

            // Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetPercentage_ValidList_Returns25()
        {
            // Arrange
            var service = new GoalCompletionService(null, tracingService);
            var records = new List<pg_exerciserecord>() {
                new pg_exerciserecord { pg_scorenumber = 20 },
                new pg_exerciserecord { pg_scorenumber = 5 }
            };
            var expectedScore = 100;
            var expected = 25; // 25%

            // Act
            var actual = service.GetPercentage(records, expectedScore);

            // Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetPercentage_ValidList_Returns24()
        {
            // Arrange
            var service = new GoalCompletionService(null, tracingService);
            var records = new List<pg_exerciserecord>() {
                new pg_exerciserecord { pg_scorenumber = 20 },
                new pg_exerciserecord { pg_scorenumber = 5 }
            };
            var expectedScore = 101;
            var expected = 24; // 24%

            // Act
            var actual = service.GetPercentage(records, expectedScore);

            // Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetCompletionPercentage_DailyGoal_ReturnPercentage()
        {
            //Arrange
            var expected = 50; // 50%

            //Act
            var actual = GetActualPercentage(pg_ExerciseGoalTypeSet.Daily); 

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCompletionPercentage_WeeklyGoal_ReturnPercentage()
        {
            //Arrange
            var expected = 50; // 50%

            //Act
            var actual = GetActualPercentage(pg_ExerciseGoalTypeSet.Weekly);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetCompletionPercentage_MonthlyGoal_ReturnPercentage()
        {
            //Arrange
            var expected = 50; // 50%

            //Act
            var actual = GetActualPercentage(pg_ExerciseGoalTypeSet.Monthly);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetCompletionPercentage_AnnualGoal_ReturnPercentage()
        {
            //Arrange
            var expected = 50; // 50%

            //Act
            var actual = GetActualPercentage(pg_ExerciseGoalTypeSet.Yearly);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCompletionPercentage_MissingGoalParameters_ThrowsException()
        {
            //Arrange
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetGoal(It.IsAny<Guid>())).Returns(new pg_exercisegoal());
            var service = new GoalCompletionService(repo.Object, tracingService);

            //Act
            var exception = Assert.Throws<InvalidPluginExecutionException>(() => service.GetCompletionPercentage(Guid.NewGuid()));

            //Assert
            Assert.Equal("Not enough data to calculate goal completion", exception.Message);
        }

        private int GetActualPercentage(pg_ExerciseGoalTypeSet type)
        {
            var repo = GetMockedRepository(type);
            var service = new GoalCompletionService(repo, tracingService);
            return service.GetCompletionPercentage(Guid.NewGuid());
        }

        private IRepository GetMockedRepository(pg_ExerciseGoalTypeSet type)
        {

            var list = new List<pg_exerciserecord> { new pg_exerciserecord { pg_scorenumber = 5 } }; 

            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetGoal(It.IsAny<Guid>())).Returns(new pg_exercisegoal
            {
                pg_goaltype = type,
                pg_scorenumber = 10, 
                pg_Exercise = pg_ExerciseSet.PushUps, 
                OwnerId = new EntityReference { Id = Guid.NewGuid() }   
            });

            repo.Setup(r => r.GetCurrentDayRecords(
                It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<pg_ExerciseSet>()))
                .Returns(list);

            repo.Setup(r => r.GetCurrentWeekRecords(
                It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<pg_ExerciseSet>()))
                .Returns(list);

            repo.Setup(r => r.GetCurrentMonthRecords(
                It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<pg_ExerciseSet>()))
                .Returns(list);

            repo.Setup(r => r.GetCurrentYearRecords(
                It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<pg_ExerciseSet>()))
                .Returns(list);

            return repo.Object; 
        }
    }
}
