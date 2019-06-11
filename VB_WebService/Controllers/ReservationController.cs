using System;
using System.Net;
using System.Web.Http;
using VB_EF;

namespace VB_WebService.Controllers
{
    [RoutePrefix("reservation")]
    public class ReservationController : ApiController
    {
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddReservation([FromBody] Reservation r)
        {
            return Ok(ReservationDB.AddReservation(r));
        }
        
        [Route("delete/{idReservation:int}/{firstname}/{lastname}")]
        [HttpDelete]
        public IHttpActionResult DeleteReservation(int idReservation, string firstname, string lastname)
        {
            int value = ReservationDB.DeleteReservation(idReservation, firstname, lastname);

            if (value == 1)
                return Ok();
            else
                return StatusCode(HttpStatusCode.NoContent);

        }
        
        [Route("show/{firstname}/{lastname}/{checkin:DateTime}")]
        [HttpGet]
        public IHttpActionResult GetReservationWithFirstname(string firstname, string lastname, DateTime checkIn)
        {
            return Ok(ReservationDB.GetReservationWithFirstname(firstname, lastname, checkIn));
        }

 

    }
}
