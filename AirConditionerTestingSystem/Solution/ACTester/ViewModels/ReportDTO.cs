namespace ACTester.ViewModels
{
    using System.Text;
    using Interfaces;
    using Utilities.Enumerations;

    public class ReportDTO : IReport
    {
        public ReportDTO()
        {
        }

        //public ReportDTO(string manufacturer, string model, Mark mark)
        //{
        //    this.Manufacturer = manufacturer;
        //    this.Model = model;
        //    this.Mark = mark;
        //}

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public Mark Mark { get; set; }
        
        public override string ToString()
        {
            StringBuilder print = new StringBuilder();
            print.AppendLine("Report");
            print.AppendLine("====================");
            print.AppendLine(string.Format("Manufacturer: {0}", this.Manufacturer));
            print.AppendLine(string.Format("Model: {0}", this.Model));
            print.AppendLine(string.Format("Mark: {0}", this.Mark));
            print.Append("====================");
            return print.ToString();
        }
    }
}
