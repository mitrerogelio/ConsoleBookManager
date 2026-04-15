using ConsoleBookManager.Models;

namespace ConsoleBookManager.Tests.Models;

public class BookTests
{
	[Fact]
	public void Constructor_ValidInput_BuildsValidModel()
	{
		// Arrange
		string expectedTitle = "Clean Code";
		string expectedAuthor = "Albert Lee";
		int expectedPages = 100;

		// Act
		Book bookObj = new(expectedTitle, expectedAuthor, expectedPages);

		// Assert
		Assert.Equal(expectedTitle, bookObj.Title);
		Assert.Equal(expectedAuthor, bookObj.Author);
		Assert.Equal(expectedPages, bookObj.TotalPages);
		Assert.Equal(BookStatus.Inactive, bookObj.Status);
		Assert.Null(bookObj.CurrentPage);
		Assert.Null(bookObj.DueDate);
		Assert.Null(bookObj.TotalChapters);
		Assert.Null(bookObj.CurrentChapter);
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("    ")]
	public void Constructor_InvalidTitle_ThrowsArgumentNullException(string? title)
	{
		// Arrange
		string? invalidTitle = title;
		string validAuthor = "Albert Lee";
		int validPages = 100;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
		{
			new Book(invalidTitle!, validAuthor, validPages);
		});
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("    ")]
	public void Constructor_InvalidAuthor_ThrowsArgumentNullException(string? author)
	{
		// Arrange
		string validTitle = "Clean Code";
		string? invalidAuthor = author;
		int validPages = 100;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
		{
			new Book(validTitle, invalidAuthor!, validPages);
		});
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public void Constructor_InvalidTotalPages_ThrowsArgumentNullException(int pages)
	{
		// Arrange
		string validTitle = "Clean Code";
		string validAuthor = "Albert Lee";
		int invalidPages = pages;

		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			new Book(validTitle, validAuthor, invalidPages);
		});
	}

	[Fact]
	public void Book_SetCurrentPage_FailsWithInvalidArg()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			book.SetCurrentPage(900);
		});
	}

	[Fact]
	public void Book_SetCurrentChapter_FailsWithInvalidArg()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetTotalChapters(5);

		// Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			book.SetCurrentChapter(10);
		});
	}

	[Theory]
	[InlineData(50, 100, 50)]
	[InlineData(10, 100, 10)]
	[InlineData(300, 300, 100)]
	[InlineData(0, 100, 0)]
	public void Book_ProgressUpdate_SuccessfulOnNewCurrentPage(int current, int total, int expectedPercentage)
	{
		// Arrange
		Book book = new("Test Title", "Test Author", total);

		// Act
		book.SetCurrentPage(current);

		// Assert
		Assert.Equal(expectedPercentage, book.Progress);
	}

	[Theory]
	[InlineData(20, 55, 36)]
	[InlineData(15, 74, 20)]
	public void Book_Progress_RoundsPercentages(int current, int total, int expected)
	{
		// Arrange
		Book book = new("Test Title", "Test Author", total);

		// Act
		book.SetCurrentPage(current);

		// Assert
		Assert.Equal(expected, book.Progress);
	}

	[Fact]
	public void Book_Progress_NullWithoutCurrentPage()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Assert
		Assert.Null(book.Progress);
	}

	[Fact]
	public void Book_Status_InactiveByDefault()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Assert
		Assert.Equal(BookStatus.Inactive, book.Status);

	}

	[Fact]
	public void Book_Status_ReadingWhenCurrentPageIsSet()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetCurrentPage(5);

		// Assert
		Assert.Equal(BookStatus.Reading, book.Status);
	}


	[Fact]
	public void Book_Status_ReadingWhenCurrentChapterIsSet()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetTotalChapters(10);
		book.SetCurrentChapter(5);

		// Assert
		Assert.Equal(BookStatus.Reading, book.Status);
	}

	[Fact]
	public void Book_Status_FinishedWhenPageLimitIsReached()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetCurrentPage(100);

		// Assert
		Assert.Equal(BookStatus.Finished, book.Status);
	}

	[Fact]
	public void Book_Status_FinishedWhenChaptersAreFinished()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetTotalChapters(10);
		book.SetCurrentChapter(10);

		// Assert
		Assert.Equal(BookStatus.Finished, book.Status);
	}

	[Fact]
	public void Book_Status_InactiveOnChapterZero()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetTotalChapters(10);
		book.SetCurrentChapter(0);

		// Assert
		Assert.Equal(BookStatus.Inactive, book.Status);
	}

	[Fact]
	public void Book_Status_InactiveWhileCurrentPageIsSet()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetTotalChapters(10);
		book.SetCurrentPage(50);
		book.SetCurrentChapter(0);

		// Assert
		Assert.Equal(BookStatus.Inactive, book.Status);
		Assert.Equal(0, book.CurrentPage);
	}


	[Theory]
	[InlineData(true, BookStatus.Wishlisted)]
	[InlineData(false, BookStatus.Inactive)]
	public void Book_Status_SetsToWishlisted(bool wishlist, BookStatus status)
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetWishlisted(wishlist);

		// Assert
		Assert.Equal(status, book.Status);
	}

	[Fact]
	public void Book_Status_FromWishlistedToReadingOnPageUpdate()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetWishlisted(true);
		book.SetCurrentPage(5);

		// Assert
		Assert.Equal(BookStatus.Reading, book.Status);
	}

	[Fact]
	public void Book_Status_FromWishlistedToReadingOnChapterUpdate()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetWishlisted(true);
		book.SetTotalChapters(4);
		book.SetCurrentChapter(2);

		// Assert
		Assert.Equal(BookStatus.Reading, book.Status);
	}

	[Fact]
	public void Book_SetCurrentChapter_FailsWithoutTotalChapters()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act & Assert
		Assert.Throws<ArgumentException>(() =>
		{
			book.SetCurrentChapter(2);
		});
	}

	[Fact]
	public void Book_SetCurrentChapter_FailsWithOutOfRangeArg()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			book.SetTotalChapters(1);
			book.SetCurrentChapter(2);
		});
	}

	[Fact]
	public void Book_SetCurrentChapter_AllowsNullArg()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetTotalChapters(5);
		book.SetCurrentChapter(null);

		// Assert
		Assert.Null(book.CurrentChapter);
	}

	[Fact]
	public void Book_SetDueDate_RejectsPastDateTimeArg()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);
		DateTime pastDate = DateTime.Today.AddDays(-1);

		// Act
		var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				book.SetDueDate(pastDate);
			}
		);

		// Assert
		Assert.Equal("dueDate", exception.ParamName);
		Assert.Equal(pastDate, exception.ActualValue);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(100)]
	public void Book_SetDueDate_AllowsFutureDates(int daysInFuture)
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);
		DateTime expectedDate = DateTime.Today.AddDays(daysInFuture);

		// Act
		book.SetDueDate(expectedDate);

		// Assert
		Assert.Equal(expectedDate, book.DueDate);
	}

	[Fact]
	public void Book_SetDueDate_AllowsMaxDate()
	{
		// Arrange
		Book book = new("Test Title", "Test Author", 100);

		// Act
		book.SetDueDate(DateTime.MaxValue);

		// Assert
		Assert.Equal(DateTime.MaxValue, book.DueDate);
	}
}
