using ConsoleBookManager.Models;
using ConsoleBookManager.Repositories;

namespace ConsoleBookManager.Services;

public class BookService(IBookRepository repository)
{
    public void AddBook(string title, string author, int totalPages)
    {
        repository.AddBook(new Book(title, author, totalPages));
    }

    public IEnumerable<Book> GetBooks(IEnumerable<BookStatus>? status = null)
    {
        return repository.GetBooks(status);
        // TODO: Sorting logic (e.g. ascending vs descending)?
    }

    public void UpdateTitle(int bookId, string title)
    {
        Book? book = repository.GetBook(bookId);
        if (book is null)
            throw new InvalidOperationException($"Book with id {bookId} does not exist.");
        if (book.Title == title)
            throw new InvalidOperationException($"Title is already: {book.Title}. Your input: {title}");
        book.SetTitle(title);
        repository.UpdateBook(book);
    }

    public void UpdateAuthor(int bookId, string author)
    {
        Book? book = repository.GetBook(bookId);
        if (book is null)
            throw new InvalidOperationException($"Book with id {bookId} does not exist.");
        if (book.Author == author)
            throw new InvalidOperationException($"Author is already set to: {book.Author}. Your input: {author}");
        book.SetAuthor(author);
        repository.UpdateBook(book);
    }

    public void UpdateTotalPages(int bookId, int newPageTotal)
    {
        Book? book = repository.GetBook(bookId);
        if (book is null)
            throw new InvalidOperationException($"Book with id {bookId} does not exist.");
        if (book.TotalPages <= book.CurrentPage)
            book.SetCurrentPage(null);
        book.SetTotalPages(newPageTotal);
        repository.UpdateBook(book);
    }

    public void UpdateCurrentPage(int bookId, int? newCurrentPageNumber)
    {
        Book? book = repository.GetBook(bookId);
        if (book is null)
            throw new InvalidOperationException($"Book with id {bookId} does not exist.");

        if (book.CurrentPage <= newCurrentPageNumber)
            throw new ArgumentOutOfRangeException(nameof(newCurrentPageNumber),
                $"The page number you provided ({newCurrentPageNumber}) is less than the current page number of: {book.CurrentPage}. Please try again");

        book.SetCurrentPage(newCurrentPageNumber);
        repository.UpdateBook(book);
    }


    public void UpdateTotalChapters(int bookId, int newChapterTotal)
    {
        Book? book = repository.GetBook(bookId);
        if (book is null)
            throw new InvalidOperationException($"Book with id {bookId} does not exist.");
        if (book.TotalChapters <= book.CurrentChapter)
            book.SetCurrentChapter(null);
        book.SetTotalChapters(newChapterTotal);
        repository.UpdateBook(book);
    }

    public void UpdateCurrentChapter(int bookId, int? newCurrentChapter)
    {
        Book? book = repository.GetBook(bookId);
        if (book is null)
            throw new InvalidOperationException($"Book with id {bookId} does not exist.");

        if (book.CurrentChapter <= newCurrentChapter)
            throw new ArgumentOutOfRangeException(nameof(newCurrentChapter),
                $"The chapter number you provided ({newCurrentChapter}) is invalid. Current chapter number: {book.CurrentChapter}.");

        book.SetCurrentChapter(newCurrentChapter);
        repository.UpdateBook(book);
    }

    public void WishlistBook(int bookId, bool wishlistStatus)
    {
        Book? book = repository.GetBook(bookId);
        if (book is null)
            throw new InvalidOperationException($"Book with id {bookId} does not exist.");
        book.SetWishlisted(wishlistStatus);
        repository.UpdateBook(book);
    }

    public int GetDailyReadingGoal(int bookId, DateTime date)
    {
        Book? book = repository.GetBook(bookId);
        if (book is null)
            throw new InvalidOperationException($"Book with id {bookId} does not exist.");

        if (book.DueDate is not null) return (book.DueDate.Value.Date - DateTime.Today).Days;
        book.SetDueDate(date);
        repository.UpdateBook(book);

        return (book.DueDate!.Value.Date - DateTime.Today).Days;
    }
}