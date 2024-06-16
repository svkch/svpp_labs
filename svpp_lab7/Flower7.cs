using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace svpp_lab7
{
    public class Flower7
    {
        [Key]
        public int FlowerID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Color { get; set; }
        public double FlowerPrice { get; set; }
    }
}
