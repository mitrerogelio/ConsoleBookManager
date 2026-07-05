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
        {
            throw new ArgumentNullException(nameof(title), "Title cannot be null or empty.");
        }
        Title = title.Trim();
    }

    public void SetAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(Author)} cannot be null or empty.");
        }
        Author = author.Trim();
    }

    public void SetTotalPages(int totalPages)
    {
        if (totalPages <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalPages),
                $"{nameof(TotalPages)} must be greater than zero.");
        }
        if (CurrentPage > totalPages)
        {
            throw new ArgumentOutOfRangeException(nameof(totalPages), $"Invalid page number input.");
        }

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

        if (TotalChapters is null)
        {
            if (page == 0)
            {
                Status = BookStatus.Inactive;
            }
            else if (page == TotalPages)
            {
                Status = BookStatus.Finished;
            }
            else
            {
                Status = BookStatus.Reading;
            }
        }
        else
        {
            if (page == 0)
            {
                CurrentChapter = 0;
                Status = BookStatus.Inactive;
            }
            if (page == TotalPages)
            {
                TotalChapters = null;
                CurrentChapter = null;
                Status = BookStatus.Finished;
            }
        }
    }

    public void SetWishlisted(bool wishlisted)
    {
        if (wishlisted)
        {
            Status = BookStatus.Wishlisted;
        }
        else if (CurrentPage == 0)
        {
            Status = BookStatus.Inactive;
        }
    }

    public void SetDueDate(DateTime dueDate)
    {
        if (dueDate < DateTime.Today)
        {
            throw new ArgumentOutOfRangeException(nameof(dueDate), dueDate, "Due Date must be today or in the future.");
        }

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
        {
            throw new ArgumentOutOfRangeException(nameof(totalChapters), totalChapters, "Cannot set total chapters to a value less than current chapter.");
        }

        TotalChapters = totalChapters;
    }

    public void SetCurrentChapter(int? currentChapter)
    {
        if (currentChapter is null)
        {
            CurrentChapter = null;
            return;
        }

        if (TotalChapters is null)
        {
            throw new ArgumentException($"{nameof(TotalChapters)} must have a value and be greater than zero.",
                nameof(currentChapter));
        }

        if (currentChapter < 0 || currentChapter > TotalChapters)
        {
            throw new ArgumentOutOfRangeException(nameof(currentChapter), "Current chapter cannot be less than zero or greater than total chapters.");
        }

        CurrentChapter = currentChapter;

        if (CurrentPage is null)
        {
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
        else
        {
            if (currentChapter == 0)
            {
                CurrentPage = 0;
                Status = BookStatus.Inactive;
            }
        }
    }

    public void UpdateBookDetails(string? title, string? author, int? totalPages, int? currentPage,
        int? totalChapters, int? currentChapter, DateTime? dueDate)
    {
        string mergedTitle = title ?? Title;
        string mergedAuthor = author ?? Author;
        int mergedTotalPages = totalPages ?? TotalPages;
        int? mergedCurrentPage = currentPage ?? CurrentPage;
        int? mergedTotalChapters = totalChapters ?? TotalChapters;
        int? mergedCurrentChapter = currentChapter ?? CurrentChapter;

        if (string.IsNullOrWhiteSpace(mergedTitle))
        {
            throw new ArgumentNullException(nameof(title), "Title cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(mergedAuthor))
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(Author)} cannot be null or empty.");
        }

        if (mergedTotalPages <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalPages), $"{nameof(TotalPages)} must be greater than zero.");
        }

        if (mergedCurrentPage < 0 || mergedCurrentPage > mergedTotalPages)
        {
            throw new ArgumentOutOfRangeException(nameof(currentPage), "Current page cannot be less than zero or exceed total pages.");
        }

        if (mergedTotalChapters is <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalChapters), "TotalChapters must be greater than zero.");
        }

        if (mergedCurrentChapter is not null)
        {
            if (mergedTotalChapters is null)
            {
                throw new ArgumentException($"{nameof(TotalChapters)} must have a value and be greater than zero.", nameof(currentChapter));
            }

            if (mergedCurrentChapter < 0 || mergedCurrentChapter > mergedTotalChapters)
            {
                throw new ArgumentOutOfRangeException(nameof(currentChapter), "Current chapter cannot be less than zero or greater than total chapters.");
            }
        }

        if (dueDate.HasValue && dueDate.Value < DateTime.Today)
        {
            throw new ArgumentOutOfRangeException(nameof(dueDate), dueDate, "Due Date must be today or in the future.");
        }

        SetTitle(mergedTitle);
        SetAuthor(mergedAuthor);

        CurrentPage = null;
        CurrentChapter = null;

        SetTotalPages(mergedTotalPages);
        SetTotalChapters(mergedTotalChapters);

        SetCurrentPage(mergedCurrentPage);
        SetCurrentChapter(mergedCurrentChapter);

        if (dueDate.HasValue)
        {
            SetDueDate(dueDate.Value);
        }
    }
}
