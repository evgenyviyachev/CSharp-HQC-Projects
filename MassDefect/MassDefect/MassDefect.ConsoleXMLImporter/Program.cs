namespace MassDefect.ConsoleXMLImporter
{
    using Data;
    using Models;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using System;
    using System.Linq;

    public class Program
    {
        private const string AnomalyVictimsPath = "../../../../Resources/datasets/new-anomalies.xml";

        public static void Main()
        {
            //IMPORTANT!
            //To run it -> CHANGE data source in all Connection Strings!

            var xml = XDocument.Load(AnomalyVictimsPath);
            var anomalies = xml.XPathSelectElements("anomalies/anomaly");

            var context = new MassDefectContext();
            foreach (var anomaly in anomalies)
            {
                ImportAnomalyAndVictim(anomaly, context);
            }
        }

        private static void ImportAnomalyAndVictim(XElement anomaly, MassDefectContext context)
        {
            var originPlanetName = anomaly.Attribute("origin-planet");
            var teleportPlanetName = anomaly.Attribute("teleport-planet");

            if (originPlanetName == null || teleportPlanetName == null)
            {
                Console.WriteLine("Error: Invalid data.");
                return;
            }

            if (!context.Planets.Any(p => p.Name == originPlanetName.Value)
                || !context.Planets.Any(p => p.Name == teleportPlanetName.Value))
            {
                Console.WriteLine("Error: Invalid data.");
                return;
            }

            var originPlanet = context.Planets
                .First(p => p.Name == originPlanetName.Value);
            var teleportPlanet = context.Planets
                .First(p => p.Name == teleportPlanetName.Value);

            var anomalyEntity = new Anomaly
            {
                OriginPlanet = originPlanet,
                TeleportPlanet = teleportPlanet
            };

            context.Anomalies.Add(anomalyEntity);
            Console.WriteLine("Successfully imported anomaly.");

            var victims = anomaly.XPathSelectElements("victims/victim");

            foreach (var victim in victims)
            {
                ImportVictim(victim, context, anomalyEntity);
            }

            context.SaveChanges();
        }

        private static void ImportVictim(XElement victim, MassDefectContext context, Anomaly anomalyEntity)
        {
            var name = victim.Attribute("name");

            if (name == null)
            {
                Console.WriteLine("Error: Invalid data.");
                return;
            }

            if (!context.People.Any(p => p.Name == name.Value))
            {
                Console.WriteLine("Error: Invalid data.");
                return;
            }

            var personEntity = context.People
                .First(p => p.Name == name.Value);

            anomalyEntity.People.Add(personEntity);
        }
    }
}
