namespace LambdaCore_Skeleton.Models.Cores
{
    using System;
    using System.Text;
    using LambdaCore_Skeleton.Contracts;
    using Enums;
    using Collection;
    
    public abstract class BaseCore : ICore
    {
        private readonly int initialDurability;
        private string type;
        private int durability;
        private LStack fragments;
        private CoreState state;
        private int pressureOnCore;
        private string name;

        protected BaseCore(string type, int durability, string name)
        {
            this.Type = type;
            this.Name = name;
            this.initialDurability = durability;
            this.Durability = durability;
            this.fragments = new LStack();
            this.state = CoreState.NORMAL;
            this.pressureOnCore = 0;
        }

        public virtual int Durability
        {
            get
            {
                return this.durability;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                this.durability = value;
            }
        }

        public LStack Fragments
        {
            get
            {
                return this.fragments;
            }
        }

        public int PressureOnCore
        {
            get
            {
                return this.pressureOnCore;
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

        public CoreState State
        {
            get
            {
                return this.state;
            }
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

        public string AttachFragment(IFragment fragment)
        {
            this.fragments.Push(fragment);

            if (fragment.Type == FragmentType.Cooling.ToString())
            {
                this.pressureOnCore -= fragment.PressureAffection;
            }
            else if (fragment.Type == FragmentType.Nuclear.ToString())
            {
                this.pressureOnCore += fragment.PressureAffection;
            }

            this.CheckPressureAndAlterDurability();

            return $"Successfully attached Fragment {fragment.Name} to Core {this.Name}!";
        }
        
        public string DetachFragment()
        {
            if (this.fragments.IsEmpty())
            {
                throw new ArgumentOutOfRangeException();
            }

            IFragment fragment = this.fragments.Peek();
            this.fragments.Pop();

            if (fragment.Type == FragmentType.Cooling.ToString())
            {
                this.pressureOnCore += fragment.PressureAffection;
            }
            else if (fragment.Type == FragmentType.Nuclear.ToString())
            {
                this.pressureOnCore -= fragment.PressureAffection;
            }

            this.CheckPressureAndAlterDurability();

            return $"Successfully detached Fragment {fragment.Name} from Core {this.Name}!";
        }

        private void CheckPressureAndAlterDurability()
        {
            if (IsPressureAboveZero())
            {
                this.state = CoreState.CRITICAL;
                this.durability = this.initialDurability - this.pressureOnCore;

                if (this.durability < 0)
                {
                    this.durability = 0;
                }
            }
            else
            {
                //this.durability = this.initialDurability;
                this.state = CoreState.NORMAL;
            }
        }

        private bool IsPressureAboveZero()
        {
            return this.pressureOnCore > 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Core {this.Name}:");
            sb.AppendLine($"####Durability: {this.Durability}");
            sb.Append($"####Status: {this.State.ToString()}\n");

            return sb.ToString();
        }
    }
}
