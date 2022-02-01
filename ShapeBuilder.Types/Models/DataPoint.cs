using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeBuilder.Types.Models
{
    public class DataPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double R1 { get; set; }
        public double R2 { get; set; }
        public Dictionary<string, int> D { get; set; }
    }
}
