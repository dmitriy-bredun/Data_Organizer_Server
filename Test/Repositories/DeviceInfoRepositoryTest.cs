using Data_Organizer_Server.Interfaces;
using Data_Organizer_Server.Repositories;
using Moq;

namespace Test.Repositories
{
    public class DeviceInfoRepositoryTest
    {
        private readonly Mock<ICollectionFactory> collectionFactoryMock = new Mock<ICollectionFactory>();

        [Fact]
        public async Task CreateDevice_Shold_Throw_Exception_When_Device_IsNull()
        {
            var repository = new DeviceInfoRepository(collectionFactoryMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                repository.CreateDeviceAsync(null)
            );
        }

        [Fact]
        public async Task GetDeviceDocRefByCombinedInfo_Shold_Throw_Exception_When_Device_IsNull()
        {
            var repository = new DeviceInfoRepository(collectionFactoryMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                repository.GetDeviceDocRefByCombinedInfo(null)
            );
        }
    }
}
