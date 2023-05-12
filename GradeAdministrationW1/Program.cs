public class Grade
{
    public double Value { get; private set; }
    public readonly DateTime Date { get; }
    public readonly int  ExamCode { get; }
    public readonly string Note { get; }
    public readonly bool Frozen { get; private set; }

    // Constructor with all details
    public Grade(double value, int examCode, string note, DateTime date)
    {
        SetValue(value);
        this.Date = date;
        this.ExamCode = examCode;
        this.Note = note;
        this.Frozen = false;
    }

    // Constructor with only exam code and value
    public Grade(double value, int examCode)
    {
        SetValue(value);
        this.Date = DateTime.Now;
        this.ExamCode = examCode;
        this.Note = string.Empty;
        this.Frozen = false;
    }

    // Constructor with exam code, value, and note
    public Grade(double value, int examCode, string note)
    {
        SetValue(value);
        this.Date = DateTime.Now;
        this.ExamCode = examCode;
        this.Note = note;
        this.Frozen = false;
    }

    // Overload that doesn’t have a date parameter and initializes the Date property with DateTime.Now
    public Grade(double value, int examCode, string note, bool isNow)
    {
        SetValue(value);
        this.Date = DateTime.Now;
        this.ExamCode = examCode;
        this.Note = note;
        this.Frozen = false;
    }

    public void SetValue(double value)
    {
        if (value < 1 || value > 10 || (value * 2) % 1 != 0 || Frozen)
        {
            throw new ArgumentException("Invalid grade value.");
        }
        this.Value = value;
    }




}

public class Student
{
    public string FirstName { }
    public string LastName { }
    public string FullName => $"{FirstName} {LastName}";
    public int StudentNumber { }
    public DateTime BirthDate { }


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