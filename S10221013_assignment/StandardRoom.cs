using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10221013_PRG2Assignment
{
    internal class StandardRoom : Room
    {
        public bool RequireWifi { get; set; }

        public bool RequireBreakfast { get; set; }

        public StandardRoom() { }

        public StandardRoom(int roomNo, string bedconfig, double rate, bool avail) : base(roomNo, bedconfig, rate, avail)
        {

        }
        public override double CalculateCharges()
        {
            double total = 0;
            if (RequireWifi == true)
            {
                total = total + 10;
            }
            if (RequireBreakfast == true)
            {
                total = total +20;
            }

            total = DailyRate + total;
            /* if (BedConfiguration == "single")
            {
                total =total +90;
            }
            else if (BedConfiguration == "twin")
            {
                total = total +110;
            }
            else if (BedConfiguration == "triple")
            {
                total = total +120;
            } */
            return total;
        }
        public override string ToString()
        {
            return string.Format("Room Number: {0} Bed Configuration: {1} Daily Rate: {2} Available?: {3}", RoomNumber, BedConfiguration, DailyRate, IsAvail);
        }
    }
}
