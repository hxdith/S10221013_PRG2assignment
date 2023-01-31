




//==========================================================
// Student Number : S10221013
// Student Name : Hadith Mukrish
//==========================================================



using S10221013_PRG2Assignment;
using System.Runtime.CompilerServices;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

string projectRoot = Directory.GetCurrentDirectory();
string filename = "Guests.csv";
string filename2 = "Stays.csv";
string filename3 = "Rooms.csv";
string Stayfile = Path.Combine(projectRoot, filename2);
string Guestfile = Path.Combine(projectRoot, filename);
string Roomfile = Path.Combine(projectRoot, filename3);

// reads the data from the files and store them in an array
string[] roomData = File.ReadAllLines(Roomfile);
string[] guestData = File.ReadAllLines(Guestfile);
string[] stayData = File.ReadAllLines(Stayfile);


//create all list

List<Guest> guestList = new List<Guest>();
List<Room> roomList = new List<Room>();
List<Stay> StayList = new List<Stay>();
initroomdata();
// This method intialize the guest list using the relevant information from the data files
void initroomdata()
{

    // read data from room data
    for (int i = 1; i < roomData.Length; i++)
    {
        string[] roominfo = roomData[i].Split(',');
        string roomtype = roominfo[0].ToLower();
        int roomnumber = 0;
        if (int.TryParse(roominfo[1], out roomnumber)){ }
        string bedconfig = roominfo[2];
        double dailyrate = double.Parse(roominfo[3]);
        bool availability = true;
        bool requirewifi = false;
        bool requirebreakfast = false ;
        bool requireadditionalbed = false;
        DateTime checkin;
        DateTime checkout;
        // acheck the availability of the room

        for (int j = 1; j < stayData.Length; j++)
        {

            string[] stayinfo = stayData[j].Split(','); 
            //default case
            int stayRoominfo1 = 0; // set placeholder value for the room cells
            int stayRoominfo2 = 0;
            if (int.TryParse(stayinfo[5], out stayRoominfo1) && (int.TryParse(stayinfo[9], out stayRoominfo2))) //check if the cell is empty, otherwise convert the cell the int * this is assuming only 2 rooms can be booked per person
            {
                 
            }

            if (roomnumber == stayRoominfo1) // checks if the room number is in a column in the stay data file.
            {

                //check the check-in column to see if its available. Checks the second column of the csv file (isCheckedin)
                if (stayinfo[2].ToLower() == "true") //to lower so that in the case where the text gets typed with the wrong capitalisation of letters, the code doesnt skip this step by default.
                {
                    // if it is present in the stay data list and the check-in column shows true, then the room is not available.
                    availability = false;

                }
                else if (stayinfo[2].ToLower() == "false")
                {
                    // if it is present in the stay data list and the check-in column shows false, then the room is available.
                    availability = true;
                }
                if (stayinfo[6] == "TRUE") //check wifi requirement
                {
                    requirewifi = true;
                }
                else
                {
                    requirewifi = false;
                }
                
                if (stayinfo[7].ToLower() == "true") //check breakfast requirement
                {
                    requirebreakfast = true;

                }
                else
                {
                    requirebreakfast = false;
                }
                
                if (stayinfo[8] != null && stayinfo[8] != "")
                {
                    if (stayinfo[8].ToLower() == "true") //check breakfast requirement
                    {
                        requireadditionalbed = true;

                    }
                    else
                    {
                        requireadditionalbed = false;
                    }
                }


            }
            if (roomnumber == stayRoominfo2)
            {
                //check the check-in column to see if its available. Checks the second column of the csv file (isCheckedin)
                if (stayinfo[2].ToLower() == "true") //to lower so that in the case where the text gets typed with the wrong capitalisation of letters, the code doesnt skip this step by default.
                {

                    // if it is present in the stay data list and the check-in column shows true, then the room is not available.
                    availability = false;

                }
                else if (stayinfo[2].ToLower() == "false")
                {
                    // if it is present in the stay data list and the check-in column shows false, then the room is available.
                    availability = true;

                }
                if (stayinfo[10] != null && stayinfo[10] != "")
                {
                    if (stayinfo[10].ToLower() == "true") //check wifi requirement
                    {
                        requirewifi = true;
                    }
                    else
                    {
                        requirewifi = false;
                    }
                }


                if (stayinfo[11] != null && stayinfo[11] != "")
                {
                    if (stayinfo[11].ToLower() == "true") //check breakfast requirement
                    {
                        requirebreakfast = true;

                    }
                    else
                    {
                        requirebreakfast = false;
                    }
                }
                if (stayinfo[12] != null && stayinfo[12] != "")
                {
                    if (stayinfo[12].ToLower() == "true") //check breakfast requirement
                    {
                        requireadditionalbed = true;

                    }
                    else
                    {
                        requireadditionalbed = false;
                    }
                }
                break;

            }

        }

        //now create the room object
        if (roomtype == "standard")
        {
            //change the room data according to stay data


            {
                StandardRoom room = new StandardRoom(roomnumber, bedconfig, dailyrate, availability);
                room.RequireWifi = requirewifi;
                room.RequireBreakfast = requirebreakfast;
                roomList.Add(room);
                
            }
        }
        else if (roomtype == "deluxe")
        {
            DeluxeRoom room = new DeluxeRoom(roomnumber, bedconfig, dailyrate, availability);
            room.AdditionalBed = requireadditionalbed;
            roomList.Add(room);
        }

        
        
        

    }




    //display the content of the roomlist TESTING PURPOSES
    /* foreach (Room room in roomList)
    {
        if (room.IsAvail == false)
        {
            Console.WriteLine(room);
        }

    }
    */
}

initGuestData();
//method takes info from the data file and does the necessary comparing to generate a guest obj
void initGuestData()
{
    for (int i = 1; i < guestData.Length; i++)
    {
        string[] guestinfo = guestData[i].Split(',');
        string name = guestinfo[0];
        string passportNo = guestinfo[1];
        string membership = guestinfo[2];
        int points = 0;
        if (int.TryParse(guestinfo[3], out points)) { }
        for (int j = 1; j < stayData.Length; j++)
        {
            string[] gueststayinfo = stayData[j].Split(',');
            if (name == gueststayinfo[0] && passportNo == gueststayinfo[1])
            {
                DateTime checkin = DateTime.Parse(gueststayinfo[3]);
                DateTime checkout = DateTime.Parse(gueststayinfo[4]);
                int roomnumber = 0;
                int roomnumber2 = 0;
                if (int.TryParse(gueststayinfo[5], out roomnumber))
                {
                }
                if (int.TryParse(gueststayinfo[9], out roomnumber2))
                {
                }
                Stay stayduration = new Stay(checkin, checkout);
                if (roomnumber != 0)
                {
                    Room room = roomList.Find(x => x.RoomNumber == roomnumber);
                    //check whether the add ons are correct,
                    bool additionalwifi = Convert.ToBoolean(gueststayinfo[6].ToLower());
                    bool additionalbreakfast = Convert.ToBoolean(gueststayinfo[7].ToLower());
                    if (room != null && room.GetType() == typeof(DeluxeRoom))
                    {
                        bool additionalbed = Convert.ToBoolean(gueststayinfo[8].ToLower());
                        DeluxeRoom deluxeroom = new DeluxeRoom(room.RoomNumber, room.BedConfiguration, room.DailyRate, room.IsAvail); ;
                        deluxeroom.AdditionalBed = additionalbed;
                        stayduration.AddRoom(deluxeroom);
                    }
                    else if (room != null && room.GetType() == typeof(StandardRoom))
                    {
                        StandardRoom standardroom = new StandardRoom(room.RoomNumber, room.BedConfiguration, room.DailyRate, room.IsAvail);
                        standardroom.RequireBreakfast = additionalbreakfast;
                        standardroom.RequireWifi = additionalwifi;
                        stayduration.AddRoom(standardroom);
                    }


                }
                if (roomnumber2 != 0)
                {
                    Room room2 = roomList.Find(x => x.RoomNumber == roomnumber2);
                    //check whether the add ons are correct,
                    bool additionalwifi2 = Convert.ToBoolean(gueststayinfo[10].ToLower());
                    bool additionalbreakfast2 = Convert.ToBoolean(gueststayinfo[11].ToLower());
                    if (room2 != null && room2.GetType() == typeof(DeluxeRoom))
                    {
                        bool additionalbed2 = Convert.ToBoolean(gueststayinfo[12].ToLower());
                        DeluxeRoom deluxeroom2 = new DeluxeRoom(room2.RoomNumber, room2.BedConfiguration, room2.DailyRate, room2.IsAvail);
                        deluxeroom2.AdditionalBed = additionalbed2;
                        stayduration.AddRoom(deluxeroom2);
                    }
                    else if (room2 != null && room2.GetType() == typeof(StandardRoom))
                    {
                        StandardRoom standardroom2 = new StandardRoom(room2.RoomNumber, room2.BedConfiguration, room2.DailyRate,room2.IsAvail) ;
                        standardroom2.RequireWifi = additionalwifi2;
                        standardroom2.RequireBreakfast = additionalbreakfast2;
                        stayduration.AddRoom(standardroom2);
                    }
                }
                Guest guest = new Guest(name, passportNo, stayduration, new Membership(membership, points));
                guest.IsCheckedin = bool.Parse(gueststayinfo[2]);
                guestList.Add(guest);
                break;
            }
        }


    }

}
/* foreach (Guest guest in guestList)
{
    Console.WriteLine(guest);
}
*/

//Feature 1, display all guest
//displayAllGuest();
void displayAllGuest()
{
    
    Console.WriteLine($"\n|{"S/N",-4}|{"Guests",-15}|{"PassportNo",-10}|{"Membership Status",-15}|");
    Console.WriteLine("---------------------------------------------------------");
    int i = 1;
    foreach (Guest guest in guestList)
    {

        Console.WriteLine($"|{i,-4}|{guest.Name,-15}|{guest.PassportNum,-10}|{guest.Member.Status,-15}|");
        i = i + 1;
    }
}
void displayAllGuestLessInfo()
{
    Console.WriteLine($"\n{"S/N",-4}|{"Guests",-15}|{"PassportNo",-10}|");
    Console.WriteLine("---------------------------------------------------------");
    int i = 1;
    foreach (Guest guest in guestList)
    {

        Console.WriteLine($"|{i,-4}|{guest.Name,-15}|{guest.PassportNum,-10}|");
        i = i + 1;
    }
}
void displayAllRoom() //display all available room
{

    Console.WriteLine("------------------------------------------------------------------------------------------");
    Console.WriteLine("All available rooms: ");
    Console.WriteLine("Standard Rooms: ");
    Console.WriteLine($"|{"Room Number",-15}|{"Bed Configuration",-20}|{"Daily Rate",-10}|");
    foreach (Room room in roomList) 
    {
        if (room.IsAvail == true)
        {
            if (room.GetType() == typeof(StandardRoom)) //check room type (standard)
            {
                Console.WriteLine($"|{room.RoomNumber,-15}|{room.BedConfiguration,-20}|{room.DailyRate,-10}|");
            }
        }
    }
    Console.WriteLine("----------------------------------------------------");
    Console.WriteLine("Deluxe Rooms: ");
    Console.WriteLine($"|{"Room Number",-15}|{"Bed Configuration",-20}|{"Daily Rate",-10}|");
    foreach (Room room in roomList)
    {
        if (room.IsAvail == true)
        {
            if (room.GetType() == typeof(DeluxeRoom)) //check room type (deluxe)
            {
                Console.WriteLine($"|{room.RoomNumber,-15}|{room.BedConfiguration,-20}|{room.DailyRate,-10}|");
            }

        }
    }
    Console.WriteLine("----------------------------------------------------");
}

void mainmenu()
{
    {
        Console.WriteLine("Select one of the options below");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("1. Display all guests");
        Console.WriteLine("2. Display all available rooms");
        Console.WriteLine("3. Register Guest");
        Console.WriteLine("4. Check-in Guest");
        Console.WriteLine("5. Display stay of guest");
        Console.WriteLine("6. Extends the stay by numbers of day");
        Console.WriteLine("7. Display monthly charged amounts breakdown & total charged amounts for the year");
        Console.WriteLine("8. Check-out guests");
        Console.WriteLine("9. Show all guests and their total cost of stay");
        Console.WriteLine("10. Make room unavailable(For maintenance)");
        Console.WriteLine("11. Make room available");
        Console.WriteLine("12. Exit");
        Console.WriteLine("-------------------------------------");
    }

}


void registerGuest()
{
    Console.WriteLine("Enter the name of the guest: ");
    string? name = Convert.ToString(Console.ReadLine());
    Console.WriteLine("Enter the passport number of the guest: ");
    string? passportNo = Convert.ToString(Console.ReadLine());
    Membership status = new Membership("ordinary", 0);
    Guest guest = new Guest(name, passportNo, null, status );
    guest.IsCheckedin = false;
    guestList.Add(guest);
    string guestdata = $"{name},{passportNo},{status.Status},{status.Points}"; //combine data
    string filename = "Guests.csv";
    string projectRoot = System.AppDomain.CurrentDomain.BaseDirectory;
    string filePath = Path.Combine(projectRoot, filename);

    /* using (StreamWriter sw = new StreamWriter("Guests.csv", true))//write to file
    {
        sw.WriteLine(guestdata);
    }    */
    File.AppendAllText(filePath, guestdata);

    Console.WriteLine("Guest successfully registered!");


}
Guest validateselectedguest()
{
    int guestnum;
    Guest selectedGuest;
    while (true)
    {
        try
        {
            Console.WriteLine("Which guest would you like to check-in? (enter the S/N of the guest)");
            guestnum = Convert.ToInt32(Console.ReadLine());
            selectedGuest = guestList[guestnum - 1];
            break;
        }

        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please enter a number within the range");
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input, please enter a number");
        }


    }
    return selectedGuest;
}
Guest validateselectedguestforcheckout()
{
    int guestnum;
    Guest selectedGuest;
    while (true)
    {
        try
        {
            Console.WriteLine("Which guest would you like to check-out? (enter the S/N of the guest)");
            guestnum = Convert.ToInt32(Console.ReadLine());
            selectedGuest = guestList[guestnum - 1];
            break;
        }

        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please enter a number within the range");
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input, please enter a number");
        }


    }
    return selectedGuest;
}
Room selectRoom()
{
    Console.WriteLine("Which room would you like to check in? (enter the room number)");
    Console.Write("Your Option: ");
    int roomnum = 0;
    while (true)
    {
        try
        {
            roomnum = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input, please enter a room number");

        }
    }
    foreach (Room room in roomList)
    {
        if (room.RoomNumber == roomnum)
        {
            if (room.IsAvail == true)
            {
                if (room.GetType() == typeof(StandardRoom))
                {

                    while (true)
                    {
                        Console.WriteLine("Require Wifi? (y/n)");
                        Console.Write("Your option: ");
                        string? option = Convert.ToString(Console.ReadLine());
                        option = option.ToLower();
                        if (option != "y" && option != "n")
                        {
                            Console.WriteLine("Invalid input, please enter \"y\" or \"n\"");
                            continue;
                        }
                        else if (option == "y")
                        {
                            ((StandardRoom)room).RequireWifi = true;
                            break;
                        }
                        else if (option == "n")
                        {
                            ((StandardRoom)room).RequireWifi = false;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Require breakfast? (y/n)");
                        Console.Write("Your option: ");
                        string? option = Convert.ToString(Console.ReadLine());
                        option = option.ToLower();
                        if (option != "y" && option != "n")
                        {
                            Console.WriteLine("Invalid input, please enter \"y\" or \"n\"");
                            continue;
                        }
                        else if (option == "y")
                        {
                            ((StandardRoom)room).RequireBreakfast = true;

                            break;
                        }
                        else if (option == "n")
                        {
                            ((StandardRoom)room).RequireBreakfast = false;
                            break;
                        }
                    }
                    return room;
                }
                else if (room.GetType() == typeof(DeluxeRoom))
                {
                    while (true)
                    {
                        Console.WriteLine("Require additional bed? (y/n)");
                        Console.Write("Your option: ");
                        string? option = Convert.ToString(Console.ReadLine());
                        option = option.ToLower();
                        if (option != "y" && option != "n")
                        {
                            Console.WriteLine("Invalid input, please enter \"y\" or \"n\"");
                            continue;
                        }
                        else if (option == "y")
                        {
                            ((DeluxeRoom)room).AdditionalBed = true;
                            break;
                        }
                        else if (option == "n")
                        {
                            ((DeluxeRoom)room).AdditionalBed = false;
                            break;
                        }
                        
                    }
                    return room;
                }
            
            }
        }
    }
    return null;
}
void checkinGuest()
{
    displayAllGuestLessInfo();

    Guest selectedGuest = validateselectedguest();
    while (true)
    {
        if (selectedGuest.IsCheckedin == true)
        {


            Console.WriteLine("Selected guest has already checked in. Select this guest?(y/n)");
            string? option = Convert.ToString(Console.ReadLine());
            option = option.ToLower();
            Console.WriteLine(option);
            if (option != "y" && option != "n")
            {
                Console.WriteLine("write \"y\" or \"n\" only ");
                continue;
            }
            else if (option == "n")
            {
                selectedGuest = validateselectedguest();
                continue;

            }
            else if (option == "y")
            {
                break;
            }

        }
        else if (selectedGuest.IsCheckedin == false)
        {
            Console.WriteLine($"Guest selected: {selectedGuest.Name}");
            break;
        }
    }
    DateTime checkin;
    DateTime checkout;
    Stay checkinstay;
    while (true)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the check-in date (dd/mm/yyyy): ");
                checkin = Convert.ToDateTime(Console.ReadLine());
                break;

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input, please enter a valid date in (dd/mm/yyyy) format");

            }
        }
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the check-out date (dd/mm/yyyy): ");
                checkout = Convert.ToDateTime(Console.ReadLine());
                break;
            }
            catch (Exception)
            {

                Console.WriteLine("Invalid input, please enter a valid date in (dd/mm/yyyy) format");

            }
        }
        if (checkout.Subtract(checkin).Days < 1)

        {
            Console.WriteLine("Check-out date must be after check-in date or at least 1 day after check-in date");
            continue;
        }
        else
        {
            checkinstay = new Stay(checkin, checkout);
            
        }
        displayAllRoom();
        while (true)
        {
            Room room = selectRoom();
            if (room == null)
            {
                Console.WriteLine("room selected does not exist, please try again!");
                continue;
            }
            room.IsAvail = false;
            selectedGuest.IsCheckedin = true;
            selectedGuest.HotelStay = checkinstay;
            checkinstay.AddRoom(room);
            break;
        }
        


        Room room2;
        while (true)
        {
            Console.WriteLine("Another room? (y/n)");
            Console.Write("Your option: ");
            string? option = Convert.ToString(Console.ReadLine());
            option = option.ToLower();
            if (option != "y" && option != "n")
            {
                Console.WriteLine("Invalid input, please enter \"y\" or \"n\"");
                continue;
            }
            else if (option == "y")
            {
                displayAllRoom();
                while (true)
                {
                    room2 = selectRoom();
                    if (room2 == null)
                    {
                        Console.WriteLine("room selected does not exist, please try again!");
                        continue;
                    }
                    room2.IsAvail = false;
                    selectedGuest.HotelStay = checkinstay;
                    checkinstay.AddRoom(room2);
                    break;
                }
                break;
                

            }
            else if (option == "n")
            {
                break;
            }
        }

                    
        Console.WriteLine("Guest successfully checked in!");
        break;
             
    }
}
void displayStayDetails()
{
    displayAllGuestLessInfo();
    Console.WriteLine("Change the stay details of which Guest? (Enter S/N number of the guest)");
    int guestNum = 0;
    Guest selectedGuest;
    while (true)
    {
        try
        {
            guestNum = Convert.ToInt32(Console.ReadLine());
            selectedGuest = guestList[guestNum - 1];
            break;
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter a valid number or a number that is in the list");
        }
    }
    
    if (selectedGuest.IsCheckedin != false || selectedGuest.HotelStay != null)
    {
        Console.WriteLine($"Selected Guest: {selectedGuest.Name}");
        Console.WriteLine($"check-in: {selectedGuest.HotelStay.CheckinDate.ToShortDateString()}\ncheckout: {selectedGuest.HotelStay.CheckoutDate.ToShortDateString()}");
        string roomtype = "";

        Console.WriteLine("Room(s): ");
        foreach (Room r in selectedGuest.HotelStay.RoomList)
        {
            if (r.GetType() == typeof(StandardRoom))
            {
                roomtype = "Standard";
            }
            else if (r.GetType() == typeof(DeluxeRoom))
            {
                roomtype = "Deluxe";
            }

            Console.WriteLine($"Room type: {roomtype}, Room number: {r.RoomNumber}, Bed Configuration: {r.BedConfiguration}, Daily Rate: {r.DailyRate}");
        } 
        
    }
    else
    {
        Console.WriteLine("Guest has not checked in, please update the stay details of the guest!");
    }


}
void displaystaydetails()
{
    Console.WriteLine($"\n|{"S/N",-3}|{"Guests",-15}|{"check-in Date",-15}|{"check-out Date",-15}|");
    Console.WriteLine("---------------------------------------------------------");
    int i = 1;
    foreach (Guest guest in guestList)
    {

        Console.WriteLine($"|{i,-3}|{guest.Name,-15}|{guest.HotelStay.CheckinDate.ToShortDateString(),-15}|{guest.HotelStay.CheckoutDate.ToShortDateString(),-15}|");
        i = i + 1;
    }
}
void extenddays()
{
    displaystaydetails();
    Console.WriteLine("Extend the stay of which Guest? (Enter S/N number of the guest)");
    int guestNum = 0;
    Guest selectedGuest;
    while (true)
    {
        try
        {
            guestNum = Convert.ToInt32(Console.ReadLine());
            selectedGuest = guestList[guestNum - 1];
            break;
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter a valid number or a number that is in the list");
        }
    }
    Console.WriteLine($"Selected Guest: {selectedGuest.Name}");
    Console.WriteLine($"Current:\ncheck-in: {selectedGuest.HotelStay.CheckinDate.ToShortDateString()}\ncheckout: {selectedGuest.HotelStay.CheckoutDate.ToShortDateString()}\nDays: {selectedGuest.HotelStay.CheckoutDate.Subtract(selectedGuest.HotelStay.CheckinDate).Days}");
    Console.WriteLine("Enter the number of days to extend the stay by: ");
    int AddDays;
    while (true)
    {
        try
        {
            AddDays = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch (Exception)
        {
            Console.WriteLine("Please type a number");
        }
    }
    DateTime newcheckoutdate = selectedGuest.HotelStay.CheckoutDate.AddDays(AddDays);
    selectedGuest.HotelStay.CheckoutDate = newcheckoutdate;
    Console.WriteLine($"New check-out date: {selectedGuest.HotelStay.CheckoutDate.ToShortDateString()}\nDays extended by:{AddDays}\nDays now:{selectedGuest.HotelStay.CheckoutDate.Subtract(selectedGuest.HotelStay.CheckinDate).Days}");

}
void ChargeAmountsMonthly()
{
    Console.Write("Enter the year:");
    int yearSelected;
    while (true)
    {
        try
        {
            yearSelected = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter a valid year");
        }
    }
    IDictionary<int, double> yearcharge = new Dictionary<int, double>();

    foreach (Guest g in guestList)
    {

        if (g.HotelStay != null)
        {
            if (g.HotelStay.CheckoutDate.Year == yearSelected)
            {
                for (int i = 1; i <= 12; i++)
                {
                    double total = 0;
                    if (g.HotelStay.CheckoutDate.Month == i)
                    {
                        total = total + g.HotelStay.CalculateTotal();

                        if (!yearcharge.ContainsKey(i))
                        {
                            yearcharge.Add(i, total);
                        }
                        else if (yearcharge.ContainsKey(i))
                        {
                            yearcharge[i] = yearcharge[i] + total;
                        }

                    }
                    if (!yearcharge.ContainsKey(i))
                    {
                        yearcharge.Add(i, total);
                    }
                }
            } 
        }
    }
    foreach (KeyValuePair<int, double> kvp in yearcharge)
    {
        if (kvp.Key != 13)
        {
            Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key)[..3]} {yearSelected}, Total: {kvp.Value}");

        }

    }
}
void CheckoutGuest()
{
    displayAllGuestLessInfo();
    Guest selectedGuest = validateselectedguestforcheckout();
    DateTime newcheckoutdate;

    while (true)
    {
        if (selectedGuest.IsCheckedin == false)
        {


            Console.WriteLine("Selected guest does not have a check-in info. Select another guest?(y/n)");
            string? option = Convert.ToString(Console.ReadLine());
            option = option.ToLower();
            Console.WriteLine(option);
            if (option != "y" && option != "n")
            {
                Console.WriteLine("write \"y\" or \"n\" only ");
                continue;
            }
            else if (option == "y")
            {
                selectedGuest = validateselectedguestforcheckout();
                continue;

            }
            else if (option == "n")
            {
                break;
            }

        }
        else if (selectedGuest.HotelStay == null)
        {
            Console.WriteLine("Selected guest has not checked in and have not selected a stay. Please select another guest.");
            continue;
        }

        else if (selectedGuest.IsCheckedin == true)
        {
            Console.WriteLine($"Guest selected: {selectedGuest.Name}");

        }
        Console.WriteLine($"Total cost of stay: {selectedGuest.HotelStay.CalculateTotal()}");
        Console.WriteLine($"Membership status: {selectedGuest.Member.Status}\t Current amount of points:{selectedGuest.Member.Points}");
        if (selectedGuest.Member.Status == "ordinary")
        {
            Console.WriteLine("Currently not eligible to redeem points for discount");
            Console.WriteLine($"Press anything to make payment");
            Console.ReadKey();
            Console.WriteLine();
            string previousmember = selectedGuest.Member.Status;
            selectedGuest.Member.EarnPoints(selectedGuest.HotelStay.CalculateTotal());
            selectedGuest.IsCheckedin = false;
            foreach (Room stayroom in selectedGuest.HotelStay.RoomList)
            {
                foreach (Room clearroom in roomList)
                {
                    if (stayroom.RoomNumber == clearroom.RoomNumber)
                    {
                        clearroom.IsAvail = true;
                    }
                }
            }
            selectedGuest.HotelStay.RoomList.Clear();
            Console.WriteLine($"New points: {selectedGuest.Member.Points}");

            Console.WriteLine("Payment successful");

        }
        else
        {
            Console.WriteLine("Use points for discount?(Type points, otherwise type 0)");
            int option;
            double newtotal = 0;
            while (true)
            {
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                    if (option <= selectedGuest.Member.Points)
                    {
                        newtotal = selectedGuest.HotelStay.CalculateTotal() - option;
                        Console.WriteLine($"New total: {newtotal}");
                        Console.WriteLine($"Press anything to make payment");
                        Console.ReadKey();
                        Console.WriteLine();
                        selectedGuest.IsCheckedin = false;

                        selectedGuest.Member.EarnPoints(selectedGuest.HotelStay.CalculateTotal());
                        foreach (Room stayroom in selectedGuest.HotelStay.RoomList)
                        {
                            foreach (Room clearroom in roomList)
                            {
                                if (stayroom.RoomNumber == clearroom.RoomNumber)
                                {
                                    clearroom.IsAvail = true;
                                }
                            }
                        }
                        selectedGuest.HotelStay.RoomList.Clear();
                        string previousmember = selectedGuest.Member.Status;
                        selectedGuest.Member.RedeemPoints(option);
                        if (previousmember != selectedGuest.Member.Status)
                        {
                            Console.WriteLine($"Membership upgraded to: {selectedGuest.Member.Status}");
                        }

                        Console.WriteLine($"New points: {selectedGuest.Member.Points}");
                        Console.WriteLine("Payment successful");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not enough");
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }

        }break;
    }
}
void makeroomunavailable()
{

    Console.WriteLine("------------------------------------------------------------------------------------------");
    Console.WriteLine("All available rooms: ");
    Console.WriteLine("Standard Rooms: ");
    Console.WriteLine($"|{"Room Number",-15}|{"Bed Configuration",-20}|{"Daily Rate",-10}|");
    foreach (Room room in roomList)
    {

        if (room.IsAvail == true)
        {
            if (room.GetType() == typeof(StandardRoom)) //check room type (standard)
            {
                Console.WriteLine($"|{room.RoomNumber,-15}|{room.BedConfiguration,-20}|{room.DailyRate,-10}|");
            }
        }
    }
    Console.WriteLine("----------------------------------------------------");
    Console.WriteLine("Deluxe Rooms: ");
    Console.WriteLine($"|{"Room Number",-15}|{"Bed Configuration",-20}|{"Daily Rate",-10}|");
    foreach (Room room in roomList)
    {

        if (room.IsAvail == true)
        {
            if (room.GetType() == typeof(DeluxeRoom)) //check room type (deluxe)
            {
                Console.WriteLine($"|{room.RoomNumber,-15}|{room.BedConfiguration,-20}|{room.DailyRate,-10}|");
            }

        }
    }
    Console.WriteLine("----------------------------------------------------");

    Console.WriteLine("Select a room to make unavailable");
    int roomSN;
    Room selectedroom;
    while (true)
    {
        try
        {
            roomSN = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Enter something in range");
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter a valid number");
        }
    }
    foreach (Room r in roomList)
    {
        if (r.RoomNumber == roomSN)
        {
            while (true)
            {
                Console.WriteLine("Confirm? (y/n)");
                string confirmation = Convert.ToString(Console.ReadLine()).ToLower();
                if (confirmation == "y")
                {
                    r.IsAvail = false;
                    Console.WriteLine($"Room {r.RoomNumber} is now unavailable");

                    break;
                }
                else if (confirmation == "n")
                {
                    Console.WriteLine("Process Terminated");
                    return;
                }
                else
                {
                    Console.WriteLine("Please enter (y/n)");
                    continue;
                }
            }
        }
    }

}
void makeroomavailable()
{

    Console.WriteLine("------------------------------------------------------------------------------------------");
    Console.WriteLine("All unavailable rooms: ");
    Console.WriteLine("Standard Rooms: ");
    Console.WriteLine($"|{"Room Number",-15}|{"Bed Configuration",-20}|{"Daily Rate",-10}|");
    foreach (Room room in roomList)
    {

        if (room.IsAvail == false)
        {
            if (room.GetType() == typeof(StandardRoom)) //check room type (standard)
            {
                Console.WriteLine($"|{room.RoomNumber,-15}|{room.BedConfiguration,-20}|{room.DailyRate,-10}|");
            }
        }
    }
    Console.WriteLine("----------------------------------------------------");
    Console.WriteLine("Deluxe Rooms: ");
    Console.WriteLine($"|{"Room Number",-15}|{"Bed Configuration",-20}|{"Daily Rate",-10}|");
    foreach (Room room in roomList)
    {

        if (room.IsAvail == false)
        {
            if (room.GetType() == typeof(DeluxeRoom)) //check room type (deluxe)
            {
                Console.WriteLine($"|{room.RoomNumber,-15}|{room.BedConfiguration,-20}|{room.DailyRate,-10}|");
            }

        }
    }
    Console.WriteLine("----------------------------------------------------");

    Console.WriteLine("Select a room to make available(enter room number)");
    int roomSN;
    Room selectedroom;
    while (true)
    {
        try
        {
            roomSN = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Enter something in range");
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter a valid number");
        }
    }
    foreach (Room r in roomList)
    {
        if (r.RoomNumber == roomSN)
        {
            while (true)
            {
                Console.WriteLine("Confirm? (y/n)");
                string confirmation = Convert.ToString(Console.ReadLine()).ToLower();
                if (confirmation == "y")
                {
                    r.IsAvail = false;
                    Console.WriteLine($"Room {r.RoomNumber} is now available");

                    break;
                }
                else if (confirmation == "n")
                {
                    Console.WriteLine("Process Terminated");
                    return;
                }
                else
                {
                    Console.WriteLine("Please enter (y/n)");
                    continue;
                }
            }
        }
        else
        {
            makeroomavailable();
        }
    }

    
    
}


//main program
while (true)
{
    mainmenu();
    int option;
    while (true)
    {
        try
        {
            Console.Write("Your option:");
            option = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input, please enter a number");
        }
    }
    if (option == 1)
    {

        while (true)
        {
            displayAllGuest();
            Console.WriteLine("\n Press anything to go back to the main menu");
            Console.ReadKey();
            Console.WriteLine(" "); //input key on next line
            break;
        }
    }
    else if (option == 2)
    {
        while (true)
        {
            displayAllRoom();
            Console.WriteLine("\n Press anything to go back to the main menu");
            Console.ReadKey();
            Console.WriteLine(" "); //input key on next line

            break;
        }
    }
    else if (option == 3)
    {
        while (true)
        {
            registerGuest();
            break;
        }
    }
    else if (option == 4)

    {
        while (true)
        {
            checkinGuest();
            Console.WriteLine("\n Press anything to go back to the main menu");
            Console.ReadKey();
            Console.WriteLine(" "); //input key on next line
            break;
        }
    }
    else if (option == 5)
    {
        while (true)
        {
            displayStayDetails();
            Console.WriteLine("\n Press anything to go back to the main menu");
            Console.ReadKey();
            Console.WriteLine(" "); //input key on next line
            break;
        }
    }
    else if (option == 6)
    {
        while (true)
        {
            extenddays();
            Console.WriteLine("\n Press anything to go back to the main menu");
            Console.ReadKey();
            Console.WriteLine(" "); //input key on next line
            break;
        }

    }
    else if (option == 7)
    {
        while (true)
        {
            if (guestList[1].HotelStay.CheckinDate.Year == 2022)
            {
                Console.WriteLine("Foobar");
            }
            ChargeAmountsMonthly();
            Console.WriteLine("\n Press anything to go back to the main menu");
            Console.ReadKey();
            Console.WriteLine(" "); //input key on next line
            break;
        }
    }
    else if (option == 8)
    {
        CheckoutGuest();
        Console.WriteLine("\n Press anything to go back to the main menu");
        Console.ReadKey();
        Console.WriteLine(" "); //input key on next line
    }
    else if (option == 9)
    {
        Console.WriteLine($"{"Guest's name",-15}|{"Total Cost of stay",-20}");
        Console.WriteLine("-----------------------------------------");
        foreach (Guest g in guestList)
        {
            if (g.HotelStay != null)
            {
                Console.WriteLine($"{g.Name,-15}|${g.HotelStay.CalculateTotal()}");
                Console.WriteLine("-----------------------------------------");
            }
        }
        Console.WriteLine("\n Press anything to go back to the main menu");
        Console.ReadKey();
        Console.WriteLine(" "); //input key on next line
    }
    else if (option == 10)
    {
        makeroomunavailable();
        Console.WriteLine("\n Press anything to go back to the main menu");
        Console.ReadKey();
        Console.WriteLine(" "); //input key on next line
    }
    else if (option == 11)
    {
        makeroomavailable();
        Console.WriteLine("\n Press anything to go back to the main menu");
        Console.ReadKey();
        Console.WriteLine(" "); //input key on next line
    }
    else if (option == 12) 
    {
        break;
    }


    else
    {
        Console.WriteLine("Please choose one of the options in the list!!");
        continue;
    }
    
}








    


