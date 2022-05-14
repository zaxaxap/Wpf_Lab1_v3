using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public partial struct VMTime
    {
        public VMGrid net { get; set; }
        public float HA_time { get; set; }
        public float EP_time { get; set; }
        public float coef { get; set; }
        public VMTime(int nodes, float start, float end, VMf func_type, float HA_time, float EP_time, float coef)
        {
            net = new VMGrid(nodes, start, end, func_type);
            this.HA_time = HA_time;
            this.EP_time = EP_time;
            this.coef = coef;
        }
        override public string ToString()
        {
            return "Start: " + net.start + " End: " + net.end + " Step: " + net.step + " Type: " + net.func_type + "\nHA: " + HA_time + " EP: " + EP_time + " Coef: " + coef;
        }
    
    }
}
