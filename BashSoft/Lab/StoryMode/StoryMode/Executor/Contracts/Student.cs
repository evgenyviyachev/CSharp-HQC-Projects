using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Contracts
{
    public interface Student : IComparable<Student>
    {
        string UserName { get; }
        IReadOnlyDictionary<string, double> MarksByCourseName { get; }
        void EnrollInCourse(Course course);
        void SetMarkOnCourse(string courseName, params int[] scores);
        string GetMarkForCourse(string courseName);
    }
}
