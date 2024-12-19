namespace LibraryManagement
{
    internal class Library
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
}
}
