using ConsoleBookManager.Models;
using ConsoleBookManager.Repositories;

namespace ConsoleBookManager.Services;

public class BookService(IBookRepository repository)
{
    public void AddBook(string title, string author, int totalPages)
    {
        repository.AddBook(new Book(title, author, totalPages));
    }

    public void DeleteBook(int bookId)
    {
        bool success = repository.DeleteBook(bookId);
        if (!success)
        {
            throw new ArgumentNullException($"Book with id {bookId} does not exist.");
        }
    }

    public IEnumerable<Book> GetBooks(IEnumerable<BookStatus>? status = null)
    {
        return repository.GetBooks(status).OrderByDescending(b => b.Id);
    }

    public Book GetRequiredBook(int bookId)
    {
        Book? book = repository.GetBook(bookId);
        return book ?? throw new KeyNotFoundException($"Book with id {bookId} does not exist.");
    }

    public void UpdateTitle(int bookId, string title)
    {
        Book book = GetRequiredBook(bookId);
        if (book.Title == title)
            throw new InvalidOperationException($"Title is already: {book.Title}. Your input: {title}");
        book.SetTitle(title);
        repository.UpdateBook(book);
    }

    public void UpdateAuthor(int bookId, string author)
    {
        Book book = GetRequiredBook(bookId);
        if (book.Author == author)
            throw new InvalidOperationException($"Author is already set to: {book.Author}. Your input: {author}");
        book.SetAuthor(author);
        repository.UpdateBook(book);
    }

    public void UpdateTotalPages(int bookId, int newPageTotal)
    {
        Book book = GetRequiredBook(bookId);
        if (newPageTotal < book.CurrentPage)
            book.SetCurrentPage(null);
        book.SetTotalPages(newPageTotal);
        repository.UpdateBook(book);
    }

    public void UpdateCurrentPage(int bookId, int newCurrentPageNumber)
    {
        Book book = GetRequiredBook(bookId);
        book.SetCurrentPage(newCurrentPageNumber);
        repository.UpdateBook(book);
    }

    public void UpdateTotalChapters(int bookId, int? newChapterTotal)
    {
        Book book = GetRequiredBook(bookId);
        if (newChapterTotal is null)
            book.SetTotalChapters(null);
        if (book.CurrentChapter <= newChapterTotal)
            book.SetCurrentChapter(null);
        book.SetTotalChapters(newChapterTotal);
        repository.UpdateBook(book);
    }

    public void UpdateCurrentChapter(int bookId, int? newCurrentChapter)
    {
        Book book = GetRequiredBook(bookId);
        if (newCurrentChapter is null)
            book.SetCurrentChapter(null);
        if (book.CurrentChapter > newCurrentChapter)
            throw new ArgumentOutOfRangeException(nameof(newCurrentChapter),
                $"The chapter number you provided ({newCurrentChapter}) is invalid. Current chapter number: {book.CurrentChapter}.");

        book.SetCurrentChapter(newCurrentChapter);
        repository.UpdateBook(book);
    }

    public void WishlistBook(int bookId, bool wishlistStatus)
    {
        Book book = GetRequiredBook(bookId);
        book.SetWishlisted(wishlistStatus);
        repository.UpdateBook(book);
    }

    public void SetDueDate(int bookId, DateTime dueDate)
    {
        Book book = GetRequiredBook(bookId);
        book.SetDueDate(dueDate);
        repository.UpdateBook(book);
    }

    public (Book Book, int PagesPerDay) GetDailyReadingGoal(int bookId, DateTime? date)
    {
        Book book = GetRequiredBook(bookId);

        DateTime dueDate = date
                           ?? book.DueDate
                           ?? throw new InvalidOperationException("No due date provided.");

        int pagesRemaining = book.TotalPages - (book.CurrentPage ?? 0);
        int daysRemaining = (dueDate.Date - DateTime.Today).Days;

        int goal = daysRemaining switch
        {
            0 => pagesRemaining,
            < 0 => throw new InvalidOperationException($"This book was due {Math.Abs(daysRemaining)} days ago!"),
            _ => (int)Math.Ceiling((double)pagesRemaining / daysRemaining)
        };

        return (book, goal);
    }
}