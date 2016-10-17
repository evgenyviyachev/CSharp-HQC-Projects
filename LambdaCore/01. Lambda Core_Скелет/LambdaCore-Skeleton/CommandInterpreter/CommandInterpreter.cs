namespace LambdaCore_Skeleton.CommandInterpreter
{
    //using System;
    using System.Linq;
    using Attributes;
    using System.Reflection;
    using LambdaCore_Skeleton.Contracts;
    using DatabaseNS;

    public class CommandInterpreter : ICommandInterpreter
    {
        private IDatabase db = new Database();

        public string InterpretCommand(string name, string[] parameters)
        {
            //switch (name)
            //{
            //    case "CreateCore":
            //        return this.db.CreateCore(parameters[0], int.Parse(parameters[1]));
            //    case "RemoveCore":
            //        return this.db.RemoveCore(parameters[0]);
            //    case "SelectCore":
            //        return this.db.SelectCore(parameters[0]);
            //    case "AttachFragment":
            //        return this.db.AttachFragment(parameters[0], parameters[1], int.Parse(parameters[2]));
            //    case "DetachFragment":
            //        return this.db.DetachFragment();
            //    case "Status":
            //        return this.db.Status();
            //    default:
            //        throw new Exception();
            //}

            object[] parametersForConstruction = new object[]
            {
                parameters
            };

            //Without attributes:
            //
            //MethodInfo method = this.db.GetType()
            //   .GetMethod(name, BindingFlags.Instance | BindingFlags.Public);

            var method = (typeof(Database))
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(m => m.GetCustomAttributes(typeof(AliasAttribute))
                .Where(atr => atr.Equals(name))
                .ToArray().Length > 0);

            string result = method.Invoke(this.db, parametersForConstruction).ToString();

            return result;
        }
    }
}
