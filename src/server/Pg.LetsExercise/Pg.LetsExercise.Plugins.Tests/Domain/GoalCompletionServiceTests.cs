using Pg.LetsExercise.Plugins.Domain;
using Pg.LetsExercise.Plugins.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pg.LetsExercise.Plugins.Tests.Domain
{
    public class GoalCompletionServiceTests
    {
        [Fact]
        public void GetPercentage_ExpectedScoreIsZero_ThrowsArgumentException()
        {
            // Arrange
            var service = new GoalCompletionService(null);
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
            var service = new GoalCompletionService(null);
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
            var service = new GoalCompletionService(null);
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
            var service = new GoalCompletionService(null);
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
    }
}
