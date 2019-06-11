using System;
using System.Collections.Generic;
using System.Web.Http;
using VB_EF;

namespace VB_WebService.Controllers
{
    [RoutePrefix("hotels")]
    public class HotelController : ApiController
    {
        [Route("{id:int}")]
        public IHttpActionResult GetHotel(int id)
        {
            return Ok(HotelDB.GetHotel(id));
        }

        [Route("locations")]
        public IHttpActionResult GetAllLocations()
        {
            return Ok(HotelDB.GetAllLocations());
        }

        public static List<int> GetAllHotels()
        {
            return HotelDB.GetAllHotels();
        }


        public static bool HasReached70(int idHotel, DateTime day)
        {
            return HotelDB.HasReached70(idHotel,day);
        }

    }
}
