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

        [Route("rooms")]
        [HttpGet]
    /*    public IHttpActionResult GetAllRooms()
        {
            return Ok(EF_RoomManager.GetAllRooms());
        }
*/
        [Route("room/{id:int}")]
        public IHttpActionResult GetRoom(int id)
        {
            var q = from room in context.Rooms.Include("Pictures")
                    where room.IdRoom == id
                    select room;
            
            Room r = q.FirstOrDefault((p) => p.IdRoom == id);
            return Ok(r);
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
                if (HotelController.HasReached70(room.FirstOrDefault().Hotel_IdHotel, i))
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

        public static bool verifyRoomStillAvailable(int idRoom, DateTime checkin, DateTime checkout)
        {
            bool result = false;

            var query = context.
        }

   
}
