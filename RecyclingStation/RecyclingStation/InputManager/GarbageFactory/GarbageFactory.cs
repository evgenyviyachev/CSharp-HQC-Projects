namespace RecyclingStationNS.InputManager.GarbageFactoryNS
{
    using Contracts;
    using System;
    using System.Linq;
    using System.Reflection;
    using WasteDisposal.Interfaces;

    public class GarbageFactory : IGarbageFactory
    {
        public IWaste CreateGarbage(string type, string name, double weight, double volumePerKg)
        {
            var typeOfGarbage = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .First(x => x.Name.EndsWith("Garbage") && x.Name.StartsWith(type));

            var constructor = typeOfGarbage
                .GetConstructor(new Type[] { typeof(string), typeof(double), typeof(double) });

            var paramsForConstruction = new object[]
            {
                name, weight, volumePerKg
            };

            IWaste garbage = (IWaste)constructor.Invoke(paramsForConstruction);

            return garbage;
        }
    }
}
