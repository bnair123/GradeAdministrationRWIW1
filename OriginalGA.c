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
