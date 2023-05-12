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

    public Grade(double value, int examCode, DateTime date)
    {
        SetValue(value);
        this.Date = date;
        this.ExamCode = examCode;
        this.Frozen = false;
    }

    public void SetValue(double value)
    {
        if (!Frozen && value >= 1 && value <= 10 && value % 0.5 == 0)
            this.Value = value;
    }




}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public int StudentNumber { get; }
    public DateTime BirthDate { get; }
    private List<Grade> grades;

    // Constructor with only name and student number
    public Student(string firstName, string lastName, int studentNumber)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.StudentNumber = studentNumber;
        this.grades = new List<Grade>();
    }

    // Constructor with all details
    public Student(string firstName, string lastName, int studentNumber, DateTime birthDate)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.StudentNumber = studentNumber;
        this.BirthDate = birthDate;
        this.grades = new List<Grade>();
    }

    public void SetGrade(int examCode, double value, bool isFinal)
    {

    }



    public void PrintGrades()
    {

    }

    public void PrintGrades(DateTime start, DateTime end)
    {

    }

    public List<Grade> GradesFor(int examCode)
    {

    }

    public double GradePointAverage()
    {

    }

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