using System;
using BoatRacingSimulator.Interfaces;

namespace BoatRacingSimulator.Models.Boats
{
    public class PowerBoat : Boat
    {
        private IBoatEngine firstEngine;
        private IBoatEngine secondEngine;

        public PowerBoat(string model, int weight, IBoatEngine firstEngine, IBoatEngine secondEngine)
            : base(model, weight)
        {
            this.FirstEngine = firstEngine;
            this.SecondEngine = secondEngine;
        }

        public IBoatEngine FirstEngine
        {
            get
            {
                return this.firstEngine;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.firstEngine = value;
            }
        }

        public IBoatEngine SecondEngine
        {
            get
            {
                return this.secondEngine;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.secondEngine = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            var speed = this.FirstEngine.Output + this.SecondEngine.Output - this.Weight + (race.OceanCurrentSpeed / 5d);
            return speed;
        }
    }
}
