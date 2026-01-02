namespace ConsoleBookManager.Models;

public class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int TotalPages { get; private set; }
    public int? CurrentPage { get; private set; }
    public BookStatus Status { get; private set; }
    public DateTime? DueDate { get; private set; }
    public int? TotalChapters { get; private set; }
    public int? CurrentChapter { get; private set; }

    public int? Progress =>
        TotalPages <= 0 || CurrentPage is null
            ? null
            : CurrentPage.Value * 100 / TotalPages;

    public Book(string title, string author, int totalPages)
    {
        SetTitle(title);
        SetAuthor(author);
        SetTotalPages(totalPages);
        Status = BookStatus.Inactive;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException(nameof(title), "Title cannot be null or empty.");
        Title = title.Trim();
    }

    public void SetAuthor(string author)
    {
        if (string.IsNullOrEmpty(author))
            throw new ArgumentNullException(nameof(author), $"{nameof(Author)} cannot be null or empty.");
        Author = author.Trim();
    }

    public void SetTotalPages(int totalPages)
    {
        if (totalPages <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalPages),
                $"{nameof(TotalPages)} must be greater than zero.");
        if (CurrentPage > totalPages)
            throw new ArgumentOutOfRangeException(nameof(totalPages), $"Invalid page number input.");

        TotalPages = totalPages;
    }

    public void SetCurrentPage(int? page)
    {
        if (page < 0 || page > TotalPages)
        {
            throw new ArgumentOutOfRangeException(nameof(page), $"{nameof(page)} is invalid. Please try again");
        }

        CurrentPage = page;

        if (page == 0)
            Status = BookStatus.Inactive;
        else if (page == TotalPages)
            Status = BookStatus.Finished;
        else Status = BookStatus.Reading;
    }

    public void SetWishlisted(bool wishlisted)
    {
        if (wishlisted)
            Status = BookStatus.Wishlisted;
        else if (CurrentPage == 0)
            Status = BookStatus.Inactive;
    }

    public void SetDueDate(DateTime dueDate)
    {
        if (dueDate < DateTime.Today)
            throw new ArgumentException("Due date must be in the future.", nameof(dueDate));

        DueDate = dueDate;
    }

    public void SetTotalChapters(int totalChapters)
    {
        if (totalChapters <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalChapters), "TotalChapters must be greater than zero.");

        if (TotalChapters.HasValue && CurrentChapter > totalChapters)
            throw new InvalidOperationException(
                $"{nameof(TotalChapters)} cannot be less than {nameof(CurrentChapter)}.");

        TotalChapters = totalChapters;
    }

    public void SetCurrentChapter(int? currentChapter)
    {
        if (!TotalChapters.HasValue)
            throw new ArgumentException($"{nameof(TotalChapters)} must have a value and be greater than zero.",
                nameof(currentChapter));
        if (currentChapter < 0 || currentChapter > TotalChapters)
            throw new ArgumentOutOfRangeException(nameof(currentChapter), "Invalid chapter amount.");

        if (currentChapter == TotalChapters)
        {
            CurrentChapter = currentChapter;
            Status = BookStatus.Finished;
        }
        else
        {
            CurrentChapter = currentChapter;
        }

        if (currentChapter is not null)
            Status = BookStatus.Reading;
    }
}