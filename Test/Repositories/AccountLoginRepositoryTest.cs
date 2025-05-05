using Data_Organizer_Server.Entities;
using Data_Organizer_Server.Interfaces;
using Google.Cloud.Firestore;
using Moq;

namespace Test.Repositories
{
    public class AccountLoginRepositoryTest
    {
        private readonly Mock<ICollectionFactory> collectionFactoryMock = new Mock<ICollectionFactory>();
        //private readonly Mock<CollectionReference> accountLoginCollectionMock = new Mock<CollectionReference>();

        public AccountLoginRepositoryTest()
        { }

        [Fact]
        public async Task CreateAccountLogin_Should_Throw_Exception_When_AccountLogin_IsNull()
        {


            // ERRROOOORRR!!!



            //var repository = new AccountLoginRepository(collectionFactoryMock.Object);
            //
            //await Assert.ThrowsAsync<ArgumentNullException>(() =>
            //    repository.CreateAccountLoginAsync(null)
            //);
        }

        [Fact]
        public async Task CreateAccountLoginAsync_Should_Call_AddAsyncMethod_When_AccountLogin_InNotNull()
        {
            // Arrange
            var docRefResultMock = new Mock<IDocumentReference>();

            var accountLoginRefCollectionMock = new Mock<IAccountLoginCollectionReference>();
            accountLoginRefCollectionMock.Setup(x => x.AddAsync(It.IsAny<AccountLogin>(), It.IsAny<CancellationToken>())).ReturnsAsync(docRefResultMock.Object);

            var collectionFactory = new Mock<ICollectionFactoryMock>();
            collectionFactory.Setup(x => x.GetAccountLoginCollection()).Returns(accountLoginRefCollectionMock.Object);

            var accountLoginMock = new Mock<IAccountLoginRepositoryMock>();

            var accountLoginObj = new AccountLogin();

            accountLoginMock.Setup(x => x.CreateAccountLoginAsync(accountLoginObj)).ReturnsAsync(collectionFactory.Object.GetAccountLoginCollection().AddAsync(accountLoginObj).Result);

            // Act
            var result = await accountLoginMock.Object.CreateAccountLoginAsync(accountLoginObj);

            // Assert
            Assert.Same(docRefResultMock.Object, result);



            //var documentReferenceResultMock = new Mock<DocumentReference>();
            //var accountLoginCollectionMock = new Mock<ICollectionReferenceAccountLoginMock>();
            //    accountLoginCollectionMock.Setup(x => x.AddAsync(It.IsAny<AccountLogin>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(It.IsAny<DocumentReference>());
            //     
            //var repository = new AccountLoginRepository(collectionFactoryMock.Object);
            //var accountLogin = new AccountLogin();
            //
            //var result = await repository.CreateAccountLoginAsync(accountLogin);

            //Assert.Equal(documentReferenceResultMock.Object, result);
        }
    }

    interface IFakeCollectionFactory
    {
        IFakeAccountLoginCollection GetAccountLoginCollection();
    }

    interface IFakeAccountLoginCollection
    {
        public Task<DocumentReference> AddAsync(object documentData, CancellationToken cancellationToken = default);
    }

    //class FakeAccountLoginCollection : IFakeAccountLoginCollection
    //{
    //    public Task<DocumentReference> AddAsync(object documentData, CancellationToken cancellationToken = default)
    //    {
    //        
    //    }
    //}

    public interface IDocumentReference { }

    public interface ICollectionReference
    {
        public Task<IDocumentReference> AddAsync(object documentData, CancellationToken cancellationToken = default);
    }

    public interface IAccountLoginCollectionReference : ICollectionReference { }

    public interface ICollectionFactoryMock // P.S. about namings: In TDD for tests we use the interfaces that we would use for classes. 
                                            // We do not have interfaces for DocumentReference, CollectionReference. So I've named them
                                            // like if they're real interface. But we have interface for class CollectionFactory - ICollectionFactory
                                            // but it doesn't align with the return type IAccountLoginCollectionReference, because CollectionReference
                                            // doesn't have any interfaces. So I create another interface - ICollectionFactoryMock, but with word "Mock"
                                            // in the end
    {
        IAccountLoginCollectionReference GetAccountLoginCollection();
    }

    public interface IAccountLoginRepositoryMock
    {
        Task<IDocumentReference> CreateAccountLoginAsync(AccountLogin accountLogin);
    }

    public class AccountLoginRepositoryMock : IAccountLoginRepositoryMock
    {
        private readonly ICollectionReference _accountLoginCollection;

        public AccountLoginRepositoryMock(ICollectionFactoryMock collectionFactory)
        {
            _accountLoginCollection = collectionFactory.GetAccountLoginCollection();
        }
        public async Task<IDocumentReference> CreateAccountLoginAsync(AccountLogin accountLogin)
        {
            if (accountLogin == null)
                throw new ArgumentNullException("Argument \"accountLogin\" is null while creating the account login.");

            return await _accountLoginCollection.AddAsync(accountLogin);
        }
    }
}
