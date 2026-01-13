using ConsoleBookManager.ConsoleUi.Controllers;
using ConsoleBookManager.Data;
using ConsoleBookManager.Repositories;
using ConsoleBookManager.Services;

BookContext context = new();

#if DEBUG
context.Database.EnsureDeleted();
context.Database.EnsureCreated();
#endif

BookRepository repository = new(context);
BookService service = new(repository);
BookController controller = new(service);
controller.Run();