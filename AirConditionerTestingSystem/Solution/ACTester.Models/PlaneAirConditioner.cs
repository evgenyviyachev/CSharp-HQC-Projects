namespace ACTester.Models
{
    using Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PlaneAirConditioner")]
    public class PlaneAirConditioner : VehicleAirConditioner
    {
        [Required, ElectricityUsed]
        public int ElectricityUsed { get; set; }
    }
}
