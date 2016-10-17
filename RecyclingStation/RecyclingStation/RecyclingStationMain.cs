using RecyclingStationNS.Contracts;
using RecyclingStationNS.Database;
using RecyclingStationNS.InputManager;
using RecyclingStationNS.InputManager.GarbageFactoryNS;
using RecyclingStationNS.WasteDisposal;
using RecyclingStationNS.WasteDisposal.Interfaces;

namespace RecyclingStationNS
{
    public class RecyclingStationMain
    {
        public static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
