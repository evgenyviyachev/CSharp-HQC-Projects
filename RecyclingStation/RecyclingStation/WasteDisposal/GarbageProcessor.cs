namespace RecyclingStationNS.WasteDisposal
{
    using System;
    using System.Linq;
    using RecyclingStationNS.WasteDisposal.Attributes;
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public class GarbageProcessor : IGarbageProcessor
    {
        public GarbageProcessor(IStrategyHolder strategyHolder)
        {
            this.StrategyHolder = strategyHolder;
        }

        public GarbageProcessor() 
            : this(new StrategyHolder())
        {
        }

        public IStrategyHolder StrategyHolder { get; private set; }

        public IProcessingData ProcessWaste(IWaste garbage)
        {
            Type type = garbage.GetType();

            var disposableAttribute = (DisposableAttribute)type
                .GetCustomAttributes(true)
                .FirstOrDefault();

            IGarbageDisposalStrategy currentStrategy;

            if (disposableAttribute == null 
                || !this.StrategyHolder
                .GetDisposalStrategies
                .TryGetValue(disposableAttribute.GetType(), out currentStrategy))
            {
                throw new ArgumentException(
                    "The passed in garbage does not implement a supported Disposable Strategy Attribute.");
            }

            return currentStrategy.ProcessGarbage(garbage);
        }
    }
}
