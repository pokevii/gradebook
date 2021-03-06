using System;
using System.Collections.Generic;
using System.IO;

//namespace
namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStats();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetStats()
        {
            var result = new Statistics();

            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStats();
    }

    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

/*
        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A': 
                    AddGrade(90);
                    break;
                
                case 'B':
                    AddGrade(80);
                    break;
                
                case 'C':
                    AddGrade(70);
                    break;

                case 'D':
                    AddGrade(60);
                    break;

                default:
                    AddGrade(0);
                    break;
            }       
        }
*/

        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);   
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}.");
            }
        }

        //event
        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStats()
        {
            var result = new Statistics();
            
            for(var index = 0; index < grades.Count; index += 1)
            {
                result.Add(grades[index]);
            }

            return result;
        } 

        private List<double> grades;
        
        public const string CATEGORY = "Science";
    }
}