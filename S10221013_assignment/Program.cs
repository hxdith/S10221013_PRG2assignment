




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

// reads the data from the files and store them in an array
string[] roomData = File.ReadAllLines("Rooms.csv");
string[] guestData = File.ReadAllLines("Guests.csv");
string[] stayData = File.ReadAllLines("Stays.csv");


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
        int roomnumber = int.Parse(roominfo[1]);
        
        string bedconfig = roominfo[2];
        double dailyrate = double.Parse(roominfo[3]);
        bool availability = true;
        bool requirewifi = false;
        bool requirebreakfast = false ;
        bool requireadditionalbed = false;

        // acheck the availability of the room

        for (int j = 1; j < stayData.Length; j++)
        {

            string[] stayinfo = stayData[j].Split(','); //measures: if both column happens to have null or empty string.
            //default case
            int stayRoominfo1 = 0; // set placeholder value for the room cells
            int stayRoominfo2 = 0;
            if (int.TryParse(stayinfo[5], out stayRoominfo1) && int.TryParse(stayinfo[9], out stayRoominfo2)) //check if the cell is empty, otherwise convert the cell the int * this is assuming only 2 rooms can be booked per person
            {
                 
            }

            /* if (stayinfo[9] == "" || stayinfo[5] == "") //less cleaner way to do the previous validation? just leaving this here
            {
                notnullinfo1 = 0; // assuming there's no room 0
                notnullinfo2 = 0;

            }
            else
            {
                notnullinfo1 = int.Parse(stayinfo[9]); // parse the string to int if not null
                notnullinfo2 = int.Parse(stayinfo[5]);
            } */


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
                if (stayinfo[6] != null && stayinfo[6] != "")
                {
                    if (stayinfo[6].ToLower() == "true") //check wifi requirement
                    {
                        requirewifi = true;
                    }
                    else
                    {
                        requirewifi = false;
                    }
                }
                if (stayinfo[7] != null && stayinfo[7] != "")
                {
                    if (stayinfo[7].ToLower() == "true") //check breakfast requirement
                    {
                        requirebreakfast = true;

                    }
                    else
                    {
                        requirebreakfast = false;
                    }
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
            else if (roomnumber == stayRoominfo2)
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
        int points = int.Parse(guestinfo[3]);
        for (int j = 1; j < stayData.Length; j++)
        {
            string[] gueststayinfo = stayData[j].Split(',');
            if (name == gueststayinfo[0] && passportNo == gueststayinfo[1])
            {
                DateTime checkin = DateTime.Parse(gueststayinfo[3]);
                DateTime checkout = DateTime.Parse(gueststayinfo[4]);
                Guest guest = new Guest(name, passportNo, new Stay(checkin, checkout), new Membership(membership, points));
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
    
    Console.WriteLine($"\n{"S/N",-3} {"Guests",-7} {"PassportNo",-10} {"Membership Status",-15}");
    Console.WriteLine("============================================");
    int i = 1;
    foreach (Guest guest in guestList)
    {

        Console.WriteLine($"{i,-3} {guest.Name,-7} {guest.PassportNum,-10} {guest.Member.Status,-15} ");
        i = i + 1;
    }
}
void displayAllGuestLessInfo()
{
    Console.WriteLine($"\n{"S/N",-3} {"Guests",-7} {"PassportNo",-10}");
    Console.WriteLine("============================================");
    int i = 1;
    foreach (Guest guest in guestList)
    {

        Console.WriteLine($"{i,-3} {guest.Name,-7} {guest.PassportNum,-10}");
        i = i + 1;
    }
}
void displayAllRoom()
{

    Console.WriteLine("==================================================================================================");
    Console.WriteLine("All available rooms: ");
    Console.WriteLine("Standard Rooms: ");
    Console.WriteLine($" {"Room Number",-15} {"Bed Configuration",-20} {"Daily Rate",-10}");
    foreach (Room room in roomList) 
    {
        if (room.IsAvail == true)
        {
            if (room.GetType() == typeof(StandardRoom)) //check room type (standard)
            {
                Console.WriteLine($" {room.RoomNumber,-15} {room.BedConfiguration,-20} {room.DailyRate,-10}");
            }
        }
    }
    Console.WriteLine("Deluxe Rooms: ");
    Console.WriteLine($" {"Room Number",-15} {"Bed Configuration",-20} {"Daily Rate",-10}");
    foreach (Room room in roomList)
    {
        if (room.IsAvail == true)
        {
            if (room.GetType() == typeof(DeluxeRoom)) //check room type (deluxe)
            {
                Console.WriteLine($" {room.RoomNumber,-15} {room.BedConfiguration,-20} {room.DailyRate,-10}");
            }

        }
    }

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
        Console.WriteLine("6. Extend the number of stay days");
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

    Console.WriteLine("Guest successfully registered!\npress anything to continue");
        Console.ReadKey();
        Console.WriteLine("");


}
Guest validateselectedguest()
{
    int guestnum;
    Guest selectedGuest;
    while (true)
    {
        try
        {
            Console.WriteLine("Which guest would you like to check in? (enter the S/N of the guest)");
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
                    return room;a
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

                    }
                    else
                    {
                        room2.IsAvail = false;
                        selectedGuest.HotelStay = checkinstay;
                        checkinstay.AddRoom(room2);
                        break;
                    }
                }

                
                room2.IsAvail = false;
                selectedGuest.HotelStay = checkinstay;
                checkinstay.AddRoom(room2);
                break;
            }
            else if (option == "n")
            {
                break;
            }
        }

                    
        Console.WriteLine("Guest successfully checked in!");
        Console.WriteLine("press anything to continue");
        Console.ReadKey();
        Console.WriteLine("");
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
    Console.WriteLine($"Selected Guest: {selectedGuest.Name}");
    Console.WriteLine($"Selected Guest's stay: {selectedGuest.HotelStay.CheckinDate.ToShortDateString()} to {selectedGuest.HotelStay.CheckoutDate.ToShortDateString()}");
    Console.WriteLine($"Room number: ");
    //when in the initialisation, guest list and stay list are set to be the same length with the same index, so that means the stay list can be accessed by the same index as the guest list.


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
        break;
    }
    else
    {
        Console.WriteLine("Please choose one of the options in the list!!");
        continue;
    }
}



    


