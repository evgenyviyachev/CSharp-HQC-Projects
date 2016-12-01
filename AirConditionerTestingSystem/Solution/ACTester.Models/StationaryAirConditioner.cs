namespace ACTester.Models
{
    using Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Utilities.Enumerations;

    [Table("StationaryAirConditioners")]
    public class StationaryAirConditioner : AirConditioner
    {
        [Required, PowerUsage]
        public int PowerUsage { get; set; }

        [Required]
        public EnergyEfficiencyRating RequiredEnergyEfficiencyRating { get; set; }
    }
}
