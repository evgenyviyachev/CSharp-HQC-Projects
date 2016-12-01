﻿namespace ACTester.Models
{
    using Utilities.Enumerations;

    public class Report
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public Mark Mark { get; set; }
    }
}
