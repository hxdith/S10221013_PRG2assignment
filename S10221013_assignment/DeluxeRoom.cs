using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10221013_PRG2Assignment
{
    internal class DeluxeRoom : Room
    {
        public bool AdditionalBed { get; set; }

        public DeluxeRoom() { }

        public DeluxeRoom(int roomNo, string bedconfig, double rate, bool avail) : base(roomNo, bedconfig, rate, avail)
        {
        }

        public override double CalculateCharges()
        {
            double total = 0;
            if (AdditionalBed == true)
            {
                total = +20;
            }
            total = DailyRate + total;
            /* if (BedConfiguration == "single")
            {
                total = +90;
            }
            else if (BedConfiguration == "twin")
            {
                total = +110;
            }
            else if (BedConfiguration == "triple")
            {
                total = +120;
            } */
            return total;
        }

        public override string ToString()
        {
            return string.Format("Room Number: {0} Bed Configuration: {1} Daily Rate: {2} Available?: {3}", RoomNumber, BedConfiguration, DailyRate, IsAvail);
        }
    }
}
