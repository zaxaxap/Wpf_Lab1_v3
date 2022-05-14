using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public enum VMf
    {
        vmsLn,
        vmdLn,
        vmsLGamma,
        vmdLGamma
    }
    public partial class VMGrid
    {
        public int nodes { get; set; }
        public float start { get; set; }
        public float end { get; set; }
        public float step { get;}
        public VMf func_type { get; set; }
        public VMGrid() { }
        public VMGrid(int nodes, float start, float end, VMf func_type) {
            this.start = start; 
            this.end = end;
            this.func_type = func_type;
            this.nodes = nodes;
            this.step = (start - end) / nodes;
            if (step < 0) { step = -step; }
        }

    }
}
