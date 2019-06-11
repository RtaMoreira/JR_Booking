using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB_EF
{
    public class RoomDB
    {
        static VB_ModelContainer context = new VB_ModelContainer();

        public static Room GetRoom(int id)
        {
            var q = from room in context.Rooms.Include("Pictures")
                    where room.IdRoom == id
                    select room;

            Room r = q.FirstOrDefault();

            return r;
        }

        public static List<Room> SearchAvailableRooms(DateTime checkin, DateTime checkout, String location)
        {
            List<Room> result = new List<Room>();
            List<int> bookedRooms = new List<int>();

            var q = from res in context.Reservations.Include("Rooms")
                    where (((res.CheckIn >= checkin && res.CheckIn <= checkout) || (res.CheckOut >= checkin && res.CheckOut <= checkout))
                    && (res.CheckOut != checkin && res.CheckIn != checkout))
                    || (res.CheckIn < checkin && res.CheckOut > checkout)
                    select res;


            foreach (Reservation rsv in q)
            {
                foreach (Room room in rsv.Rooms)
                {
                    bookedRooms.Add(room.IdRoom);
                }
            }

            var qr = from room in context.Rooms.Include("Hotels").Include("Pictures")
                     where (room.Hotels.Location.Equals(location)) && (!bookedRooms.Contains(room.IdRoom))
                     select room;

            foreach (Room room in qr)
            {
                room.Hotels = null;
                result.Add(room);
            }

            if (result != null)
            {
                foreach (Room r in result)
                {
                    r.Price = CalculatePriceStayRoom(r.IdRoom, checkin, checkout);
                }
            }

            return result;
        }

        public static decimal CalculatePriceStayRoom(int idRoom, DateTime checkIn, DateTime checkOut)
        {
            decimal totalPrice = 0;

            var room = from r in context.Rooms
                       where r.IdRoom == idRoom
                       select r;

            decimal normalPrice = room.FirstOrDefault().Price;

            for (DateTime i = checkIn; i < checkOut; i = i.AddDays(1.0))
            {
                if (HotelDB.HasReached70(room.FirstOrDefault().Hotel_IdHotel, i))
                {
                    totalPrice += Decimal.Multiply(normalPrice, (decimal)1.2);
                }
                else
                {
                    totalPrice += normalPrice;
                }
            }

            return totalPrice;
        }

        public static List<Room> GetRoomsAvailableDetails(DateTime checkIn, DateTime checkOut, string location, bool HasTV, bool HasHairDryer,
            int minCategory, int type, bool HasWifi, bool HasParking, decimal minPrice, decimal maxPrice)
        {
            List<Room> result = new List<Room>();
            List<int> bookedRooms = new List<int>();

            //recherche de chambres occupées
            var q = from res in context.Reservations.Include("Rooms")
                    where (((res.CheckIn >= checkIn && res.CheckIn <= checkOut) || (res.CheckOut >= checkIn && res.CheckOut <= checkOut))
                    && (res.CheckOut != checkIn && res.CheckIn != checkOut))
                    || (res.CheckIn < checkIn && res.CheckOut > checkOut)
                    select res;
            foreach (Reservation rsv in q)
            {
                string test = rsv.Firstname;
                string test2 = rsv.Lastname;
                foreach (Room room in rsv.Rooms)
                {
                    bookedRooms.Add(room.IdRoom);
                }
            }

            var qr = from room in context.Rooms.Include("Hotels").Include("Pictures")
                     where (room.Hotels.Location.Equals(location))
                     && (!bookedRooms.Contains(room.IdRoom))
                     && (room.HasTV == true || room.HasTV == HasTV)
                     && (room.HasHairDryer == true || room.HasHairDryer == HasHairDryer)
                     && (room.Hotels.HasWifi == true || room.Hotels.HasWifi == HasWifi)
                     && (room.Hotels.HasParking == true || room.Hotels.HasParking == HasParking)
                     && (room.Price >= minPrice)
                     && (room.Price <= maxPrice || maxPrice == 0)
                     && (room.Hotels.Category >= minCategory)
                     && (room.Type == type || type == 0)
                     select room;
            foreach (Room room in qr)
            {
                room.Hotels = null;
                result.Add(room);
            }

            if (result != null)
            {
                foreach (Room r in result)
                {
                    r.Price = CalculatePriceStayRoom(r.IdRoom, checkIn, checkOut);
                }
            }

            return result;
        }

        public static List<Room> SearchAvailRoomsForHotel(DateTime checkin, DateTime checkout, int idHotel)
        {
            List<Room> result = new List<Room>();
            List<int> bookedRooms = new List<int>();

            var q = from res in context.Reservations.Include("Rooms")
                    where (((res.CheckIn >= checkin && res.CheckIn <= checkout) || (res.CheckOut >= checkin && res.CheckOut <= checkout))
                    && (res.CheckOut != checkin && res.CheckIn != checkout))
                    || (res.CheckIn < checkin && res.CheckOut > checkout)
                    select res;


            foreach (Reservation rsv in q)
            {
                foreach (Room room in rsv.Rooms)
                {
                    bookedRooms.Add(room.IdRoom);
                }
            }

            var qr = from room in context.Rooms
                     where (room.Hotel_IdHotel == idHotel) && (!bookedRooms.Contains(room.IdRoom))
                     select room;

            return qr.ToList();
        }

        public static bool verifyRoomStillAvailable(int idRoom, DateTime checkin, DateTime checkout)
        {

            //get booked rooms for this date range
            var query = from res in context.Reservations.Include("Rooms")
                        where (((res.CheckIn >= checkin && res.CheckIn <= checkout) || (res.CheckOut >= checkin && res.CheckOut <= checkout))
                        && (res.CheckOut != checkin && res.CheckIn != checkout))
                        || (res.CheckIn < checkin && res.CheckOut > checkout)
                        select res;

            //test if the room still included
            foreach (Reservation r in query)
            {
                foreach (Room room in r.Rooms)
                {
                    if (room.IdRoom == idRoom)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static List<List<Room>> FindVariousRoomsForGroup(int nbGuests, DateTime checkIn, DateTime checkOut)
        {
            //we will return a "package" of rooms per hotel for the number of Guests
            List<List<Room>> allPackages = new List<List<Room>>();

            //for each hotel, we create a package for the nb of guests
            List<int> hotels = HotelDB.GetAllHotels();

            foreach (int h in hotels)
            {
                List<Room> roomsInHotel = RoomDB.SearchAvailRoomsForHotel(checkIn, checkOut, h);

                //creation of the package per hotel
                List<Room> roomsFor1Package = new List<Room>();

                if (roomsInHotel != null)   //if there is available rooms, we try to do a package
                {
                    int nbGuestsLeft = nbGuests;


                    for (int i = 0; i < roomsInHotel.Count(); i++)
                    {
                        //calculate price of room 
                        roomsInHotel[i].Price = RoomDB.CalculatePriceStayRoom(roomsInHotel[i].IdRoom, checkIn, checkOut);

                        if (roomsInHotel[i].Type <= (nbGuestsLeft + 1)) //accept the case where 1 person left and Double room
                        {
                            roomsFor1Package.Add(roomsInHotel[i]);
                            nbGuestsLeft -= roomsInHotel[i].Type;
                        }

                        if (nbGuestsLeft <= 0)   //quand assez de chambres pour guest, plus besoin de continuer
                        {
                            break;
                        }
                    }

                    //test que tous les guest aient un lit pour proposer ce package
                    if (nbGuestsLeft <= 0)
                    {
                        //ajout de ce package à RoomsInPackage
                        allPackages.Add(roomsFor1Package);
                    }

                }

            }
            return allPackages;
        }
    }
}
