namespace ACTester.Core
{
    using System;
    using Interfaces;
    using UI;
    using AutoMapper;
    using Models;
    using ViewModels;

    public class Engine
    {
        public Engine(IActionManager actionManager, IUserInterface userInterface)
        {
            this.ActionManager = actionManager;
            this.UserInterface = userInterface;
        }

        public Engine()
            : this(new ActionManager(), new ConsoleUserInterface())
        {
        }

        public IActionManager ActionManager { get; private set; }

        public IUserInterface UserInterface { get; private set; }

        public void Run()
        {
            while (true)
            {
                /// Using AutoMapper
                ConfigureMapping();
                string line = this.UserInterface.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                line = line.Trim();
                try
                {
                    var command = new Command(line);
                    string commandResult = this.ActionManager.ExecuteCommand(command);
                    this.UserInterface.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    this.UserInterface.WriteLine(ex.Message);
                }
            }
        }

        private void ConfigureMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AirConditioner, AirConditionerDTO>()
                    .Include<StationaryAirConditioner, StationaryAirConditionerDTO>()
                    .Include<VehicleAirConditioner, VehicleAirConditionerDTO>();

                cfg.CreateMap<StationaryAirConditioner, StationaryAirConditionerDTO>();

                cfg.CreateMap<VehicleAirConditioner, VehicleAirConditionerDTO>()
                    .Include<PlaneAirConditioner, PlaneAirConditionerDTO>()
                    .Include<CarAirConditioner, CarAirConditionerDTO>();

                cfg.CreateMap<PlaneAirConditioner, PlaneAirConditionerDTO>();
                cfg.CreateMap<CarAirConditioner, CarAirConditionerDTO>();

                cfg.CreateMap<Report, ReportDTO>();
            });
        }
    }
}
