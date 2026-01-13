namespace ConsoleBookManager.ConsoleUi.Menus;

public static class MainMenu
{
    private static bool _introPlayed = false;
    public static void Show()
    {
        if (!_introPlayed)
        {
            PlayIntroAnimation();
        }

        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("              BOOK MANAGER");
        Console.WriteLine("========================================");
        Console.WriteLine(" 1. Add book");
        Console.WriteLine(" 2. List books");
        Console.WriteLine(" 3. Update a book");
        Console.WriteLine(" 4. Delete a book");
        Console.WriteLine(" 5. Add book to wishlist");
        Console.WriteLine(" 6. Get a daily reading plan for a book");
        Console.WriteLine(" 0. Exit");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.Write(" Select an option: ");
    }

    private static void PlayIntroAnimation()
    {
        try
        {
            Console.Clear();
            Console.CursorVisible = false;

            const int shelfWidth = 30;
            const int totalBooks = 10;

            // Animation Loop: Add one book per frame
            for (int i = 0; i <= totalBooks; i++)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n"); // Push it down a bit

                // 1. Build the Shelf String dynamically
                // Books part: " [] [] [] "
                string books = "";
                for (int b = 0; b < i; b++) books += " []";

                // Empty part: "__________"
                // We calculate remaining space to keep the shelf width fixed
                int usedSpace = books.Length;
                int remainingSpace = shelfWidth - usedSpace;

                // Safety check: don't let remaining space go negative
                if (remainingSpace < 0) remainingSpace = 0;

                string emptySpace = new string('_', remainingSpace);

                // 2. Draw the Shelf Frame
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("      " + new string('_', shelfWidth + 2));
                Console.WriteLine("     / " + new string(' ', shelfWidth) + " /|");
                Console.WriteLine("    /" + new string('_', shelfWidth + 2) + "/ |");

                // The main shelf content
                Console.Write("    |");
                Console.ForegroundColor = ConsoleColor.DarkRed; // Books are blue
                Console.Write(books);
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Shelf is yellow
                Console.Write(emptySpace);
                Console.WriteLine(" | |");

                Console.WriteLine("    |" + new string('_', shelfWidth + 2) + "|/");

                // 3. Title Loading Effect
                if (i == totalBooks)
                {
                    Console.WriteLine("\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("        LIBRARY LOADED.");
                }

                Thread.Sleep(150); // Speed of animation
            }

            _introPlayed = true;
            Thread.Sleep(800);
            Console.ResetColor();
            Console.CursorVisible = true;
        }
        catch
        {
            Console.ResetColor();
            Console.Clear();
        }
    }

    public static int GetChoice()
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
                return choice;

            Console.Write("Invalid input. Please enter a valid option from above: ");
        }
    }

    public static void PlayOutroAnimation()
    {
        try
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            // --- ASSETS ---
            string[] openBook =
            [
                "      ______ ______ ",
                "     /      /     /|",
                "    /______/_____/ |",
                "    |      |     | |",
                "    | GOOD | BYE | /",
                "    |______|_____|/ "
            ];

            string[] halfClosed =
            [
                "         \\      /   ",
                "          \\    /    ",
                "           \\  /     ",
                "            ||      ",
                "            ||      ",
                "            ||      "
            ];

            string[] closedBook =
            [
                "          ______    ",
                "         /     /|   ",
                "        /_____/ |   ",
                "        |     | |   ",
                "        |     | /   ",
                "        |_____|/    "
            ];

            // --- ANIMATION SEQUENCE ---

            // 1. Show Open Book
            DrawFrame(openBook, "Saving progress...");
            Thread.Sleep(600);

            // 2. Show Half Closed
            DrawFrame(halfClosed, "Closing binding...");
            Thread.Sleep(400);

            // 3. Show Closed Book
            DrawFrame(closedBook, "Stored safely.");
            Thread.Sleep(600);

            // 4. The Fade Out (Simulate lights turning off)
            Console.Clear();
            Console.WriteLine("\n\n\n");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("          See you next time.");
            Thread.Sleep(300);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Clear();
            Console.WriteLine("\n\n\n");
            Console.WriteLine("          See you next time.");
            Thread.Sleep(300);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(200);

            Console.ResetColor();
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("Goodbye!");
        }
    }

    private static void DrawFrame(string[] art, string message)
    {
        Console.Clear();
        Console.WriteLine("\n\n\n"); // Padding top

        foreach (string line in art)
        {
            Console.WriteLine("     " + line);
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n     " + message);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
    }
}