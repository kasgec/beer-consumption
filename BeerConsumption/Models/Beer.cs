using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerConsumption.Models
{
    public class Beer
    {
        public string BeerId { get; set; }
        public string Title { get; set; }
        public bool NonAlcohol { get; set; }
        public decimal Volume { get; set; }
        public int Quantity { get; set; }
    }
}