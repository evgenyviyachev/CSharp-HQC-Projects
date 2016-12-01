namespace ACTester.Models
{
    using Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class AirConditioner
    {
        public int Id { get; set; }

        [Required, Manufacturer]
        public string Manufacturer { get; set; }

        [Required, Model]
        public string Model { get; set; }
    }
}
