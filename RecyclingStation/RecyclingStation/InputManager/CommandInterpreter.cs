namespace RecyclingStationNS.InputManager
{
    using GarbageFactoryNS;
    using RecyclingStationNS.Contracts;
    using RecyclingStationNS.Database;
    using RecyclingStationNS.WasteDisposal;
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public class CommandInterpreter : ICommandInterpreter
    {
        private IRecyclingStation rs;
        private IGarbageProcessor gp;
        private IGarbageFactory gf;

        public CommandInterpreter(IRecyclingStation rs, IGarbageProcessor gp, IGarbageFactory gf)
        {
            this.rs = rs;
            this.gp = gp;
            this.gf = gf;
        }

        public CommandInterpreter() 
            : this(new RecyclingStation(), new GarbageProcessor(), new GarbageFactory())
        {
        }

        public string InterpretCommand(string[] data)
        {
            if (data.Length == 1)
            {
                return this.rs.ToString();
            }
            else if (data[0] == "ProcessGarbage")
            {
                string[] garbageInfo = data[1].Split('|');
                string garbageName = garbageInfo[0];
                double garbageWeight = double.Parse(garbageInfo[1]);
                double garbageVolumePerKg = double.Parse(garbageInfo[2]);
                string type = garbageInfo[3];

                var garbage = this.gf.CreateGarbage(type, garbageName, garbageWeight, garbageVolumePerKg);

                IProcessingData processingData = this.gp.ProcessWaste(garbage);

                bool success = this.rs.UpdateData(processingData);

                if (success)
                {
                    return $"{garbage.Weight:F2} kg of {garbage.Name} successfully processed!";
                }
                else
                {
                    return "Processing Denied!";
                }
            }
            else
            {
                string[] requirementInfo = data[1].Split('|');
                double energyBalanceMin = double.Parse(requirementInfo[0]);
                double capitalBalanceMin = double.Parse(requirementInfo[1]);
                string garbageTypeToCheck = requirementInfo[2];

                return this.rs
                    .ChangeGarbageToCheck(garbageTypeToCheck, energyBalanceMin, capitalBalanceMin);
            }
        }
    }
}
