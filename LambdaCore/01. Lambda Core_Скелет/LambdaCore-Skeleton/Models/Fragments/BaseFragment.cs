namespace LambdaCore_Skeleton.Models.Fragments
{
    using System;
    using LambdaCore_Skeleton.Enums;
    using Contracts;
    
    public abstract class BaseFragment : IFragment
    {
        private string name;
        private string type;
        private int pressureAffection;

        protected BaseFragment(string type, string name, int pressureAffection)
        {
            this.Type = type;
            this.Name = name;
            this.PressureAffection = pressureAffection;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            protected set
            {
                this.name = value;
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }
            protected set
            {
                this.type = value;
            }
        }

        public virtual int PressureAffection
        {
            get
            {
                return this.pressureAffection;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                this.pressureAffection = value;
            }
        }
    }
}
