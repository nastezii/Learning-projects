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