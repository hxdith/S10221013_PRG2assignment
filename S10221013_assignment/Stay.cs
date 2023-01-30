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
        }d
        public void AddRoom(Room room)
        { 
            if (RoomList == null) //if not empty create new list
            {
                RoomList = new List<Room>();
            }
            RoomList.Add(room);

        }

        public double CalculateTotal()
        {
            double total = 0;
            if(RoomList.Count <= 0)
            {
                foreach (Room room in RoomList)
                {
                    if (room.IsAvail == false) //runs check if an available room is added inside by accident
                    {
                        total = +room.CalculateCharges() * (CheckinDate.Subtract(CheckoutDate).Days);
                    }


                } 
            }
            return total;
        }

        public override string ToString()
        {
            return $"Check-in date: {CheckinDate} \t Check-out date: {CheckoutDate}";
        }
    }
}
