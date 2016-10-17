using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRacingSimulator.Interfaces
{
    public interface IBoatEngine : IModelable
    {
        int Output { get; }
    }
}
