using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    public class AH64ADFData
    {
        private decimal frequency = 0.0m;
        public decimal Frequency {
            get
            {
                return frequency;
            }
            set
            {
                // Valid range 100 - 2199.5 kHz
                throw new NotImplementedException();
            }
        }

        private string identifier = null;
        public string Identifier
        {
            get
            {
                return identifier;
            }
            set
            {
                // Valid ID 1-3 alpha characters (no numbers, no other stuff)
                throw new NotImplementedException();
            }
        }


    }
}
