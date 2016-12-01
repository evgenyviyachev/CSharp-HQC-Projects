namespace ACTester.ViewModels
{
    using System;
    using System.Text;
    using Utilities.Constants;

    public abstract class VehicleAirConditionerDTO : AirConditionerDTO
    {
        private int volumeCovered;

        protected VehicleAirConditionerDTO()
            : base()
        {
        }

        //protected VehicleAirConditionerDTO(string manufacturer, string model, int volumeCoverage) 
        //    : base(manufacturer, model)
        //{
        //    this.VolumeCovered = volumeCoverage;
        //}

        public int VolumeCovered
        {
            get
            {
                return this.volumeCovered;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositiveNumber, "Volume Covered"));
                }

                this.volumeCovered = value;
            }
        }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder(base.ToString());
            print.AppendLine(string.Format("Volume Covered: {0}", this.VolumeCovered));
            return print.ToString();
        }
    }
}
