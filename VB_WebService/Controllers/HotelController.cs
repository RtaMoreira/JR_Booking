using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VB_EF;

namespace VB_WebService.Controllers
{
    [RoutePrefix("api")]
    public class HotelController : ApiController
    {
        static VB_ModelContainer context = new VB_ModelContainer();

        [Route("hotel/{id:int}")]
        public IHttpActionResult GetHotel(int id)
        {
            var q = from hotel in context.Hotels
                    where hotel.IdHotel == id
                    select hotel;

           // Hotel h = q.FirstOrDefault((p) => p.IdHotel == id);

            return Ok(q);
        }

        [Route("hotel/locations")]
        public IHttpActionResult GetAllLocations()
        {
            var q = context.Hotels.Select(h => h.Location).Distinct();

            return Ok(q);
        }


        public static bool HasReached70(int idHotel, DateTime day)
        {
            //DateTime day = new DateTime(2019,05,02);

            //number of rooms for this hotel
            var q = from hotel in context.Hotels
                    where hotel.IdHotel == idHotel 
                    select hotel.Rooms.ToList();

            double nbRoomsOfHotel =  Convert.ToDouble(q.Count());




            //number of booked rooms
            double nbUnavailableRooms = 0;


            //get booked room for this day
             var q2 = from r in context.Reservations.Include("Rooms")
                      where day >= r.CheckIn && day <= r.CheckOut
                      select r;

            //keep only booked for this hotel
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
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }

    }
}
