namespace MiniORM.Attributes
{
    using System;
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class IdAttribute : Attribute
    {
    }
}
