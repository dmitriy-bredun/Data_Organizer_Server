using Data_Organizer_Server.Interfaces;
using Data_Organizer_Server.Repositories;
using Moq;

namespace Test.Repositories
{
    public class ChangePasswordRepositoryTest
    {
        private readonly Mock<ICollectionFactory> collectionFactoryMock = new Mock<ICollectionFactory>();

        [Fact]
        public async Task CreateChangePassword_Should_Throw_Exception_When_ChangePassword_IsNull()
        {
            var repository = new ChangePasswordRepository(collectionFactoryMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                repository.CreateChangePasswordAsync(null)
            );
        }
    }
}
