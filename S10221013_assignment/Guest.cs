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
    internal class Guest
    {
        public string Name { get; set; }

        public string PassportNum { get; set; }

        public Stay HotelStay { get; set; }

        public Membership Member { get; set; }

        public bool isCheckedin { get; set; }

        public Guest() { }

        public Guest(string name, string passportNum, Stay hotelStay, Membership member)
        {
            Name = name;
            PassportNum = passportNum;
            HotelStay = hotelStay;
            Member = member;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} Passport Number: {1}  {2}  {3}", Name, PassportNum, Member, HotelStay);
        }
    }
}
