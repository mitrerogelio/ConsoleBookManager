namespace ConsoleBookManager.Models;

public class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Author { get; private set; } = null!;
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
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title), "Title cannot be null or empty.");
        Title = title.Trim();
    }

    public void SetAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
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
        if (page is null)
        {
            CurrentPage = null;
            Status = BookStatus.Inactive;
            return;
        }

        if (page < 0 || page > TotalPages)
        {
            throw new ArgumentOutOfRangeException(nameof(page),
                $"{nameof(page)} is invalid. Please try again");
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

    public void SetTotalChapters(int? totalChapters)
    {
        TotalChapters = totalChapters switch
        {
            null => null,
            <= 0 => throw new ArgumentOutOfRangeException(nameof(totalChapters),
                "TotalChapters must be greater than zero."),
            _ => totalChapters
        };

        if (TotalChapters.HasValue && CurrentChapter > totalChapters)
            throw new InvalidOperationException(
                $"{nameof(totalChapters)} cannot be less than {nameof(CurrentChapter)}. Maybe reset {nameof(CurrentChapter)} first?");

        TotalChapters = totalChapters;
    }

    public void SetCurrentChapter(int? currentChapter)
    {
        if (currentChapter is null)
        {
            CurrentChapter = null;
            return;
        }

        if (!TotalChapters.HasValue)
        {
            throw new ArgumentException($"{nameof(TotalChapters)} must have a value and be greater than zero.",
                nameof(currentChapter));
        }

        if (currentChapter < 0 || currentChapter > TotalChapters)
        {
            throw new ArgumentOutOfRangeException(nameof(currentChapter), "Invalid chapter amount.");
        }

        CurrentChapter = currentChapter;

        if (currentChapter == TotalChapters)
        {
            Status = BookStatus.Finished;
        }
        else if (currentChapter == 0)
        {
            Status = BookStatus.Inactive;
        }
        else if (currentChapter > 0)
        {
            Status = BookStatus.Reading;
        }
    }
}
