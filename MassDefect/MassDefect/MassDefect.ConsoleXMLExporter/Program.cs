namespace MassDefect.ConsoleXMLExporter
{
    using System.Linq;
    using Data;
    using Models;
    using System.Xml.Linq;

    public class Program
    {
        private const string anomaliesPath = "../../../../../Output/anomalies.xml";

        public static void Main()
        {
            //IMPORTANT!
            //To run it -> CHANGE data source in all Connection Strings!

            var context = new MassDefectContext();
            var exportedAnomalies = context.Anomalies
                .Select(a => new
                {
                    id = a.Id,
                    originPlanetName = a.OriginPlanet.Name,
                    teleportPlanetName = a.TeleportPlanet.Name,
                    victims = a.People.Select(p => new
                    {
                        p.Name
                    }).ToList()
                })
                .OrderBy(a => a.id);

            var xmlDoc = new XElement("anomalies");

            foreach (var anomaly in exportedAnomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", anomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", anomaly.originPlanetName));
                anomalyNode.Add(new XAttribute("teleport-planet", anomaly.teleportPlanetName));

                var victimsNode = new XElement("victims");

                foreach (var victim in anomaly.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim.Name));
                    victimsNode.Add(victimNode);
                }

                anomalyNode.Add(victimsNode);
                xmlDoc.Add(anomalyNode);
            }

            xmlDoc.Save(anomaliesPath);
        }
    }
}
