using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Contracts
{
    public interface Course : IComparable<Course>
    {
        string Name { get; }
        IReadOnlyDictionary<string, Student> StudentsByName { get; }
        void EnrollStudent(Student student);
    }
}
