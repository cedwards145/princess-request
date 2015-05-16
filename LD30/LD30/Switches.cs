using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD30
{
    public class Switches
    {
        private static bool[] switches = new bool[255];

        public static bool getSwitch(int id)
        {
            return switches[id];
        }

        public static void setSwitch(int id, bool value)
        {
            switches[id] = value;
            update(id);
        }

        public static void load()
        {
            switches[196] = true;
            switches[197] = true;
            switches[198] = true;
            switches[199] = true;
            switches[200] = true;
            switches[201] = true;
            switches[202] = true;
            switches[203] = true;
            switches[204] = true;
            switches[205] = true;

            switches[207] = true;
        }

        public static void update(int id)
        {
            if (id == 23)
                switches[206] = switches[23];
            else if (id == 24)
                switches[208] = switches[24];
            else if (id == 28)
            {
                switches[209] = switches[28];
                switches[210] = switches[28];
            }
        }
    }
}
