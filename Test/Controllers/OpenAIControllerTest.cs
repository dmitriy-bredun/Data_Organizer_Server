using Data_Organizer_Server.Controllers;
using Data_Organizer_Server.DTOs;
using Data_Organizer_Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Controllers
{
    public class OpenAIControllerTest
    {
        private readonly Mock<IOpenAIService> openAIServiceMock = new Mock<IOpenAIService>();
        private readonly Mock<ILogger<OpenAIController>> loggerMock = new Mock<ILogger<OpenAIController>>();

        [Fact]
        public async Task GetSummaryAsync_Should_Return_BadRequest_When_Request_IsNull()
        {
            var openAIController = new OpenAIController(openAIServiceMock.Object, loggerMock.Object);
            var errorMsg = "Empty request or missing content!";
            var requestWithError = new SummaryRequestDTO()
            {
                Error = errorMsg
            };
            var expected = new BadRequestObjectResult(requestWithError);

            var result = await openAIController.GetSummaryAsync(null);

            Assert.Equivalent(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }
            
        [Fact]
        public async Task GetSummaryAsync_Should_Return_BadRequest_When_RequestContent_IsNullOrWhiteSpace()
        {
            var openAIController = new OpenAIController(openAIServiceMock.Object, loggerMock.Object);
            var errorMsg = "Empty request or missing content!";
            var requestWithError = new SummaryRequestDTO()
            {
                Error = errorMsg
            };
            var expected = new BadRequestObjectResult(requestWithError);
            var input = new SummaryRequestDTO();

            var result = await openAIController.GetSummaryAsync(input);

            Assert.Equivalent(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetSummaryAsync_Should_Return_Result()
        {
            var getSummaryResult = "SomeResult";
            var content = "someContent";
            openAIServiceMock.Setup(x => x.GetSummary(It.IsAny<string>()))
                .ReturnsAsync(getSummaryResult);
            var summaryDtoResult = new SummaryRequestDTO()
            {
                Content = content,
                Result = getSummaryResult
            };
            var expected = new OkObjectResult(summaryDtoResult);
            var openAIController = new OpenAIController(openAIServiceMock.Object, loggerMock.Object);
            var input = new SummaryRequestDTO()
            {
                Content = content
            };

            var result = await openAIController.GetSummaryAsync(input);

            openAIServiceMock.Verify(x => x.GetSummary(It.IsAny<string>()));
            Assert.Equivalent(expected.Value, ((OkObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((OkObjectResult)result).StatusCode);
        }
    }
}
