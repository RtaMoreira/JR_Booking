using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB_EF
{
    public class HotelDB
    {
        static VB_ModelContainer context = new VB_ModelContainer();

        public static Hotel GetHotel(int id)
        {
            var q = from hotel in context.Hotels
                    where hotel.IdHotel == id
                    select hotel;
            Hotel h = q.FirstOrDefault();
            //h.Capacity = 4;
            return h;
        }

        public static List<String> GetAllLocations()
        {
            var q = context.Hotels.Select(h => h.Location).Distinct();
            return q.ToList();
        }

        public static List<int> GetAllHotels()
        {
            var q = from h in context.Hotels
                    select h.IdHotel;

            return q.ToList();
        }

        public static bool HasReached70(int idHotel, DateTime day)
        {

            //number of rooms for this hotel
            var q = from hotel in context.Hotels
                    where hotel.IdHotel == idHotel
                    select hotel.Rooms.ToList().Count();

            double nbRoomsOfHotel = Convert.ToDouble(q.FirstOrDefault());


            //number of booked rooms


            //get booked room for this day
            var q2 = from r in context.Reservations.Include("Rooms")
                     where day >= r.CheckIn && day <= r.CheckOut
                     select r;

            //count number of booked rooms for the hotel
            double nbUnavailableRooms = 0;
            foreach (Reservation re in q2.ToList())
            {
                foreach (Room r in re.Rooms)
                    if (r.Hotel_IdHotel == idHotel)
                    {
                        nbUnavailableRooms += 1;
                    }
            }

            double capacity = (nbUnavailableRooms / nbRoomsOfHotel);

            if (capacity >= 0.7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
