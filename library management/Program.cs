//library management
using LibraryManagement;

Console.WriteLine("Commands:\nAdd book\nDelete book\nSearch book\nPrint sorted books");
Console.WriteLine("To finish enter \"Finish\".");
string option;
bool runing = true;
Library library = new Library();

while (runing)
{
    Console.Write("\nEnter command: ");
    option = Console.ReadLine();
    if (option.Equals("Add book", StringComparison.OrdinalIgnoreCase))
    {
        AddBook();
    }
    else if (option.Equals("Delete book", StringComparison.OrdinalIgnoreCase))
    {
        DeleteBook();
    }
    else if (option.Equals("Search book", StringComparison.OrdinalIgnoreCase))
    {
        SearchBook();
    }
    else if (option.Equals("Print sorted books", StringComparison.OrdinalIgnoreCase))
    {
        library.PrintSortedBooks();
    }
    else if (option.Equals("Finish", StringComparison.OrdinalIgnoreCase))
    {
        runing = false;
        Console.WriteLine("Exiting the program.");
    }
    else
    {
        Console.WriteLine("Invalid option. Please, try again.");
    }
}

void AddBook()
{
    Console.WriteLine("\nAdding a new book...");

    Console.WriteLine("Enter title:");
    string title = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(title))
    {
        Console.WriteLine("Title cannot be empty. Book not added.");
        return;
    }

    Console.Write("Enter author: ");
    string author = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(author))
    {
        Console.WriteLine("Author cannot be empty. Book not added.");
        return;
    }

    Console.Write("Enter year: ");
    string year = Console.ReadLine()?.Trim();
    int parsedYear;
    while (!int.TryParse(year, out parsedYear) || parsedYear <= 0)
    {
        Console.WriteLine("Invalid year. Please enter a valid positive number.");
        Console.Write("Enter year: ");
        year = Console.ReadLine()?.Trim();
    }

    Console.Write("Enter genre (fiction, nonFiction, science, history): ");
    string genre = Console.ReadLine()?.Trim();
    Genre parsedGenre;
    while (!Enum.TryParse(genre, true, out parsedGenre))
    {
        Console.Write("Invalid genre. Please enter a valid genre (fiction, nonFiction, science, history): ");
        genre = Console.ReadLine()?.Trim();
    }

    if (library.AddBook(title, author, parsedYear, parsedGenre))
    {
        Console.WriteLine("Book added successfully.");
    }
    else
    {
        Console.WriteLine("Book already exists in the library.");
    }
}

void DeleteBook()
{
    Console.WriteLine("Enter the name of the book, which you want to delete:");
    string input = Console.ReadLine()?.Trim();
    library.DeleteBook(input);
}

void SearchBook()
{
    Console.Write("\nEnter title or author to search: ");
    string input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Search query cannot be empty.");
        return;
    }
    library.SearchBook(input);
}

enum Genre
{
    fiction,
    nonFiction,
    science,
    history
}
