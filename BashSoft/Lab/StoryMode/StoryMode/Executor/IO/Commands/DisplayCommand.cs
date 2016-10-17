using Executor.Attributes;
using Executor.Contracts;
using Executor.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.IO.Commands
{
    [Alias("display")]
    public class DisplayCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public DisplayCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string entityToDisplay = this.Data[1];
            string sortType = this.Data[2];
            if (entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                Comparison<Student> studentComparison = this.CreateStudentComparison(sortType);
                ISimpleOrderedBag<Student> sortedStudents = this.repository.GetAllStudentsSorted(studentComparison);
                OutputWriter.WriteMessageOnNewLine(sortedStudents.JoinWith(Environment.NewLine));
            }
            else if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                Comparison<Course> courseComparison = this.CreateCourseComparison(sortType);
                ISimpleOrderedBag<Course> sortedCourses = this.repository.GetAllCoursesSorted(courseComparison);
                OutputWriter.WriteMessageOnNewLine(sortedCourses.JoinWith(Environment.NewLine));
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }

        private Comparison<Student> CreateStudentComparison(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return (student1, student2) => student1.CompareTo(student2);
            }
            else if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return (student1, student2) => student2.CompareTo(student1);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }

        private Comparison<Course> CreateCourseComparison(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return (course1, course2) => course1.CompareTo(course2);
            }
            else if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return (course1, course2) => course2.CompareTo(course1);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
