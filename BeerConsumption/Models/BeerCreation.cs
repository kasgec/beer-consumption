using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerConsumption.Models
{
    public class BeerCreation
    {
        public string Title { get; set; }
        public bool NonAlcohol { get; set; }
        public decimal Volume { get; set; }
        public int Quantity { get; set; }
    }
}