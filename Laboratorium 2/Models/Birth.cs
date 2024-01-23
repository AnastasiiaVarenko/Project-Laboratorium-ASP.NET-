namespace Laboratorium_2.Models;

public class Birth
{
    public string Imie { get; set; } = "";
    public DateTime BirthDate { get; set; }


    public bool IsValid()
    {
        return Imie != null && BirthDate < DateTime.Now;
    }
    public int CalcBirth()
    {
       
        var age = (int)DateTime.Now.Year-BirthDate.Year;
        return age;
    }
}
