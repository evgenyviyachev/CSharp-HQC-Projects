using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaCore_Skeleton.Contracts
{
    public interface IDatabase
    {
        IList<ICore> DB { get; }
        string CreateCore(string[] parameters);
        string RemoveCore(string[] parameters);
        string SelectCore(string[] parameters);
        string AttachFragment(string[] parameters);
        string DetachFragment(string[] parameters);
        string Status(string[] parameters);
    }
}
