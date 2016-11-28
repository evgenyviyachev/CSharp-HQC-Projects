namespace MassDefect.ConsoleDataImporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Models;
    using System.IO;
    using Newtonsoft.Json;
    using DTOModels;

    public class Program
    {
        public static void Main()
        {
            //IMPORTANT!
            //To run it -> CHANGE data source in all Connection Strings!

            ImportSolarSystems();
            ImportStars();
            ImportPlanets();
            ImportPersons();
            ImportAnomalies();
            ImportAnomalyVictims();
        }

        public static void ImportAnomalyVictims()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(Paths.AnomalyVictimsPath);

            var anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimsDTO>>(json);

            foreach (var anomalyVictim in anomalyVictims)
            {
                if (anomalyVictim.Id == null || anomalyVictim.Person == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                int anomalyId = int.Parse(anomalyVictim.Id);

                if (!context.Anomalies.Any(a => a.Id == anomalyId)
                    || !context.People.Any(p => p.Name == anomalyVictim.Person))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }                

                var personEntity = context.People
                    .First(p => p.Name == anomalyVictim.Person);

                var anomalyEntity = context.Anomalies.Find(anomalyId);

                personEntity.Anomalies.Add(anomalyEntity);
            }

            context.SaveChanges();
        }

        public static void ImportAnomalies()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(Paths.AnomaliesPath);

            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyDTO>>(json);

            foreach (var anomaly in anomalies)
            {
                if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                if (!context.Planets.Any(p => p.Name == anomaly.OriginPlanet)
                    || !context.Planets.Any(p => p.Name == anomaly.TeleportPlanet))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var originPlanet = context.Planets
                    .First(p => p.Name == anomaly.OriginPlanet);

                var teleportPlanet = context.Planets
                    .First(p => p.Name == anomaly.TeleportPlanet);

                var anomalyEntity = new Anomaly
                {
                    OriginPlanet = originPlanet,
                    TeleportPlanet = teleportPlanet
                };

                context.Anomalies.Add(anomalyEntity);
                Console.WriteLine("Successfully imported anomaly.");
            }

            context.SaveChanges();
        }

        public static void ImportPersons()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(Paths.PersonsPath);

            var persons = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);

            foreach (var person in persons)
            {
                if (person.Name == null || person.HomePlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                if (!context.Planets.Any(p => p.Name == person.HomePlanet))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var homePlanet = context.Planets
                    .First(p => p.Name == person.HomePlanet);

                var personEntity = new Person
                {
                    Name = person.Name,
                    HomePlanet = homePlanet
                };

                context.People.Add(personEntity);
                Console.WriteLine($"Successfully imported {personEntity.GetType().Name} {personEntity.Name}.");
            }

            context.SaveChanges();
        }

        public static void ImportPlanets()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(Paths.PlanetsPath);

            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);

            foreach (var planet in planets)
            {
                if (planet.Name == null || planet.Sun == null || planet.SolarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                if (!context.SolarSystems.Any(ss => ss.Name == planet.SolarSystem)
                    || !context.Stars.Any(s => s.Name == planet.Sun))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var solarSystem = context.SolarSystems
                    .First(ss => ss.Name == planet.SolarSystem);

                var sun = context.Stars
                    .First(s => s.Name == planet.Sun);

                var planetEntity = new Planet
                {
                    Name = planet.Name,
                    Sun = sun,
                    SolarSystem = solarSystem
                };

                context.Planets.Add(planetEntity);
                Console.WriteLine($"Successfully imported {planetEntity.GetType().Name} {planetEntity.Name}.");
            }

            context.SaveChanges();
        }

        public static void ImportStars()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(Paths.StarsPath);

            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);

            foreach (var star in stars)
            {
                if (star.Name == null || star.SolarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                if (!context.SolarSystems.Any(ss => ss.Name == star.SolarSystem))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var solarSystem = context.SolarSystems
                    .First(ss => ss.Name == star.SolarSystem);

                var starEntity = new Star
                {
                    Name = star.Name,
                    SolarSystem = solarSystem
                };

                context.Stars.Add(starEntity);
                Console.WriteLine($"Successfully imported {starEntity.GetType().Name} {starEntity.Name}.");
            }

            context.SaveChanges();
        }

        public static void ImportSolarSystems()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(Paths.SolarSystemsPath);

            var solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);

            foreach (var solarSystem in solarSystems)
            {
                if (solarSystem.Name == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var solarSystemEntity = new SolarSystem
                {
                    Name = solarSystem.Name
                };

                context.SolarSystems.Add(solarSystemEntity);
                Console.WriteLine($"Successfully imported {solarSystemEntity.GetType().Name} {solarSystemEntity.Name}.");
            }

            context.SaveChanges();
        }
    }
}
