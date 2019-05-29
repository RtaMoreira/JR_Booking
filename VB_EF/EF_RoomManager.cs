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
        public static List<Room> GetAllRooms()
        {
            List<Room> allRooms = new List<Room>();
            var q = from room in context.Rooms
                    select room;


            foreach(Room r in q)
            {
                allRooms.Add(r);
            }
            
            return allRooms;
        }

        public static Room GetRoom(int id)
        {
            var q = from room in context.Rooms
                    where room.IdRoom == id
                    select room;
            var r = q.FirstOrDefault();

            return r;
        }
    }
}
