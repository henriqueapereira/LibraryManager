namespace LibraryManager.API.Entities;

public class Book
{
    public Book(string title, string author, string iSBN, int year)
    {
        Title = title;
        Author = author;
        ISBN = iSBN;
        Year = year;
    }

    public int Id { get; set; }
    public string Title { get; set; } 
    public string Author { get; set; }
    public string ISBN {  get; set; }   
    public int Year {  get; set; }
    public List<Loan> Loans { get; set; }

    public void Update(string title, string author, string iSBN, int year)
    {
        Title = title;
        Author = author;
        ISBN = iSBN;
        Year = year;
    }
}
