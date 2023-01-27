




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
        // acheck the availability of the room
        
        for (int j = 1; j < stayData.Length; j++)
        {

            string[] stayinfo = stayData[j].Split(','); //measures: if both column happens to have null or empty string.
            //default case
            int notnullinfo1 = 0; // set placeholder value for the room cells
            int notnullinfo2 = 0;
            if (int.TryParse(stayinfo[9], out notnullinfo1) && int.TryParse(stayinfo[5], out notnullinfo2)) //check if the cell is empty, otherwise convert the cell the int * this is assuming only 2 rooms can be booked per person
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

                
            if (roomnumber == notnullinfo1 || roomnumber == notnullinfo2) // checks if the room number is in a column in the stay data file.
                {
                    //check the check-in column to see if its available
                    if (stayinfo[2].ToLower() == "true") //to lower so that in the case where the text gets typed with the wrong capitalisation of letters, the code doesnt skip this step by default.
                        {
                            // if it is present in the stay data list and the check-in column shows true, then the room is not available.
                            availability = false;

                            break;
                        }
                    else if (stayinfo[2].ToLower() == "false")
                    {
                        // if it is present in the stay data list and the check-in column shows false, then the room is available.
                        availability = true;
                        break;

                    }

                }


        }
        //now create the room object
        if (roomtype == "standard")
        {
            StandardRoom room = new StandardRoom(roomnumber, bedconfig, dailyrate, availability);
            roomList.Add(room);

        }
        else if (roomtype == "deluxe")
        {
            DeluxeRoom room = new DeluxeRoom(roomnumber, bedconfig, dailyrate, availability);
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
            Console.WriteLine("Please enter a number within te range");
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input, please enter a number");
        }


    }
    return selectedGuest;
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
            string? option = Convert.ToString(Console.ReadLine()).ToLower();
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
        Console.WriteLine("Which room would you like to check in? (enter the room number)");
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
                    room.IsAvail = false;
                    selectedGuest.IsCheckedin = true;
                    selectedGuest.HotelStay = checkinstay;
                    
                    Console.WriteLine("Guest successfully checked in!");
                    Console.WriteLine("press anything to continue");
                    Console.ReadKey();
                    Console.WriteLine("");
                    break;
                }
                else
                {
                    Console.WriteLine("Room is not available, please select another room");
                    break;
                }
            }
        }
            break;
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
        checkinGuest();
    }
    else if (option == 5)
    {
        break;
    }
    else
    {
        Console.WriteLine("Please choose one of the options in the list!!");
        continue;
    }
    

}
    


