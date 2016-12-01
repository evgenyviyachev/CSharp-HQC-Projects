namespace ACTester.Data
{
    using Interfaces;
    using Repositories;
    using Models;

    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<AirConditioner> airConditionerRepo;
        private IRepository<Report> reportRepo;
        private ACTesterContext context;

        public UnitOfWork()
        {
            this.context = new ACTesterContext();
        }

        public IRepository<AirConditioner> AirConditionerRepo
        {
            get
            {
                return airConditionerRepo ?? (this.airConditionerRepo = new Repository<AirConditioner>(this.context.AirConditioners));
            }

            set
            {
                airConditionerRepo = value;
            }
        }

        public IRepository<Report> ReportRepo
        {
            get
            {
                return reportRepo ?? (this.reportRepo = new Repository<Report>(this.context.Reports));
            }

            set
            {
                reportRepo = value;
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
