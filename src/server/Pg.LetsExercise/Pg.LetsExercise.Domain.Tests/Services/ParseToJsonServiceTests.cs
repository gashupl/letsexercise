using Newtonsoft.Json;
using Pg.LetsExercise.Domain.Dto;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Domain.Tests.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pg.LetsExercise.Domain.Tests.Services
{
    public class ParseToJsonServiceTests : ServiceTestBase
    {
        private readonly ParseToJsonService _service = new ParseToJsonService(null, null);

        [Fact]
        public void Parse_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Parse(null));
        }

        [Fact]
        public void Parse_EmptyList_ReturnsEmptyJsonArray()
        {
            var list = new List<MonthlyResult>();
            var json = _service.Parse(list);
            Assert.Equal("[]", json);
        }

        [Fact]
        public void Parse_FilledList_ReturnsExpectedJson()
        {
            var expected = "[{\"MonthCodeName\":\"Jan\",\"MonthFriendlyName\":\"January\",\"Result\":10},{\"MonthCodeName\":\"Feb\",\"MonthFriendlyName\":\"February\",\"Result\":20}]"; 
            var list = new List<MonthlyResult>
            {
                new MonthlyResult { MonthCodeName = "Jan", MonthFriendlyName = "January", Result = 10 },
                new MonthlyResult { MonthCodeName = "Feb", MonthFriendlyName = "February", Result = 20 }
            };

            var actual = _service.Parse(list);
           
            Assert.Equal(expected, actual);
        }
    }
}
