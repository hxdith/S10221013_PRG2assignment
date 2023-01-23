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
    internal class Stay
    {
        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        public List<Room> RoomList { get; set; }

        public Stay() { }

        public Stay(DateTime checkin, DateTime checkout)
        {
            CheckinDate = checkin;
            CheckoutDate = checkout;
        }
        public void AddRoom(Room room)
        {
            RoomList.Add(room);

        }

        public double CalculateTotal()
        {

            foreach (Room room in RoomList)
            {
                double total = +room.CalculateCharges() * (CheckinDate.Subtract(CheckoutDate).Days);
                return total;
            }
            return 0;
        }

        public override string ToString()
        {
            return $"Check-in date: {CheckinDate} \t Check-out date: {CheckoutDate}";
        }
    }
}
