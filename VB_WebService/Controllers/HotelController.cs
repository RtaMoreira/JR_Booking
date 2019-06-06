using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VB_EF;

namespace VB_WebService.Controllers
{
    [RoutePrefix("hotels")]
    public class HotelController : ApiController
    {
        static VB_ModelContainer context = new VB_ModelContainer();

        [Route("{id:int}")]
        public IHttpActionResult GetHotel(int id)
        {
            var q = from hotel in context.Hotels
                    where hotel.IdHotel == id
                    select hotel;

            return Ok(q);
        }

        [Route("locations")]
        public IHttpActionResult GetAllLocations()
        {
            var q = context.Hotels.Select(h => h.Location).Distinct();

            return Ok(q);
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

            double nbRoomsOfHotel =  Convert.ToDouble(q);


            //number of booked rooms


            //get booked room for this day
             var q2 = from r in context.Reservations.Include("Rooms")
                      where day >= r.CheckIn && day <= r.CheckOut
                      select r;

            //count number of booked rooms for the hotel
            double nbUnavailableRooms = 0;
            foreach (Reservation re in q2.ToList())
            {
                foreach(Room r in re.Rooms)
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
