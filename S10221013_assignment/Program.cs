




//==========================================================
// Student Number : S10221013
// Student Name : Hadith Mukrish
//==========================================================



using S10221013_PRG2Assignment;
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
        // check the availability of the room
        
        for (int j = 1; j < stayData.Length; j++)
        {

            string[] stayinfo = stayData[j].Split(','); //measures if both column happens to have null or empty string.
            //default case
            int notnullinfo1 = 0; // set placeholder value
            int notnullinfo2 = 0;
            if (stayinfo[9] == "" || stayinfo[5] == "")
            {
                notnullinfo1 = 0; // assuming there's no room 0
                notnullinfo2 = 0;

            }
            else
            {
                notnullinfo1 = int.Parse(stayinfo[9]); // parse the string to int if not null
                notnullinfo2 = int.Parse(stayinfo[5]);
            }

                
            if (roomnumber == notnullinfo1 || roomnumber == notnullinfo2) // checks if the room number is in a column in the stay data file.
                {
                    //check the checkin column to see if its available
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




    //display the content of the roomlist
    foreach (Room room in roomList)
    {
        if (room.IsAvail == false)
        {
            Console.WriteLine(room);
        }

    }
}

initGuestData();
//do all the necessary things to load the data related to stays
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

                foreach (Room room in roomList)
                {
                    if (room.
                }
)
                break;
                
            }
    
        
    }
    
}



