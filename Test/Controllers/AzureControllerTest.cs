using Data_Organizer_Server.Controllers;
using Data_Organizer_Server.DTOs;
using Data_Organizer_Server.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Controllers
{
    public class AzureControllerTest
    {
        private readonly Mock<IAzureService> azureServiceMock = new Mock<IAzureService>();
        private readonly Mock<ILogger<AzureController>> loggerMock = new Mock<ILogger<AzureController>>();
        private const long MaxFileSize = 100 * 1024 * 1024;

        [Fact]
        public async Task TranscribeFromFileAsync_Should_Return_BadRequest_When_Request_IsNull()
        {
            var azureController = new AzureController(azureServiceMock.Object, loggerMock.Object);
            var expected = new BadRequestObjectResult("Please upload an audio file");

            var result = await azureController.TranscribeFromFileAsync(null);

            Assert.Equal(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);

        }

        [Fact]
        public async Task TranscribeFromFileAsync_Should_Return_BadRequest_When_RequestAudioFile_IsNull()
        {
            var azureController = new AzureController(azureServiceMock.Object, loggerMock.Object);
            var audioFile = new AudiofileDTO();
            var expected = new BadRequestObjectResult("Please upload an audio file");

            var result = await azureController.TranscribeFromFileAsync(audioFile);

            Assert.Equal(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task TranscribeFromFileAsync_Should_Return_BadRequest_When_Request_IsEmpty()
        {
            var azureController = new AzureController(azureServiceMock.Object, loggerMock.Object);
            var expected = new BadRequestObjectResult("Please upload an audio file");
            var stream = new MemoryStream();
            var audioFile = new AudiofileDTO()
            {
                AudioFile = new FormFile(stream, 0, stream.Length, "name", "fileName.mp3")
            };

            var result = await azureController.TranscribeFromFileAsync(audioFile);

            Assert.Equal(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task TranscribeFromFileAsync_Should_Return_BadRequest_When_MaxFileSize_Exceeded()
        {
            var azureController = new AzureController(azureServiceMock.Object, loggerMock.Object);
            var expected = new BadRequestObjectResult($"File size exceeds {MaxFileSize / 1024 / 1024}MB limit");
            var stream = new MemoryStream();
            var audioFile = new AudiofileDTO()
            {
                AudioFile = new FormFile(stream, 0, int.MaxValue, "name", "fileName.mp3")
            };

            var result = await azureController.TranscribeFromFileAsync(audioFile);

            Assert.Equal(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task TranscribeFromFileAsync_Should_Return_Result()
        {
            var resultString = "TranscribeFileResultString";
            azureServiceMock.Setup(x => x.TranscribeFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(resultString);
            var expected = new OkObjectResult(new { Transcription  = resultString });
            var azureController = new AzureController(azureServiceMock.Object, loggerMock.Object);
            var stream = new MemoryStream();
            var audioFile = new AudiofileDTO()
            {
                AudioFile = new FormFile(stream, 0, 1024, "name", "fileName.mp3")
            };

            var result = await azureController.TranscribeFromFileAsync(audioFile);

            azureServiceMock.Verify(x => x.TranscribeFileAsync(It.IsAny<string>(), It.IsAny<string>()));
            Assert.Equivalent(expected.Value, ((OkObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((OkObjectResult)result).StatusCode);
        }
    }
}