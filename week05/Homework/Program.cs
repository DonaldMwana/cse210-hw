using System; 
using System.Threading; 

namespace MindfulnessProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            MindfulnessApp app = new MindfulnessApp();
            app.Run();
        }
    }

    abstract class Activity
    {
        protected string name;
        protected string description;
        protected int duration;

        public Activity(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {name} activity!");
            Console.WriteLine(description);
            Console.Write("Enter the duration of the activity in seconds: ");
            duration = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Get ready to begin...");
            Utilities.Countdown(3);
            Run();
            End();
        }

        protected abstract void Run();

        protected void End()
        {
            Console.WriteLine("Great job!");
            Utilities.Countdown(2);
            Console.WriteLine($"You have completed the {name} activity for {duration} seconds.");
            Utilities.Countdown(3);
        }
    }

    class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.") { }

        protected override void Run()
        {
            DateTime startTime = DateTime.Now;
            DateTime futureTime = startTime.AddSeconds(duration);
            while (DateTime.Now < futureTime)
            {
                Console.WriteLine("Breathe in...");
                Utilities.Countdown(4);
                Console.WriteLine("Breathe out...");
                Utilities.Countdown(4);
            }
        }
    }

    class ReflectionActivity : Activity
    {
        private string[] prompts = new string[] { "Think of a time when you stood up for someone else.", "Think of a time when you did something really difficult.", "Think of a time when you helped someone in need." };
        private string[] questions = new string[] { "Why was this experience meaningful to you?", "Have you ever done anything like this before?", "How did you get started?" };

        public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.") { }

        protected override void Run()
        {
            Console.WriteLine(prompts[new Random().Next(0, prompts.Length)]);
            DateTime startTime = DateTime.Now;
            DateTime futureTime = startTime.AddSeconds(duration);
            int index = 0;
            while (DateTime.Now < futureTime)
            {
                Console.WriteLine(questions[index]);
                Utilities.Countdown(4);
                index = (index + 1) % questions.Length;
            }
        }
    }

    class ListingActivity : Activity
    {
        private string[] prompts = new string[] { "Who are people that you appreciate?", "What are personal strengths of yours?", "Who are people that you have helped this week?" };

        public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") { }

        protected override void Run()
        {
            Console.WriteLine(prompts[new Random().Next(0, prompts.Length)]);
            Utilities.Countdown(3);
            Console.WriteLine("Start listing...");
            DateTime startTime = DateTime.Now;
            DateTime futureTime = startTime.AddSeconds(duration);
            int count = 0;
            while (DateTime.Now < futureTime)
            {
                Console.Write("Item: ");
                Console.ReadLine();
                count++;
            }
            Console.WriteLine($"You listed {count} items.");
        }
    }

    class MindfulnessApp
    {
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness App");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Choose an activity: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        new BreathingActivity().Start();
                        break;
                    case 2:
                        new ReflectionActivity().Start();
                        break;
                    case 3:
                        new ListingActivity().Start();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Utilities.Countdown(2);
                        break;
                }
            }
        }
    }

    public static class Utilities
    {
        public static void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write("\r" + i);
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }
}
