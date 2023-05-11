public class Grade
{
    public double Value { get; private set; }
    public DateTime Date { get; }
    public int ExamCode { get; }
    public string Note { get; }
    public bool Frozen { get; private set; }




}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public int StudentNumber { get; }
    public DateTime BirthDate { get; }

    
}

public class Administration
{

}

public class Program
{
    public static void Main(string[] args)
    {
    }
}