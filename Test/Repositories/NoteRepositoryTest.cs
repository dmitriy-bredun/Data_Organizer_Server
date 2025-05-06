using Data_Organizer_Server.Entities;
using Data_Organizer_Server.Interfaces;
using Data_Organizer_Server.Repositories;
using Moq;

namespace Test.Repositories
{
    public class NoteRepositoryTest
    {
        private readonly Mock<ICollectionFactory> collectionFactoryMock = new Mock<ICollectionFactory>();
        private readonly Note noteOnlyWithHeader;
        private readonly Note noteOnlyWithBody;
        private readonly NoteRepository repository;

        public NoteRepositoryTest()
        {
            repository = new NoteRepository(collectionFactoryMock.Object);

            var content = "SomeContent";
            noteOnlyWithBody = new Note()
            {
                Header = new NoteHeader()
                {
                    UserId = "SomeUserId",
                    Title = "SomeTitle",
                    PreviewText = "PreviewText",
                    CreationTime = DateTime.Now,
                    IsDeleted = false
                }
            };
            noteOnlyWithBody = new Note()
            {
                Body = new NoteBody()
                {
                    Content = content
                }
            };
        }

        [Fact]
        public async Task CreateNoteAsync_Should_Throw_Exception_When_Note_IsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.CreateNoteAsync(null));
        }

        [Fact]
        public async Task CreateNoteAsync_Should_Throw_Exception_When_NoteHeader_IsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.CreateNoteAsync(noteOnlyWithBody));
        }

        [Fact]
        public async Task CreateNoteAsync_Should_Throw_Exception_When_NoteBody_IsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.CreateNoteAsync(noteOnlyWithHeader));
        }

        [Fact]
        public async Task GetNoteBodyByHeaderAsync_Should_Throw_Exception_When_NoteHeader_IsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.GetNoteBodyByHeaderAsync(null));
        }

        [Fact]
        public async Task GetNoteHeadersByUidAsync_Should_Throw_Exception_When_uid_IsNullOrWhiteSpace()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.GetNoteHeadersByUidAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.GetNoteHeadersByUidAsync(string.Empty));
        }

        [Fact]
        public async Task UpdateNoteAsync_Should_Throw_Exception_When_note_IsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.UpdateNoteAsync(null));
        }

        [Fact]
        public async Task UpdateNoteAsync_Should_Throw_Exception_When_noteHeader_IsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.UpdateNoteAsync(noteOnlyWithBody));
        }
    }
}
