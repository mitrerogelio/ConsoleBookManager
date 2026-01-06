using ConsoleBookManager.Data;
using ConsoleBookManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleBookManager.Repositories;

public class BookRepository(BookContext context) : IBookRepository
{
    public IEnumerable<Book> GetBooks(IEnumerable<BookStatus>? status = null)
    {
        return status is null
            ? context.Books.AsNoTracking()
            : context.Books.AsNoTracking().Where(b => status.Contains(b.Status));
    }

    public Book? GetBook(int id)
    {
        return context.Books.Find(id);
    }

    public Book AddBook(Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
        return book;
    }

    public void UpdateBook(Book book)
    {
        context.Books.Update(book);
        context.SaveChanges();
    }

    public bool DeleteBook(int id)
    {
        Book? book = GetBook(id);
        if (book is null) return false;
        context.Books.Remove(book);
        context.SaveChanges();
        return true;
    }
}