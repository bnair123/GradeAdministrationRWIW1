﻿using System.Collections;
using System.Diagnostics;

public class Grade
{
    public double Value { get; set {}; } //Add to setter
    public DateTime Date { get; }
    public int  ExamCode { get; }
    public string Note { get; }
    public bool Frozen { get; set; }

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
                    int date = Int32.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                    int newExamCode = int.Parse(examCode.ToString() + date.ToString());
                    Grade newGrade = new Grade(value, newExamCode);
                    grades.Add(newGrade);
                }


            }

            if (!examExists)
            {
                var newGrade= new Grade (value, examCode);
                grades.Add (newGrade);
            }

        }

    }


    public void PrintGrades()
    {
        foreach (var grade in grades)
        {
            Console.WriteLine(grade.ToString());
        }
    }


    public double GradePointAverage()
    {
        double tempGrade = 0;

        foreach (var grade in grades)
        {
            tempGrade += grade.Value;
        }

        tempGrade /= grades.Count;

        return tempGrade;
    }

    public void FreezeGrades(int examCode)
    {
        foreach (Grade grade in grades)
        {
            if (grade.ExamCode == examCode)
            {
                grade.Frozen = true;
            }
        }
    }

    public override string ToString()
    {
        return FullName + " " + StudentNumber;
    }

    public void PrintAllStudents()
    {
        foreach (var student in Student)
        {
            Console.WriteLine(student);
        }
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
        var student = new Student(firstName, lastName, studentNumber);
        Students.Add(student);
    }

    public Student studentExists(int studentNumber)
    {
        bool Existant = false;

        foreach (var student in Students)
        {
            if (student.StudentNumber == studentNumber)
            {
                Existant = true;
                break;
            }
        }

    }

    public Student getStudent(int studentNumber)
    {
        return Students.Find(Student.StudentNumber);
    }

    public void PrintAllStudents()
    {
        foreach (var student in Students)
        {
            Console.WriteLine($"{student.FullName} has the student number {student.StudentNumber} with a Date of Birth of {student.BirthDate} ");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Student studentFiller = new Student("John", "Doe", 12345);
        var administration = new Administration();

        Console.WriteLine("Please enter your desired action\n");
        Console.WriteLine("1. Add a grade\n");
        Console.WriteLine("2. Add a Student\n");
        Console.WriteLine("3. Print all students\n");
        Console.WriteLine("4. Freeze grades\n");
        Console.WriteLine("5. Print student grades\n");
        Console.WriteLine("6. Print student GPA");

        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 1:
                Console.WriteLine("Please enter the students number: ");
                int studentNumberC1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter the exam code");
                int examCode = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter the grade");
                double value = double.Parse(Console.ReadLine());

                if (administration.studentExists(studentNumberC1))
                {
                    Student studentGA = administration.getStudent(studentNumberC1);

                    studentGA.SetGrade(examCode, value);
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
                studentFiller.FreezeGrades(examCode);
                break;

            case 5:
                Console.WriteLine("Please enter the student number: ");
                int studentNumberC5 = int.Parse(Console.ReadLine());

                if (administration.studentExists(studentNumberC5))
                {
                    Student studentPG = administration.getStudent(studentNumberC5);

                    studentPG.PrintGrades();
                }

                break;

            case 6:
                Console.WriteLine("Please enter the student number: ");
                int studentNumberC6 = int.Parse(Console.ReadLine());
                if (administration.studentExists(studentNumberC6))
                {
                    Student studentPGPA = administration.getStudent(studentNumberC6);

                    Console.WriteLine(studentPGPA.GradePointAverage());
                }
                break;
            default:
                Console.WriteLine("Invalid input!");
                break;
        }
    }
}