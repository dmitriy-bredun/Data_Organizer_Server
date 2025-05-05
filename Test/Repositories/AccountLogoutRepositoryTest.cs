using Data_Organizer_Server.Interfaces;
using Data_Organizer_Server.Repositories;
using Moq;

namespace Test.Repositories
{
    public class AccountLogoutRepositoryTest
    {
        private readonly Mock<ICollectionFactory> collectionFactoryMock = new Mock<ICollectionFactory>();

        [Fact]
        public async Task CreateAccountLogout_Should_Throw_Exception_When_AccountLogout_IsNull()
        {
            var repository = new AccountLogoutRepository(collectionFactoryMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                repository.CreateAccountLogoutAsync(null)
            );
        }
    }
}
