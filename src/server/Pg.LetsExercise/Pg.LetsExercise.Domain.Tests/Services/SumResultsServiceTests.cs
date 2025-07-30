using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsExercise.Domain.Repositories;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pg.LetsExercise.Domain.Tests.Services
{
    public class SumResultsServiceTests
    {
        private readonly SumResultsService _service;
        private readonly Guid _ownerId = Guid.NewGuid();

        public SumResultsServiceTests()
        {
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetSelectedMonthRecords(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<Model.pg_ExerciseSet>()))
                .Returns(new List<pg_exerciserecord>
                {
                    new pg_exerciserecord { pg_scorenumber = 10 },
                    new pg_exerciserecord { pg_scorenumber = 20 }
                });

            var _traceMock = new Mock<ITracingService>();
            _service = new SumResultsService(repoMock.Object, _traceMock.Object);
        }

        [Fact]
        public void GetMonthlyResults_StartDate3MonthsBeforeEndDate_Returns4Results()
        {
            // Arrange
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 4, 1);

            // Act
            var results = _service.GetMonthlyResults(startDate, endDate, _ownerId);

            // Assert
            Assert.Equal(4, results.Count);
            Assert.Equal("2024-01", results[0].MonthCodeName);
            Assert.Equal("2024-02", results[1].MonthCodeName);
            Assert.Equal("2024-03", results[2].MonthCodeName);
            Assert.Equal("2024-04", results[3].MonthCodeName);
            Assert.All(results, r => Assert.Equal(30, r.Result));
        }

        [Fact]
        public void GetMonthlyResults_StartDateSameAsEndDate_Returns1Result()
        {
            // Arrange
            var startDate = new DateTime(2024, 5, 1);
            var endDate = new DateTime(2024, 5, 20);

            // Act
            var results = _service.GetMonthlyResults(startDate, endDate, _ownerId);

            // Assert
            Assert.Single(results);
            Assert.Equal("2024-05", results[0].MonthCodeName);
            Assert.Equal(30, results[0].Result);
        }


        [Fact]
        public void GetMonthlyResults_StartDateAfterEndDate_ReturnsEmptyList()
        {
            // Arrange
            var startDate = new DateTime(2024, 6, 1);
            var endDate = new DateTime(2024, 5, 1);

            // Act
            var results = _service.GetMonthlyResults(startDate, endDate, _ownerId);

            // Assert
            Assert.Empty(results);
        }
    }
}
