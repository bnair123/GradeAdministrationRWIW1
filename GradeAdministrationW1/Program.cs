using System.Collections;
using System.Diagnostics;

public class Grade
{
    private double _value;

    public double Value
    {
        get => _value;
        set
        {
            if (value >= 1 && value <= 10 && value % 0.5 == 0)
            {
                _value = value;
            }
        }
    }
    public DateTime Date { get; }
    public int ExamCode { get; }
    public string Note { get; }
    public bool Frozen { get; set; }

    // Constructor with all details
    public Grade(double value, int examCode, string note, DateTime date)
    {
        this.Value = value;
        this.Date = date;
        this.ExamCode = examCode;
        this.Note = note;
        this.Frozen = false;
    }

    // Constructor with only exam code and value
    public Grade(double value, int examCode)
    {
        this.Value = value; 
        this.Date = DateTime.Now;
        this.ExamCode = examCode;
        this.Note = string.Empty;
        this.Frozen = false;
    }

    // Constructor with exam code, value, and note
    public Grade(double value, int examCode, string note)
    {
        this.Value = value;
        this.Date = DateTime.Now;
        this.ExamCode = examCode;
        this.Note = note;
        this.Frozen = false;
    }

    public Grade(double value, int examCode, DateTime date)
    {
        this.Value = value;
        this.Date = date;
        this.ExamCode = examCode;
        this.Frozen = false;
    }

    public void SetValue(double value)
    {
        if (value >= 1 && value <= 10 && value % 0.5 == 0)
            this.Value = value;
    }

    public override string ToString()
    {
        return ExamCode + " taken on " + Date + " with result: " + Value;
    }

}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public int StudentNumber { get; }
    public DateTime BirthDate { get; }
    public List<Grade> grades;

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

    public void SetGrade(int examCode, double value)
    {
        bool examExists = false;

        foreach (Grade grade in this.grades)
        {
            if (grade.ExamCode == examCode)
            {
                examExists = true;

                if (!grade.Frozen)
                {
                    grade.Value = value;
                    break;
                }
                else
                {
                    int date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                    int newExamCode = int.Parse(examCode.ToString() + date.ToString());
                    Grade newGrade = new Grade(value, newExamCode, DateTime.Now);
                    grades.Add(newGrade);
                    break;
                }
            }
        }

        if (!examExists)
        {
            var newGrade = new Grade(value, examCode, DateTime.Now);
            grades.Add(newGrade);
        }
    }



    public void PrintGrades()
    {
        var sortedGrades = grades.OrderBy(x => x.Date);

        foreach (var grade in sortedGrades)
        {
            Console.WriteLine(grade.ToString());
        }

    }

    public void PrintGrades(DateTime startDate, DateTime endDate)
    {
        var sortedGrades = grades.OrderBy(x => x.Date).ToList();
        sortedGrades = sortedGrades.Where(x=>(x.Date >= startDate && x.Date <= endDate)).ToList();

        foreach (var grade in sortedGrades)
        {
            Console.WriteLine(grade.ToString());
        }
    }

    public List<Grade> gradesFor(int examCode)
    {
        List<Grade> gradeForExamCode;
        gradeForExamCode = grades.Where(x => x.ExamCode == examCode).ToList();

        return gradeForExamCode;
    }

    public override string ToString()
    {
        return FullName + " " + StudentNumber;
    }


}


public class Administration
{
    public List<Student> Students = new List<Student>();

    public void AddStudent(int studentNumber, string firstName, string lastName, DateTime birthDate)
    {
        var student = new Student(firstName, lastName, studentNumber, birthDate);
        Students.Add(student);
    }

    public void AddStudent(int studentNumber, string firstName, string lastName)
    {
        Student student = new Student(firstName, lastName, studentNumber); //Change from var to Student
        Students.Add(student);
    }

    public bool studentExists(int studentNumber)
    {
        bool isExists = false;
        foreach (var student in Students)
        {
            if (student.StudentNumber == studentNumber)
            {
                isExists = true;
            }
        }
        return isExists;
    }

    public Student getStudent(int studentNumber)
    {
        return Students.FirstOrDefault(x => x.StudentNumber == studentNumber);
    }

    public void PrintAllStudents()
    {
        foreach (var student in Students)
        {
            Console.WriteLine($"{student.FullName} has the student number {student.StudentNumber} with a Date of Birth of {student.BirthDate} ");
        }
        Console.WriteLine("\n");
    }

    public void FreezeExam(int examCode)
    {
        foreach (Student student in Students)
        {
            foreach (Grade grade in student.grades)
            {
                if (grade.ExamCode == examCode)
                {
                    grade.Frozen = true;
                }
            }
        }
    }

    public double GradePointAverage(int studentNumber)
    {
        var student = getStudent(studentNumber);

        if (student == null)
        {
            throw new ArgumentException("Student number not found");
        }

        double tempGrade = 0.0;
        int gradeCount = 0;

        foreach (var grade in student.grades)
        {
            var similarExams = student.grades.Where(g => g.ExamCode.ToString().StartsWith(grade.ExamCode.ToString())).ToList();

            if (similarExams.Count > 0)
            {
                var highestGrade = similarExams.Max(g => g.Value);
                tempGrade += highestGrade;
                gradeCount++;
            }
        }

        if (gradeCount > 0)
        {
            tempGrade /= gradeCount;
            return tempGrade;
        }
        else
        {
            return 0.0; // or NaN, depending on how you want to handle this case
        }
    }


    public void populateStudents()
    {
        Student student1 = new Student("Bharath", "Nair", 530070); 
        Students.Add(student1);
        Student student2 = new Student("John", "Doe", 000001);
        Students.Add(student2);
        Student student3 = new Student("Micheal", "Farrage", 420001); 
        Students.Add(student3);
        Student student4 = new Student("John", "Cena", 101010); 
        Students.Add(student4);
        Student student5 = new Student("Donald", "Trump", 999999); 
        Students.Add(student5);
    }

    public void populateGrade()
    {
        foreach (Student student in Students)
        {
            student.SetGrade(1111, 5.5);
            student.SetGrade(1010, 5);
            student.SetGrade(1234, 8);
            student.SetGrade(4321, 6);
            student.SetGrade(9191, 2);

        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        //Student studentFiller = new Student("John", "Doe", 12345);
        Administration administration = new Administration();

        int option = 0;

        do
        {
            Console.WriteLine("Please enter your desired action\n");
            Console.WriteLine("1. Add a grade\n");
            Console.WriteLine("2. Add a Student\n");
            Console.WriteLine("3. Print all students\n");
            Console.WriteLine("4. Freeze grades\n");
            Console.WriteLine("5. Print student grades\n");
            Console.WriteLine("6. Print student GPA\n");
            Console.WriteLine("7. Populate students\n");
            Console.WriteLine("8. Populate grades for students\n");
            Console.WriteLine("9. Quit\n");
            option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Please enter the students number: ");
                    int studentNumberC1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the exam code");
                    int examCode1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the grade");
                    double value = double.Parse(Console.ReadLine());

                    if (administration.studentExists(studentNumberC1))
                    {
                        Student studentGA = administration.getStudent(studentNumberC1);

                        studentGA.SetGrade(examCode1, value);
                    }

                    else
                    {
                        Console.WriteLine("Student not found!");
                    }

                    break;

                case 2:
                    Console.WriteLine("Please enter their first name: ");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Please enter their last name: ");
                    string lastName = Console.ReadLine();
                    Console.WriteLine("Please enter their Student Number: ");
                    int studentNumberC2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter their Date of Birth (In DD/MM/YYYY)");
                    DateTime dob = DateTime.Parse(Console.ReadLine());

                    administration.AddStudent(studentNumberC2, firstName, lastName, dob);
                    break;

                case 3:
                    administration.PrintAllStudents();
                    break;

                case 4:
                    Console.WriteLine("Please give the exam code to freeze");
                    int examCode = int.Parse(Console.ReadLine());
                    administration.FreezeExam(examCode);

                    break;

                case 5:
                    Console.WriteLine("Please enter the student number: ");
                    int studentNumberC5 = int.Parse(Console.ReadLine());

                    if (administration.studentExists(studentNumberC5))
                    {
                        Student studentPG = administration.getStudent(studentNumberC5);

                        Console.WriteLine("Do you want to see grades within a certain time frame? (yes/no)");
                        string answer = Console.ReadLine();

                        if (answer.ToLower() == "yes")
                        {
                            Console.WriteLine("Please enter the start date (In DD/MM/YYYY): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Please enter the end date (In DD/MM/YYYY): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());

                            studentPG.PrintGrades(startDate, endDate);
                        }
                        else
                        {
                            studentPG.PrintGrades();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Student not found!");
                    }

                    break;

                case 6:
                    Console.WriteLine("Please enter the student number: ");
                    int studentNumberC6 = int.Parse(Console.ReadLine());
                    Console.WriteLine(administration.GradePointAverage(studentNumberC6));
                    
                    break;
                case 7:
                    administration.populateStudents();
                    break;
                case 8:
                    administration.populateGrade();
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        } while (option != 9);
    }
}