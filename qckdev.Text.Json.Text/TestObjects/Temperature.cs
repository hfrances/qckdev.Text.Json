using System;
using System.Collections.Generic;
using System.Text;

namespace qckdev.Text.Json.Test.TestObjects
{
    sealed class Temperature
    {
        public DateTime Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
    }
}
