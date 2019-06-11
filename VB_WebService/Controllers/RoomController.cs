using System;
using System.Web.Http;
using VB_EF;

namespace VB_WebService.Controllers
{
    [RoutePrefix("rooms")]
    public class RoomController : ApiController
    {
        [Route("{id:int}")]
        public IHttpActionResult GetRoom(int id)
        {
            return Ok(RoomDB.GetRoom(id));
        }


        [Route("available/{checkin:DateTime}/{checkout:DateTime}/{location}")]
        [HttpGet]
        public IHttpActionResult SearchAvailableRooms(DateTime checkin, DateTime checkout, String location)
        {
            return Ok(RoomDB.SearchAvailableRooms(checkin,checkout,location));
        }


        [Route("available/details/{checkin:DateTime}/{checkOut:DateTime}/{location}/{HasTV:bool}/{HasHairDryer:bool}/{minCategory:int}/{type:int}/{HasWifi:bool}/{HasParking:bool}/{minPrice:decimal}/{maxPrice:decimal}")]
        [HttpGet]
        public IHttpActionResult GetRoomsAvailableDetails(DateTime checkIn, DateTime checkOut, string location, bool HasTV, bool HasHairDryer,
            int minCategory, int type, bool HasWifi, bool HasParking, decimal minPrice, decimal maxPrice)
        {
            return Ok(RoomDB.GetRoomsAvailableDetails(checkIn, checkOut, location, HasTV, HasHairDryer, minCategory, type, HasWifi, HasParking, minPrice, maxPrice));
        }

        [Route("available/group/{nbGuests}/{checkIn:DateTime}/{checkOut:DateTime}")]
        [HttpGet]
        public  IHttpActionResult FindVariousRoomsForGroup(int nbGuests, DateTime checkIn, DateTime checkOut)
        {
            return Ok(RoomDB.FindVariousRoomsForGroup(nbGuests, checkIn, checkOut));
        }

        
    }
}
