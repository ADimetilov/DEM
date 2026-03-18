using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEM
{
    public class PostTemplate
    {
        public string name { get; set; }
        public int id { get; set; }
        public override string ToString() => name;
    }
}
