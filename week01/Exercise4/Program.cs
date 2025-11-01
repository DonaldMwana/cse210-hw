using System; 
using System.Collections.Generic;
 
class Program  
{ 
    static void Main()
    {
        List<int> numbers = new List<int>();

        while (true)
        {
            Console.Write("Enter a number (0 to finish): ");
            int number = Convert.ToInt32(Console.ReadLine());

            if (number == 0)
            {
                break;
            }

            numbers.Add(number);
        }

        int sum = 0;
        int max = int.MinValue;

        foreach (int number in numbers)
        {
            sum += number;
            if (number > max)
            {
                max = number;
            }
        }

        double average = (double)sum / numbers.Count;

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");
    } 
} 

