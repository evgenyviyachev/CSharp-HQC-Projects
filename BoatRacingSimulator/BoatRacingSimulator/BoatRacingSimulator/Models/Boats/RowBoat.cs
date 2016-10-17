using BoatRacingSimulator.Utility;
using BoatRacingSimulator.Interfaces;

namespace BoatRacingSimulator.Models.Boats
{
    public class RowBoat : Boat
    {
        private int oars;

        public RowBoat(string model, int weight, int oars)
            : base(model, weight)
        {
            this.Oars = oars;
        }

        public int Oars
        {
            get
            {
                return this.oars;
            }

            private set
            {
                Validator.ValidatePropertyValue(value, nameof(this.Oars));
                this.oars = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            var speed = (this.Oars * 100) - this.Weight + race.OceanCurrentSpeed;
            return speed;
        }
    }
}
