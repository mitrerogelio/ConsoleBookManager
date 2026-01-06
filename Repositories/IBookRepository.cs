using ConsoleBookManager.Models;

namespace ConsoleBookManager.Repositories;

public interface IBookRepository
{
    IEnumerable<Book> GetBooks(IEnumerable<BookStatus>? status = null);
    Book? GetBook(int id);
    Book AddBook(Book book);
    void UpdateBook(Book book);
    bool DeleteBook(int id);
}