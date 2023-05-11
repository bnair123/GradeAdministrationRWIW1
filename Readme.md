# Real World Interactions - Homework 1

## Assignment

In this first homework you will rewrite the Grade Administration System from your Programming 2 class in C#.

Your program should have at least three classes: Grade, Student, and Administration.

### Grade class

Start with Grade. It will encapsulate information about an examination result. It should have the following properties:

-   `Value` - double or decimal, it accepts only setting values between 1 and 10 and only those that are multiples of 0.5.
-   `Date` - of type DateTime for storing the exam date.
-   `ExamCode` of type int for storing the examination code.
-   `Note` - a string for additional information.

The last three properties must be read-only.

Now, introduce an extra feature into the Grade class by adding a bool property `Frozen` to it. If `Frozen` is false (initial state), the value of a grade can be changed. Once `Frozen` is set to true, the value becomes immutable. It’s also impossible to unfreeze a frozen grade. With `Frozen` added to your class, the `Value` property will be initially mutable. Freezing a grade models an exam review scenario. After an exam, grades are entered into the system but their values can still change after a review. Once the review period is over, grades become immutable.

Provide a constructor for Grade. You can add an additional (overloaded) constructor that doesn’t have a date parameter and initializes the `Date` property with `DateTime.Now`. Implement `toString` that returns a text representation of a Grade instance, for example in the format `>ExamCode on Date: Value<`.

Example:
D4534231 on 27-04-2021: 8.5

### Student class

This class has three string properties for the first name, last name, and the full name of a student. The `FullName` property’s value must be synthesized, through its getter, from the two other properties. Note that this also means that the `FullName` property is read-only!

Furthermore, the class has a `StudentNumber` property that contains an integer number. You can also add a `DateTime` property to store a birth date. The birth date and the student number properties must be read-only. However, names are sometimes changed and this should be reflected in the design of this class.

Student stores grades in a private List of Grades. There is no property associated with this list. To provide the ability to add and modify grades for a given student, this class exposes a `setGrade` method that takes an exam code and a value argument:
void setGrade(int examCode, decimal value)

This method models the real behavior of a grading system:

-   If there is no grade with the same exam code stored in the grades list, a new Grade instance with the current date is created and added to it. (This represents a first exam attempt.)
-   If there is already a grade with the same exam code, and it’s frozen, a new Grade instance with the current date is created and added to it. (This represents a resit.)
-   If there is already a grade with the same exam code, and it’s not frozen, its value is changed to the new one. (This represents a grade correction after a review.)

Add the following methods to your Student class:

-   `toString` that returns a string consisting of the full name of a student and their student number.
-   `printGrades` that prints all the grades of a student (preferably sorted by date).
-   `printGrades`

A copy of the original C code (Also found under OriginalGA.c)
```c
/*
 * Bharath Nair - 530070
 * This program allows the user to enter grades for a specified number of students (in this case, 10 students)
 * and stores them in an array. It then calculates the average of all the grades entered and the number of
 * students who have passed (grades greater than or equal to 5.5). Finally, it prints all the grades entered,
 * the calculated average, and the number of students who have passed. The program uses several functions,
 * including storeGrades, calculateAverage, countPassed, and printResults, to accomplish these tasks.
 */
#include <stdio.h>

void storeGrades(float* grades, int size);
void printResults(float* grades, int size);


int main()
{
    const int N = 10;  // number of students

    // array to store grades
    float grades[N];

    // function that stores the grades entered by the user
    storeGrades(grades, N);

    // function that prints all the results
    printResults(grades, N);

    return 0;
}

// function that stores the grades entered by the user
void storeGrades(float* grades, int size)
{
    for (int i = 0; i < size; i++)
    {
        float grade;

        // prompt user to enter grade
        printf("Enter grade for student %d: ", i + 1);
        scanf("%f", &grade);

        // check if grade is valid
        while (grade < 1.0 || grade > 10.0)
        {
            printf("Invalid grade. Please enter a grade between 1.0 and 10.0: ");
            scanf("%f", &grade);
        }

        grades[i] = grade;
    }
}

// function that calculates the average of all grades
float calculateAverage(float* grades, int size)
{
    float sum = 0.0;

    for (int i = 0; i < size; i++)
    {
        sum += grades[i];
    }

    return sum / size;
}

// function that counts the number of students that have passed
int countPassed(float* grades, int size)
{
    int count = 0;

    for (int i = 0; i < size; i++)
    {
        if (grades[i] >= 5.5)
        {
            count++;
        }
    }

    return count;
}

// function that prints all the results
void printResults(float* grades, int size)
{
    printf("\nAll grades:\n");

    for (int i = 0; i < size; i++)
    {
        printf("Student %d: %.1f\n", i + 1, grades[i]);
    }

    printf("\nAverage: %.2f\n", calculateAverage(grades, size));
    printf("Number of students that have passed: %d\n", countPassed(grades, size));
}
```
