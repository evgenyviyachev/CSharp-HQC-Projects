using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRacingSimulator.Interfaces
{
    public interface IBoat : IModelable
    {
        int Weight { get; }
        double CalculateRaceSpeed(IRace race);
    }
}
