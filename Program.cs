using ConsoleBookManager.Repositories;

bool running = true;
while (running)
{
    Console.WriteLine("\n--- To-Do List Menu ---");
    Console.WriteLine("1. View All Books");
    Console.WriteLine("2. Add New Book");
    Console.WriteLine("3. Mark Book Complete");
    Console.WriteLine("4. Delete a Book");
    Console.WriteLine("5. Exit");

    string choice = Console.ReadLine();
    if (int.Parse(choice) < 1 || int.Parse(choice) > 5)
    {
        Console.WriteLine("Invalid choice. Please try again.");
    }

    switch (choice)
    {
        // case "1": bookRepository.ViewBooks(); break;
        // case "2": bookRepository.AddBook(); break;
        // case "3": bookRepository.MarkBookComplete(); break;
        // case "4": bookRepository.RemoveBook(); break;
        case "5": running = false; break;
        default: Console.WriteLine("Invalid choice. Please try again."); break;
    }
}