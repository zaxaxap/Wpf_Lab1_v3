using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public partial struct VMAccuracy
    {
        public VMGrid net { get; set; }
        public float differ { get; set; }
        public float arg { get; set; }
        public float HA { get; set; }
        public float EP { get; set; }
        public VMAccuracy(int nodes, float start, float end, VMf func_type, float differ, float arg, float HA, float EP)
        {
            net = new VMGrid(nodes, start, end, func_type);
            this.differ = differ;
            this.arg = arg;
            this.HA = HA;
            this.EP = EP;
        }
        override public string ToString()
        {
            return "Start: " + net.start + " End: " + net.end + " Step: " + net.step + " Type: " + net.func_type + "\nDifference: " + differ + "\n Argument: " + arg + " HA: " + HA + " EP: " + EP;
        }

    }
}
