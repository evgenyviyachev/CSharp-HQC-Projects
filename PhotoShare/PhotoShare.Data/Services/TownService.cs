namespace PhotoShare.Data.Services
{
    using Contracts;
    using Models;
    using System;
    using System.Linq;

    public class TownService
    {
        private IUnitOfWork unitOfWork;

        public TownService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddTown(Town town)
        {
            var sameTowns = this.unitOfWork.Towns
                .FindAll(t => t.Name == town.Name && t.Country == town.Country);

            if (sameTowns.Count() == 0)
            {
                this.unitOfWork.Towns.Add(town);
                this.unitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Already registered this town!");
            }
        }
    }
}
