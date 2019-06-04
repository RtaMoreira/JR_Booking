using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VB_EF;
using System.Data.Entity;

namespace VB_WebService.Controllers
{
    [RoutePrefix("api")]
    public class RoomController : ApiController
    {
        static VB_ModelContainer context = new VB_ModelContainer();

        ///api/rooms
        [Route("rooms")]
        [HttpGet]
        public IHttpActionResult GetAllRooms()
        {
            return Ok(EF_RoomManager.GetAllRooms());
        }

        ///api/room/1
        [Route("room/{id:int}")]
        public IHttpActionResult GetRoom(int id)
        {
            var q = from room in context.Rooms.Include("Pictures")
                    where room.IdRoom == id
                    select room;
            
            Room r = q.FirstOrDefault();
            return Ok(r);
        }

        ///api/available/2020-01-01/2020-01-03/sion
        [Route("available/{checkin:DateTime}/{checkOut:DateTime}/{location}")]
        [HttpGet]
        public IHttpActionResult SearchAvailableRooms(DateTime checkin, DateTime checkout, String location)
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

            var qr = from room in context.Rooms.Include("Hotels")
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
                    //r.Price = CalculatePriceStayRoom(r, checkin, checkout);
                }
            }

            return Ok(result);
        }

        ///api/available/details/2020-01-01/2020-01-03/sion/true/false/2/2/true/false/120/130
        [Route("available/details/{checkin:DateTime}/{checkOut:DateTime}/{location}/{HasTV:bool}/{HasHairDryer:bool}/{minCategory:int}/{type:int}/{HasWifi:bool}/{HasParking:bool}/{minPrice:decimal}/{maxPrice:decimal}")]
        [HttpGet]
        public IHttpActionResult GetRoomsAvailableDetails(DateTime checkIn, DateTime checkOut, string location, bool HasTV, bool HasHairDryer,
            int minCategory, int type, bool HasWifi, bool HasParking, decimal minPrice, decimal maxPrice)
        {
            List<Room> result = new List<Room>();
            List<int> bookedRooms = new List<int>();

            //recherche de chmabres occupées marche pas bien (recherche de chambres avec exactement la mm date ne retourne rien
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

            var qr = from room in context.Rooms.Include("Hotels")
                     where (room.Hotels.Location.Equals(location))
                     && (!bookedRooms.Contains(room.IdRoom))
                     && (room.HasTV==true || room.HasTV==HasTV)
                     && (room.HasHairDryer==true || room.HasHairDryer==HasHairDryer)
                     && (room.Hotels.HasWifi==true || room.Hotels.HasWifi==HasWifi)
                     && (room.Hotels.HasParking==true || room.Hotels.HasParking==HasParking)
                     && (room.Price >= minPrice)
                     && (room.Price <= maxPrice || maxPrice == 0)
                     && (room.Hotels.Category>=minCategory)
                     && (room.Type==type || type==0)
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
                    //r.Price = CalculatePriceStayRoom(r, checkin, checkout);
                }
            }

            return Ok(result);
        }
    }

   
}
