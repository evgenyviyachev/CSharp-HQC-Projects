namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class AddTownCommand : Command
    {
        [Inject]
        private TownService townService;

        public AddTownCommand(string[] data)
            : base(data)
        {
        }

        //AddTown <townName> <countryName>
        public override string Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string townName = Data[1];
            string country = Data[2];

            Town town = new Town()
            {
                Name = townName,
                Country = country
            };

            this.townService.AddTown(town);

            return townName + " was added to database";
        }
    }
}
