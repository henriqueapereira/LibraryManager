namespace LibraryManager.API.Models;

public class CreateBookInputModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int Year { get; set; }
}
