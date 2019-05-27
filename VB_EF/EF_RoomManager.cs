using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB_EF
{
    public static class EF_RoomManager
    {
        static VB_ModelContainer context = new VB_ModelContainer();

        //Get all rooms
        public static String GetAllRooms()
        {

            List<Room> allRooms = new List<Room>();
            var q = from r in context.Rooms
                    select r;

            Room test = q.FirstOrDefault();

            allRooms.Add(test);


            return Convert.ToString(allRooms.Count());
        }
    }
}
