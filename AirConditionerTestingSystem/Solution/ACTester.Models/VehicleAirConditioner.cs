namespace ACTester.Models
{
    using Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VehicleAirConditioners")]
    public abstract class VehicleAirConditioner : AirConditioner
    {
        [Required, VolumeCovered]
        public int VolumeCovered { get; set; }
    }
}
