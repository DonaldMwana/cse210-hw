using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("What is your grade percentage? ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int grade))
            {
                string letterGrade = "";

                if (grade >= 90)
                {
                    letterGrade = "A";
                }
                else if (grade >= 80)
                {
                    letterGrade = "B";
                }
                else if (grade >= 70)
                {
                    letterGrade = "C";
                }
                else if (grade >= 60)
                {
                    letterGrade = "D";
                }
                else
                {
                    letterGrade = "F";
                }

                Console.WriteLine($"Your grade is {letterGrade}");

                if (grade >= 70)
                {
                    Console.WriteLine("Congratulations, Donald Mwana, you passed the course!");
                }
                else
                {
                    Console.WriteLine("Better luck next time, Donald Mwana!");
                }

                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid grade percentage.");
            }
        }
    } 
} 