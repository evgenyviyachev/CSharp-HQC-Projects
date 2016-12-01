namespace ACTester.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interfaces;
    using ViewModels;
    using Utilities.Constants;
    using Utilities.Enumerations;
    using Data;
    //using System.Reflection;
    using AutoMapper;
    using Models;

    /// <summary>
    /// Controller class with all the business logic within. AutoMapper package is used.
    /// Commented out sections are the same logic using System.Reflection instead of AutoMapper.
    /// </summary>
    public class AirConditionerTesterSystem : IAirConditionerTesterSystem
    {
        public AirConditionerTesterSystem(UnitOfWork database)
        {
            this.Database = database;
        }

        public AirConditionerTesterSystem() : this(new UnitOfWork())
        {
        }

        public UnitOfWork Database { get; private set; }

        public string RegisterStationaryAirConditioner(string manufacturer, string model, string energyEfficiencyRating, int powerUsage)
        {
            EnergyEfficiencyRating rating;
            try
            {
                rating =
                    (EnergyEfficiencyRating)Enum.Parse(typeof(EnergyEfficiencyRating), energyEfficiencyRating);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(Constants.IncorrectEnergyEfficiencyRating, ex);
            }

            AirConditioner airConditioner = new StationaryAirConditioner
            {
                Manufacturer = manufacturer,
                Model = model,
                RequiredEnergyEfficiencyRating = rating,
                PowerUsage = powerUsage
            };
            this.Database.AirConditionerRepo.Add(airConditioner);
            this.Database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string RegisterCarAirConditioner(string manufacturer, string model, int volumeCoverage)
        {
            CarAirConditioner airConditioner = new CarAirConditioner
            {
                Manufacturer = manufacturer,
                Model = model,
                VolumeCovered = volumeCoverage
            };
            this.Database.AirConditionerRepo.Add(airConditioner);
            this.Database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string RegisterPlaneAirConditioner(string manufacturer, string model, int volumeCoverage, int electricityUsed)
        {
            PlaneAirConditioner airConditioner = new PlaneAirConditioner
            {
                Manufacturer = manufacturer,
                Model = model,
                VolumeCovered = volumeCoverage,
                ElectricityUsed = electricityUsed
            };

            this.Database.AirConditionerRepo.Add(airConditioner);
            this.Database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string TestAirConditioner(string manufacturer, string model)
        {
            AirConditioner airConditioner = this.GetAirConditionerByManufacturerAndModel(manufacturer, model);
            //AirConditionerDTO airConditionerDTO = this.GetAirConditionerDTOFromModel(airConditioner);
            AirConditionerDTO airConditionerDTO = Mapper.Map<AirConditionerDTO>(airConditioner);

            var mark = airConditionerDTO.Test() ? Mark.Passed : Mark.Failed;
            this.Database.ReportRepo.Add(
                new Report()
                {
                    Manufacturer = manufacturer,
                    Model = model,
                    Mark = mark
                });
            this.Database.Save();
            return string.Format(Constants.TestAirConditioner, model, manufacturer);
        }

        public string FindAirConditioner(string manufacturer, string model)
        {
            AirConditioner airConditioner = this.GetAirConditionerByManufacturerAndModel(manufacturer, model);
            //AirConditionerDTO airConditionerDTO = this.GetAirConditionerDTOFromModel(airConditioner);
            AirConditionerDTO airConditionerDTO = Mapper.Map<AirConditionerDTO>(airConditioner);
            return airConditionerDTO.ToString();
        }

        public string FindReport(string manufacturer, string model)
        {
            Report report = this.GetReportByManufacturerAndModel(manufacturer, model);
            //ReportDTO reportDTO = new ReportDTO(report.Manufacturer, report.Model, report.Mark);
            ReportDTO reportDTO = Mapper.Map<ReportDTO>(report);
            return reportDTO.ToString();
        }

        public string FindAllReportsByManufacturer(string manufacturer)
        {
            var reports = this.Database.ReportRepo.GetAll(r => r.Manufacturer == manufacturer);
            if (reports.Count() == 0)
            {
                return Constants.NoReports;
            }

            //IList<ReportDTO> reportDTOs = new List<ReportDTO>();

            //foreach (var r in reports)
            //{
            //    reportDTOs.Add(new ReportDTO(r.Manufacturer, r.Model, r.Mark));
            //}

            IList<ReportDTO> reportDTOs = Mapper.Map<IList<ReportDTO>>(reports);

            reportDTOs = reportDTOs.OrderBy(x => x.Model).ToList();
            StringBuilder reportsPrint = new StringBuilder();
            reportsPrint.AppendLine(string.Format("Reports from {0}:", manufacturer));
            reportsPrint.Append(string.Join(Environment.NewLine, reportDTOs));
            return reportsPrint.ToString();
        }

        public string Status()
        {
            int reports = this.Database.ReportRepo.Count();
            double airConditioners = this.Database.AirConditionerRepo.Count();
            if (reports == 0)
            {
                return string.Format(Constants.Status, 0);
            }

            double percent = reports / airConditioners;
            percent = percent * 100;
            return string.Format(Constants.Status, percent);
        }

        private AirConditioner GetAirConditionerByManufacturerAndModel(string manufacturer, string model)
        {
            return this.Database.AirConditionerRepo.First(
                air => air.Manufacturer == manufacturer && air.Model == model);
        }

        //private AirConditionerDTO GetAirConditionerDTOFromModel(AirConditioner airConditioner)
        //{
        //    PropertyInfo[] properties = airConditioner.GetType()
        //        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //        .Where(info => info.Name != "Id")
        //        .Reverse()
        //        .ToArray();

        //    object[] values = properties
        //        .Select(info => info.GetValue(airConditioner))
        //        .ToArray();

        //    ConstructorInfo ctor = Type
        //        .GetType("ACTester.ViewModels." + airConditioner.GetType().Name + "DTO")
        //        .GetConstructors()
        //        .FirstOrDefault();

        //    return (AirConditionerDTO)ctor.Invoke(values);
        //}

        private Report GetReportByManufacturerAndModel(string manufacturer, string model)
        {
            return this.Database.ReportRepo.First(
                r => r.Manufacturer == manufacturer && r.Model == model);
        }
    }
}
