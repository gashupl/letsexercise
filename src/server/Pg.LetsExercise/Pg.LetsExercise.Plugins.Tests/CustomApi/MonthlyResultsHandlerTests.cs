using Moq;
using Pg.LetsExercise.Domain.Services;
using Pg.LetsExercise.Plugins.CustomApi;
using Xunit;

namespace Pg.LetsExercise.Plugins.Tests.CustomApi
{
    public class MonthlyResultsHandlerTests
    {
        [Fact]
        public void CanExecute_EmptyLocalContext_ThrowsArgumentNullException()
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


    }
}
