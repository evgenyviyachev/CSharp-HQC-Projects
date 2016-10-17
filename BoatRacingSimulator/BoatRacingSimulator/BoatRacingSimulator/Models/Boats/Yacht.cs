using System;
using BoatRacingSimulator.Interfaces;
using BoatRacingSimulator.Utility;

namespace BoatRacingSimulator.Models.Boats
{
    public class Yacht : Boat
    {
        private int cargoWeight;
        private IBoatEngine engine;

        public Yacht(string model, int weight, int cargoWeight, IBoatEngine engine)
            : base(model, weight)
        {
            this.CargoWeight = cargoWeight;
            this.Engine = engine;
        }

        public int CargoWeight
        {
            get
            {
                return this.cargoWeight;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, "Cargo Weight");
                this.cargoWeight = value;
            }
        }

        public IBoatEngine Engine
        {
            get
            {
                return this.engine;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.engine = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            var speed = this.Engine.Output - (this.Weight + this.CargoWeight) + (race.OceanCurrentSpeed / 2d);
            return speed;
        }
    }
}
