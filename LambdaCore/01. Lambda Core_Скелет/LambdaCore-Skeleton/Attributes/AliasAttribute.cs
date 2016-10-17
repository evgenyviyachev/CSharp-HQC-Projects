namespace LambdaCore_Skeleton.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public class AliasAttribute : Attribute
    {
        public AliasAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public override bool Equals(object obj)
        {
            return this.Name.Equals(obj);
        }
    }
}
