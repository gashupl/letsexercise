using Pg.LetsExercise.Domain.Services;
using System;
using Xunit;

namespace Pg.LetsExercise.Domain.Tests.Services
{
    public class InputParameterToDateServiceTests
    {
        private readonly IInputParameterToDateService _service;

        public InputParameterToDateServiceTests()
        {
            _service = new InputParameterToDateService();
        }

        [Theory]
        [InlineData("2025-07-28", 2025, 7, 28)]
        [InlineData("01/02/2024", 2024, 2, 1)]
        [InlineData("2024/12/31", 2024, 12, 31)]
        public void GetDate_ValidString_ReturnsExpectedDate(string input, int year, int month, int day)
        {
            var result = _service.GetDate(input);
            Assert.Equal(new DateTime(year, month, day), result.Date);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("not-a-date")]
        [InlineData("2024-13-01")] // Invalid month
        [InlineData("2024-02-30")] // Invalid day
        public void GetDate_InvalidString_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => _service.GetDate(input));
        }
    }
}
