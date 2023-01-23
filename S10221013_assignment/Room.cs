//==========================================================
// Student Number : S10221013
// Student Name : Hadith Mukrish
//==========================================================



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10221013_PRG2Assignment
{
    abstract class Room
    {
        public int RoomNumber { get; set; }

        public string BedConfiguration { get; set; }

        public double DailyRate { get; set; }

        public bool IsAvail { get; set; }

        public Room() { }

        public Room(int roomNumber, string bedConfiguration, double dailyRate, bool isAvail)
        {
            RoomNumber = roomNumber;
            BedConfiguration = bedConfiguration.ToLower();
            DailyRate = dailyRate;
            IsAvail = isAvail;
        }


        public abstract double CalculateCharges();

        public override string ToString()
        {
            return string.Format("Room Number: {0} Bed Configuration: {1} Daily Rate: {2}", RoomNumber, BedConfiguration, DailyRate);
        }
    }
}
