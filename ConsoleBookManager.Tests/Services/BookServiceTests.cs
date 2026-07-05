using ConsoleBookManager.Models;
using ConsoleBookManager.Repositories;
using ConsoleBookManager.Services;

namespace ConsoleBookManager.Tests.Services;

public class BookServiceTests
{
	private class MockBookRepository : IBookRepository
	{
		private readonly Dictionary<int, Book> _books = new();

		public int UpdateCallCount { get; private set; }

		public void Seed(int id, Book book) => _books[id] = book;

		public IEnumerable<Book> GetBooks(IEnumerable<BookStatus>? status = null) => _books.Values;

		public Book? GetBook(int id) => _books.TryGetValue(id, out Book? book) ? book : null;

		public Book AddBook(Book book) => book;

		public void UpdateBook(Book book) => UpdateCallCount++;

		public bool DeleteBook(int id) => _books.Remove(id);
	}

	[Fact]
	public void UpdateAllFields_ValidInput_UpdatesModelAndSaves()
	{
		// Arrange
		MockBookRepository repository = new();
		Book book = new("Old Title", "Old Author", 100);
		repository.Seed(1, book);
		BookService service = new(repository);

		// Act
		service.UpdateAllFields(1, "New Title", null, null, null, null, null, null);

		// Assert
		Assert.Equal("New Title", book.Title);
		Assert.Equal("Old Author", book.Author);
		Assert.Equal(1, repository.UpdateCallCount);
	}

	[Fact]
	public void UpdateAllFields_MissingId_ThrowsKeyNotFoundException()
	{
		// Arrange
		MockBookRepository repository = new();
		BookService service = new(repository);

		// Act & Assert
		Assert.Throws<KeyNotFoundException>(() =>
		{
			service.UpdateAllFields(99, "New Title", null, null, null, null, null, null);
		});
	}

	[Fact]
	public void UpdateAllFields_InvalidInput_ThrowsAndDoesNotSave()
	{
		// Arrange
		MockBookRepository repository = new();
		Book book = new("Keep Title", "Keep Author", 100);
		book.SetCurrentPage(50);
		repository.Seed(1, book);
		BookService service = new(repository);

		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			service.UpdateAllFields(1, null, null, null, 900, null, null, null);
		});

		Assert.Equal("Keep Title", book.Title);
		Assert.Equal(50, book.CurrentPage);
		Assert.Equal(0, repository.UpdateCallCount);
	}
}
