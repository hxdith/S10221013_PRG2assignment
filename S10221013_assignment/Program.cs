




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


    //process of this method: read roomdata, store data, create obj -> read stay data, store data, create obj -> read guest data, store data, create obj using the relevant info from other data files


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
            string[] stayinfo = stayData[j].Split(',');
            //default case
            if (roomnumber == int.Parse(stayinfo[5])) // checks if the room number is in a column in the stay data file.
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

initStayData();
//do all the necessary things to load the data related to stays
void initStayData()
{
    //go through the stay data

}
Console.WriteLine("hello");



