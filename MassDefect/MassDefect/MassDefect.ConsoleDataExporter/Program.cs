namespace MassDefect.ConsoleDataExporter
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Program
    {
        private const string planetsNotOriginsPath = "../../../../../Output/planetsNotOrigin.json";

        private const string peopleNotBeenVictimsPath = "../../../../../Output/peopleNotBeenVictims.json";

        private const string topAnomalyPath = "../../../../../Output/topAnomaly.json";

        public static void Main()
        {
            //IMPORTANT!
            //To run it -> CHANGE data source in all Connection Strings!

            var context = new MassDefectContext();

            ExportPlanetsWhichAreNotAnomalyOrigins(context);

            ExportPeopleWhoHaveNotBeenVictims(context);

            ExportTopAnomaly(context);
        }

        private static void ExportTopAnomaly(MassDefectContext context)
        {
            var topAnomaly = context.Anomalies
                .OrderByDescending(a => a.People.Count)
                .Select(a => new
                {
                    id = a.Id,
                    originPlanet = new List<Planet> { a.OriginPlanet }
                    .Select(p => new
                    {
                        name = p.Name
                    }),
                    teleportPlanet = new List<Planet> { a.TeleportPlanet }
                    .Select(p => new
                    {
                        name = p.Name
                    }),
                    victimsCount = a.People.Count
                })
                .Take(1);

            var topAnomalyJson = JsonConvert
                .SerializeObject(topAnomaly, Formatting.Indented);

            File.WriteAllText(topAnomalyPath, topAnomalyJson);
        }

        private static void ExportPeopleWhoHaveNotBeenVictims(MassDefectContext context)
        {
            var exportedPeople = context.People
                .Where(p => !p.Anomalies.Any())
                .Select(p => new
                {
                    name = p.Name,
                    homePlanet = new List<Planet> { p.HomePlanet }
                    .Select(hp => new
                    {
                        name = hp.Name
                    })
                });

            var peopleAsJson = JsonConvert
                .SerializeObject(exportedPeople, Formatting.Indented);

            File.WriteAllText(peopleNotBeenVictimsPath, peopleAsJson);
        }

        private static void ExportPlanetsWhichAreNotAnomalyOrigins(MassDefectContext context)
        {
            var exportedPlanets = context.Planets
                .Where(p => !p.OriginAnomalies.Any())
                .Select(p => new
                {
                    name = p.Name
                });

            var planetsAsJson = JsonConvert
                .SerializeObject(exportedPlanets, Formatting.Indented);

            File.WriteAllText(planetsNotOriginsPath, planetsAsJson);
        }
    }
}
