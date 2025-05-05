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
        private readonly Mock<CollectionReference> accountLoginCollectionMock = new Mock<CollectionReference>();


        public AccountLoginRepositoryTest()
        {

        }

        [Fact]
        public async Task CreateAccountLogin_Should_Throw_Exception_When_AccountLogin_IsNull()
        {
            var repository = new AccountLoginRepository(collectionFactoryMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                repository.CreateAccountLoginAsync(null)
            );
        }

        [Fact]
        public async Task CreateAccountLoginAsync_Should_Call_AddAsyncMethod_When_AccountLogin_InNotNull()
        {
            var documentReferenceResultMock = new Mock<DocumentReference>();
            accountLoginCollectionMock.Setup(x => x.AddAsync(It.IsAny<AccountLogin>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(documentReferenceResultMock.Object);
            collectionFactoryMock.Setup(x => x.GetAccountLoginCollection()).Returns(accountLoginCollectionMock.Object);
            var repository = new AccountLoginRepository(collectionFactoryMock.Object);
            var accountLogin = new AccountLogin();

            var result = await repository.CreateAccountLoginAsync(accountLogin);

            Assert.Equal(documentReferenceResultMock.Object, result);
        }


    }
}
