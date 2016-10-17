using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Contracts
{
    public interface IRequester
    {
        void GetStudentScoresFromCourse(string courseName, string username);

        void GetAllStudentsFromCourse(string courseName);

        ISimpleOrderedBag<Course> GetAllCoursesSorted(Comparison<Course> cmp);

        ISimpleOrderedBag<Student> GetAllStudentsSorted(Comparison<Student> cmp);
    }
}
