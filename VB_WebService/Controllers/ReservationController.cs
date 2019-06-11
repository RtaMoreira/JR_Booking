using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VB_EF;
using System.Data.SqlClient;

namespace VB_WebService.Controllers
{
    [RoutePrefix("reservation")]
    public class ReservationController : ApiController
    {

/*
{
    "firstname": "joao",
    "lastname": "silva",
    "numberGuest": "2",
    "checkIn": "2020-01-01",
    "checkOut": "2020-01-03",
    "finalPrice": 100,
    "roomSelected": [35]
}
*/
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddReservation([FromBody] Reservation r)
        {
            //Reservation r = new Reservation
            //{
            //    Firstname = "test",
            //    Lastname = "testing",
            //    numberOfGuest = 2,
            //    CheckIn = new DateTime(2020, 01, 04),
            //    CheckOut = new DateTime(2020, 01, 06),
            //    FinalPrice = 200
            //};
            //r.Rooms.Add(RoomDB.GetRoom(35));

            return Ok(ReservationDB.AddReservation(r));
        }

        ///reservation/delete/4/joao/silva
        [Route("delete/{idReservation:int}/{firstname}/{lastname}")]
        [HttpDelete]
        public IHttpActionResult DeleteReservation(int idReservation, string firstname, string lastname)
        {
            return Ok(ReservationDB.DeleteReservation(idReservation, firstname, lastname));
        }

        ///reservation/show/joao/silva/2020-01-01
        [Route("show/{firstname}/{lastname}/{checkin:DateTime}")]
        [HttpGet]
        public IHttpActionResult GetReservationWithFirstname(string firstname, string lastname, DateTime checkIn)
        {
            return Ok(ReservationDB.GetReservationWithFirstname(firstname, lastname, checkIn));
        }
    }
}
