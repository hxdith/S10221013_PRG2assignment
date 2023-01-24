using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10221013_PRG2Assignment
{
    internal class Membership
    {
        public string Status { get; set; }

        public int Points { get; set; }

        public Membership() { }

        public Membership(string status, int points)
        {
            string memberstatus = status.ToLower();
            Status = char.ToUpper(memberstatus[0]) + memberstatus.Substring(1);
            Points = points;
        }

        public double EarnPoints(double total)
        {
            Points += Convert.ToInt32(total)/10;
            //check if point is more than the threshold of memberstatus, if yes, then change to the next tier.
            if (Points >= 100)
            {
                Status = "Silver";
            }
            if (Points >= 200)
            {
                Status = "Gold";
            }
            
            return Points;
        }

        public bool RedeemPoints(int pointsRedeem)
        {
            if (Points >= pointsRedeem)
            {
                Points = Points - pointsRedeem;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format("Membership status: {0} Points: {1}", Status, Points);
        }

    }
}
