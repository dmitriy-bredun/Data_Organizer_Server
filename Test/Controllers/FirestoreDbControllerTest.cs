using Data_Organizer_Server.Controllers;
using Data_Organizer_Server.DTOs;
using Data_Organizer_Server.Entities;
using Data_Organizer_Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Controllers
{
    public class FirestoreDbControllerTest
    {
        private readonly Mock<IFirestoreDbService> dbServiceMock = new Mock<IFirestoreDbService>();
        private readonly Mock<ILogger<FirestoreDbController>> loggerMock = new Mock<ILogger<FirestoreDbController>>();

        [Fact]
        public async Task CreateUserAsync_Should_Return_BadRequest_When_UserCreationRequest_IsNull()
        {
            var firestoreDbController = new FirestoreDbController(dbServiceMock.Object, loggerMock.Object);
            var errorMsg = "Empty request or missing user data!";
            var badUserRequest = new UserRequestDTO() { Error = errorMsg };
            var expected = new BadRequestObjectResult(badUserRequest);

            var result = await firestoreDbController.CreateUserAsync(null);

            Assert.Equivalent(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task CreateUserAsync_Should_Return_BadRequest_When_UserDTO_IsNull()
        {
            var firestoreDbController = new FirestoreDbController(dbServiceMock.Object, loggerMock.Object);
            var errorMsg = "Empty request or missing user data!";
            var badUserRequest = new UserRequestDTO() { Error = errorMsg };
            var expected = new BadRequestObjectResult(badUserRequest);
            var userRequestDto = new UserRequestDTO();

            var result = await firestoreDbController.CreateUserAsync(userRequestDto);

            Assert.Equivalent(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task CreateUserAsync_Should_Return_Result()
        {
            var returnedUserRequestDto = new UserRequestDTO()
            {
                UserDTO = new UserDTO() { Uid = "testUserId" },
                UsersMetadataDTO = new UsersMetadataDTO(),
                CreationDevice = new DeviceInfoModel(),
                DeletionDevice = new DeviceInfoModel(),
                Error = string.Empty
            };
            dbServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<UserRequestDTO>()))
                .ReturnsAsync(returnedUserRequestDto);
            var expected = new OkObjectResult(returnedUserRequestDto);
            var firestoreDbController = new FirestoreDbController(dbServiceMock.Object, loggerMock.Object);
            var inputUserData = new UserRequestDTO()
            {
                UserDTO = new UserDTO() { Uid = "testUserId" }
            };

            var result = await firestoreDbController.CreateUserAsync(inputUserData);

            dbServiceMock.Verify(x => x.CreateUserAsync(It.IsAny<UserRequestDTO>()));
            Assert.Equivalent(expected.Value, ((OkObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetUserMetadataAsync_Should_Return_BadRequest_When_Request_IsNull()
        {
            var firestoreDbController = new FirestoreDbController(dbServiceMock.Object, loggerMock.Object);
            var errorMsg = "Request body is required.";
            var badUserRequest = new UsersMetadataDTO() { Error = errorMsg };
            var expected = new BadRequestObjectResult(badUserRequest);

            var result = await firestoreDbController.GetUserMetadataAsync(null);

            Assert.Equivalent(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetUserMetadataAsync_Should_Return_BadRequest_When_Uid_IsNull()
        {
            var firestoreDbController = new FirestoreDbController(dbServiceMock.Object, loggerMock.Object);
            var errorMsg = "UID is required to retrieve metadata.";
            var badUserRequest = new UsersMetadataDTO() { Error = errorMsg };
            var expected = new BadRequestObjectResult(badUserRequest);
            var request = new UsersMetadataDTO();

            var result = await firestoreDbController.GetUserMetadataAsync(request);

            Assert.Equivalent(expected.Value, ((BadRequestObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetUserMetadataAsync_Should_Return_Result()
        {
            var currentTime = DateTime.UtcNow;
            var returnedUserMetadataDto = new UsersMetadataDTO()
            {
                Uid = "TestUserId",
                CreationDate = currentTime
            };
            dbServiceMock.Setup(x => x.GetUserMetadataAsync(It.IsAny<UsersMetadataDTO>()))
                .ReturnsAsync(returnedUserMetadataDto);
            var expected = new OkObjectResult(returnedUserMetadataDto);
            var firestoreDbController = new FirestoreDbController(dbServiceMock.Object, loggerMock.Object);
            var inputUserMetadata = new UsersMetadataDTO()
            {
                Uid = "TestUserId",
                CreationDate = currentTime
            };

            var result = await firestoreDbController.GetUserMetadataAsync(inputUserMetadata);

            dbServiceMock.Verify(x => x.GetUserMetadataAsync(It.IsAny<UsersMetadataDTO>()));
            Assert.Equivalent(expected.Value, ((OkObjectResult)result).Value);
            Assert.Equal(expected.StatusCode, ((OkObjectResult)result).StatusCode);
        }
    }
}
