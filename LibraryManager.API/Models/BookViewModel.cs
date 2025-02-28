using LibraryManager.API.Entities;

namespace LibraryManager.API.Models;

public class BookViewModel
{
    public BookViewModel(int id, string title, string author, string iSBN, int year)
    {
        Id = id;
        Title = title;
        Author = author;
        ISBN = iSBN;
        Year = year;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int Year { get; set; }

    public static BookViewModel FromEntity(Book book) => new(book.Id, book.Title, book.Author, book.ISBN, book.Year);
}
