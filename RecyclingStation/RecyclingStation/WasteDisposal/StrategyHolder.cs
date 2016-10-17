namespace RecyclingStationNS.WasteDisposal
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using RecyclingStationNS.WasteDisposal.Interfaces;
    using Attributes;

    public class StrategyHolder : IStrategyHolder
    {
        private readonly IDictionary<Type, IGarbageDisposalStrategy> strategies;

        public StrategyHolder()
        {
            this.strategies = new Dictionary<Type, IGarbageDisposalStrategy>();
            this.AddAll();
        }

        public IReadOnlyDictionary<Type, IGarbageDisposalStrategy> GetDisposalStrategies
        {
            get
            {
                return (IReadOnlyDictionary<Type, IGarbageDisposalStrategy>)this.strategies;
            }
        }

        private void AddAll()
        {
            var attributes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(DisposableAttribute)) != null
                && t.Name.EndsWith("Attribute") 
                && !t.Name.StartsWith("Disposable"));

            var typesOfStrategies = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.Name.EndsWith("Strategy")
                && !x.Name.StartsWith("IGarbage"));

            foreach (var attr in attributes)
            {
                string attrName = attr.Name.Replace("Attribute", string.Empty);

                var strategyType = typesOfStrategies
                    .FirstOrDefault(s => s.Name.StartsWith(attrName));

                if (strategyType != null)
                {
                    var strategy = (IGarbageDisposalStrategy)Activator
                        .CreateInstance(strategyType);

                    this.AddStrategy(attr, strategy);
                }
            }
        }

        public bool AddStrategy(Type disposableAttribute, IGarbageDisposalStrategy strategy)
        {
            if (!this.strategies.Keys.Contains(disposableAttribute))
            {
                this.strategies.Add(disposableAttribute, strategy);
                return true;
            }

            return false;
        }

        public bool RemoveStrategy(Type disposableAttribute)
        {
            if (!this.strategies.Keys.Contains(disposableAttribute))
            {
                return false;
            }

            this.strategies.Remove(disposableAttribute);
            return true;
        }
    }
}
