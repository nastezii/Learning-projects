
//library management
using LibraryManagement;

Console.WriteLine("Commands:\nAdd book\n Delete book\nSearch book\nPrint sorted books");
Console.WriteLine("To finish enter \"Finish\".");
string option;
bool runing = true;
Library library = new Library();

while (runing)
{
    Console.Write("\nEnter command: ");
    option = Console.ReadLine();
    switch (option)
    {
        case "Add book":
            AddBook(library);
            break;
        case "Delete book":
            DeleteBook(library);
            break;
        case "Search book":
            SearchBook(library);
            break;
        case "Print sorted books":
            library.PrintSortedBooks();
            break;
        case "Finish":
            runing = false;
            Console.WriteLine("Exiting the program.");
            break;
        default:
            Console.WriteLine("Invalid option. Please, try again.");
            break;
    }
}


void AddBook(Library library)
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


void DeleteBook(Library library)
{
    Console.WriteLine("Enter the name of the book, which you want to delete:");
    string input = Console.ReadLine()?.Trim();
    library.DeleteBook(input);
}


void SearchBook(Library library)
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

namespace LibraryManagement
{ 
    class Library
    {
        static private List<Book> books = new List<Book>();
        public bool AddBook(string title, string author, int year, Genre genre)
        {
            if (books.Any(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase)
                && book.Author.Equals(author, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            books.Add(new Book { Title = title, Author = author, Year = year, Genre = genre });
            return true;
        }
        public void SearchBook(string input)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books in the library.");
                return;
            }
            Console.WriteLine("Searching for a book...");
            var results = books.Where
            (book => book.Title.Contains(input, StringComparison.OrdinalIgnoreCase)
            || book.Author.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToList();
            if (results.Count == 0)
            {
                Console.WriteLine("No books found matching your search.");
            }
            else
            {
                Console.WriteLine("Search results:");
                foreach (var book in results)
                {
                    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.Year}, Genre: {book.Genre}");
                }
            }
        }


        public void DeleteBook(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Book title cannot be empty.");
                return;
            }

            var bookToDelete = books.FirstOrDefault(book => book.Title.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                Console.WriteLine($"The book \"{bookToDelete.Title}\" has been removed from the library.");
            }
            else
            {
                Console.WriteLine($"No book with the title \"{input}\" was found in the library.");
            }
        }


        public void PrintSortedBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books in the library.");
                return;
            }
            var sortedBooks = books.OrderBy(book => book.Year).ToList();

            foreach (var book in sortedBooks)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.Year}, Genre: {book.Genre}");
            }
        }
    }


    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
    }

    enum Genre
    {
        fiction,
        nonFiction,
        science,
        history
    }
}