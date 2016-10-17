namespace LambdaCore_Skeleton.DatabaseNS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Enums;
    using Models.Cores;
    using Models.Fragments;
    using Attributes;

    public class Database : IDatabase
    {
        private IList<ICore> db;
        private ICore selectedCore = null;
        private char currentName = 'A';

        public Database()
        {
            this.db = new List<ICore>();
        }

        public IList<ICore> DB
        {
            get
            {
                return this.db;
            }
        }

        private string CurrentName()
        {
            string name = this.currentName.ToString();
            this.currentName++;
            return name;
        }

        [Alias("CreateCore")]
        public string CreateCore(string[] parameters)
        {
            string type = parameters[0];
            int durability = int.Parse(parameters[1]);

            try
            {
                if (type != CoreType.Para.ToString()
                    && type != CoreType.System.ToString())
                {
                    throw new ArgumentException();
                }

                string coreName = string.Empty;
                ICore core = null;

                switch (type)
                {
                    case "Para":
                        coreName = this.CurrentName();
                        core = new ParaCore(type, durability, coreName);
                        break;
                    case "System":
                        coreName = this.CurrentName();
                        core = new SystemCore(type, durability, coreName);
                        break;
                }

                this.db.Add(core);

                return $"Successfully created Core {coreName}!";
            }
            catch (Exception)
            {
                return "Failed to create Core!";
            }
        }

        [Alias("RemoveCore")]
        public string RemoveCore(string[] parameters)
        {
            string name = parameters[0];

            try
            {
                ICore core = this.db.First(c => c.Name == name);
                this.db.Remove(core);

                if (this.selectedCore.Equals(core))
                {
                    this.selectedCore = null;
                }

                return $"Successfully removed Core {name}!";
            }
            catch (Exception)
            {
                return $"Failed to remove Core {name}!";
            }
        }

        [Alias("SelectCore")]
        public string SelectCore(string[] parameters)
        {
            string name = parameters[0];

            try
            {
                this.selectedCore = this.db.First(c => c.Name == name);
                return $"Currently selected Core {name}!";
            }
            catch (Exception)
            {
                return $"Failed to select Core {name}!";
            }
        }

        [Alias("AttachFragment")]
        public string AttachFragment(string[] parameters)
        {
            string type = parameters[0];
            string name = parameters[1];
            int pressureAffection = int.Parse(parameters[2]);

            try
            {
                if (type != FragmentType.Cooling.ToString()
                    && type != FragmentType.Nuclear.ToString())
                {
                    throw new ArgumentException();
                }
                IFragment fragment = null;

                switch (type)
                {
                    case "Cooling":
                        fragment = new CoolingFragment(type, name, pressureAffection);
                        break;
                    case "Nuclear":
                        fragment = new NuclearFragment(type, name, pressureAffection);
                        break;
                }
                
                return this.selectedCore.AttachFragment(fragment);
            }
            catch (Exception)
            {
                return $"Failed to attach Fragment {name}!";
            }
        }

        [Alias("DetachFragment")]
        public string DetachFragment(string[] parameters)
        {
            try
            {
                return this.selectedCore.DetachFragment();
            }
            catch (Exception)
            {
                return "Failed to detach Fragment!";
            }
        }

        [Alias("Status")]
        public string Status(string[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Lambda Core Power Plant Status:");
            int totalDurability = this.db.Sum(c => c.Durability);
            sb.AppendLine($"Total Durability: {totalDurability}");
            int totalCores = this.db.Count;
            sb.AppendLine($"Total Cores: {totalCores}");
            int totalFragments = this.db.Sum(c => c.Fragments.Count());
            sb.AppendLine($"Total Fragments: {totalFragments}");

            foreach (var core in this.db)
            {
                sb.Append(core.ToString());
            }
            
            sb.Length--;

            return sb.ToString();
        }
    }
}
