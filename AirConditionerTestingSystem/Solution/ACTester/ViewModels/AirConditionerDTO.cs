namespace ACTester.ViewModels
{
    using System;
    using System.Text;
    using Interfaces;
    using Utilities.Constants;

    public abstract class AirConditionerDTO : IAirConditioner
    {
        private string manufacturer;

        private string model;

        protected AirConditionerDTO()
        {
        }

        //protected AirConditionerDTO(string manufacturer, string model)
        //{
        //    this.Manufacturer = manufacturer;
        //    this.Model = model;
        //}

        public string Manufacturer
        {
            get
            {
                return this.manufacturer;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < Constants.ManufacturerMinLength)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyLength, "Manufacturer", Constants.ManufacturerMinLength));
                }

                this.manufacturer = value;
            }
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < Constants.ModelMinLength)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyLength, "Model", Constants.ModelMinLength));
                }

                this.model = value;
            }
        }

        public abstract bool Test();

        public override string ToString()
        {
            StringBuilder print = new StringBuilder();
            print.AppendLine("Air Conditioner");
            print.AppendLine("====================");
            print.AppendLine(string.Format("Manufacturer: {0}", this.Manufacturer));
            print.AppendLine(string.Format("Model: {0}", this.Model));
            return print.ToString();
        }
    }
}
