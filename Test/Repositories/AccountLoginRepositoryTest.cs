using Data_Organizer_Server.Entities;
using Data_Organizer_Server.Interfaces;
using Data_Organizer_Server.Repositories;
using Google.Cloud.Firestore;
using Moq;

namespace Test.Repositories
{
    public class AccountLoginRepositoryTest
    {
        private readonly Mock<ICollectionFactory> collectionFactoryMock = new Mock<ICollectionFactory>();

        [Fact]
        public async Task CreateAccountLogin_Should_Throw_Exception_When_AccountLogin_IsNull()

        {
            var repository = new AccountLoginRepository(collectionFactoryMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                repository.CreateAccountLoginAsync(null)
            );
        }
    }
}
