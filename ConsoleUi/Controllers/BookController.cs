using ConsoleBookManager.ConsoleUi.Menus;
using ConsoleBookManager.ConsoleUi.Utilities;
using ConsoleBookManager.ConsoleUi.Views;
using ConsoleBookManager.Models;
using ConsoleBookManager.Services;

namespace ConsoleBookManager.ConsoleUi.Controllers;

public class BookController(BookService service)
{
    private bool _running = true;

    public void Run()
    {
        while (_running)
        {
            MainMenu.Show();

            int choice = MainMenu.GetChoice();

            switch (choice)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    ListBooks();
                    break;
                case 3:
                    UpdateBook();
                    break;
                case 4:
                    DeleteBook();
                    break;
                case 0:
                    _running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void AddBook()
    {
        (string title, string author, int totalPages) userInput = BookMenu.GetNewBookDetails();

        try
        {
            service.AddBook(userInput.title, userInput.author, userInput.totalPages);
            ConsoleHelper.DisplaySuccess($"{userInput.title} has been added to the book list.");
        }
        catch (Exception e)
        {
            ConsoleHelper.DisplayError(e.Message);
        }
    }

    private void ListBooks(IEnumerable<BookStatus>? status = null)
    {
        List<Book> books = service.GetBooks(status).ToList();
        if (books.Count == 0)
        {
            ConsoleHelper.DisplayError($"No books found.");
            return;
        }

        BookView.ShowBooks(books);
    }

    private void UpdateBook()
    {
        int bookId = ConsoleHelper.GetInt("Enter the book ID: ", "Invalid Id. Please try again.");
        Book? book = service.GetRequiredBook(bookId);
        BookView.DisplayBookDetails(book);
        int userChoice = BookMenu.SelectPropertyToUpdate();
        // TODO: call appropriate service method
        // will use switch statement
    }

    private void DeleteBook() { }
}