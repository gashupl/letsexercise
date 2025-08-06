using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsExercise.Domain.Dto;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Model;
using Pg.LetsExercise.Plugins.CustomApi;
using Pg.LetsExercise.Plugins.Tests.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pg.LetsExercise.Plugins.Tests.CustomApi
{
    public class MonthlyResultsHandlerTests : PluginHandlerTestBase
    {
        [Fact]
        public void CanExecute_EmptyLocalContext_ReturnsTrue()
        {
            // Arrange
            var inputToDateServiceMock = new Mock<IInputParameterToDateService>();
            var parseToJsonServiceMock = new Mock<IParseToJsonService>();
            var sumResultsServiceMock = new Mock<ISumResultsService>();

            var handler = new MonthlyResultHandler
                (inputToDateServiceMock.Object, parseToJsonServiceMock.Object, sumResultsServiceMock.Object);
            // Act & Assert
            Assert.True(handler.CanExecute()); 
        }

        [Fact]
        public void Execute_EmptyLocalContext_ThrowsInvalidOperationException()
        {
            // Arrange
            var inputToDateServiceMock = new Mock<IInputParameterToDateService>();
            var parseToJsonServiceMock = new Mock<IParseToJsonService>();
            var sumResultsServiceMock = new Mock<ISumResultsService>();

            var handler = new MonthlyResultHandler
                (inputToDateServiceMock.Object, parseToJsonServiceMock.Object, sumResultsServiceMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => handler.Execute());

        }

        [Fact]
        public void Execute_MissingInputParameters_ThrowsInvalidPluginOperationException()
        {
            // Arrange
            var inputToDateServiceMock = new Mock<IInputParameterToDateService>();
            var parseToJsonServiceMock = new Mock<IParseToJsonService>();
            var sumResultsServiceMock = new Mock<ISumResultsService>();

            var handler = new MonthlyResultHandler
                (inputToDateServiceMock.Object, parseToJsonServiceMock.Object, sumResultsServiceMock.Object);
            handler.Init(CreateLocalPluginContext());
            
            // Act & Assert
            Assert.Throws<InvalidPluginExecutionException>(() => handler.Execute());

        }

        [Fact]
        public void Execute_ValidInputParameters_SetOutputResponse()
        {
            // Arrange

            var expectedResult = "{}"; 
            var inputToDateServiceMock = new Mock<IInputParameterToDateService>();
            inputToDateServiceMock.Setup(s => s.GetDate(It.IsAny<string>()))
                .Returns((string date) => DateTime.Parse(date));

            var sumResultsServiceMock = new Mock<ISumResultsService>();
            sumResultsServiceMock.Setup(s => s.GetMonthlyResults(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>()))
                .Returns(new List<MonthlyResult> { new MonthlyResult { } });
            var parseToJsonServiceMock = new Mock<IParseToJsonService>();

            parseToJsonServiceMock.Setup(s => s.Parse(It.IsAny<object>()))
                .Returns(expectedResult);

            var inputParameters = new ParameterCollection
            {
                { pg_monthlyresultsRequest.Fields.startmonth, "2023-01-01" },
                { pg_monthlyresultsRequest.Fields.endmonth, "2023-12-31" }
            };  

            var localContext = CreateLocalPluginContext(inputParameters);
            var handler = new MonthlyResultHandler
                (inputToDateServiceMock.Object, parseToJsonServiceMock.Object, sumResultsServiceMock.Object);
                handler.Init(localContext);

            // Act 
            handler.Execute();

            //Assert
            var isResponseSet = localContext.PluginExecutionContext.OutputParameters.TryGetValue(pg_monthlyresultsResponse.Fields.Results_1, out var output);
            Assert.True(isResponseSet);
            Assert.Equal(expectedResult, output);

        }


    }
}
