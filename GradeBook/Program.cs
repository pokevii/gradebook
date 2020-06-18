using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Joshua's Grade Book");
            book.GradeAdded += OnGradeAdded;

            Console.WriteLine($"Please input grades into {book}. Type Q to stop.\n");

            EnterGrades(book);

            var stats = book.GetStats();

            System.Console.WriteLine(InMemoryBook.CATEGORY);
            System.Console.WriteLine($"For the book named {book.Name}");
            System.Console.WriteLine($"Average: {(stats.Average):N1}");
            System.Console.WriteLine($"Highest Grade: {(stats.HighestGrade):N1}");
            System.Console.WriteLine($"Lowest Grade: {(stats.LowestGrade):N1}");
            System.Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        private static void EnterGrades(IBook book)
        {            
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "Q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            System.Console.WriteLine("A grade was added.");
        }
    }
}