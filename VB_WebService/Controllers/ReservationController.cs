using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VB_EF;
using VB_WebService.Models;
using System.Data.SqlClient;

namespace VB_WebService.Controllers
{
    [RoutePrefix("reservation")]
    public class ReservationController : ApiController
    {
        static VB_ModelContainer context = new VB_ModelContainer();

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
            context.Reservations.Add(r);
            context.SaveChanges();
            return Ok("hello");
        }
        ///reservation/delete/4/joao/silva
        [Route("delete/{idReservation:int}/{firstname}/{lastname}")]
        [HttpDelete]
        public IHttpActionResult DeleteReservation(int idReservation, string firstname, string lastname)
        {
            var q = from res in context.Reservations
                    where res.IdReservation==idReservation
                    && res.Firstname.Equals(firstname)
                    && res.Lastname.Equals(lastname)
                    select res;
            
            if (q.Count() > 0)
            {
                context.Database.ExecuteSqlCommand("DELETE FROM ReservationRoom WHERE Reservations_IdReservation = @id", new SqlParameter("@id", idReservation));
                context.Database.ExecuteSqlCommand("DELETE FROM Reservations WHERE IdReservation = @id AND Firstname = @Firstname AND Lastname = @Lastname", new SqlParameter("@id", idReservation), new SqlParameter("@Firstname", firstname),new SqlParameter("@Lastname", lastname));
                context.SaveChanges();
                return Ok(1);
            }
            else
                return Ok(0);
        }

        ///reservation/show/joao/silva/2020-01-01
        [Route("show/{firstname}/{lastname}/{checkin:DateTime}")]
        [HttpGet]
        public IHttpActionResult GetReservationWithFirstname(string firstname, string lastname, DateTime checkIn)
        {
            var q = from res in context.Reservations
                    where res.Firstname.Equals(firstname)
                    && res.Lastname.Equals(lastname)
                    && res.CheckIn == checkIn
                    select res;
            Reservation r = q.FirstOrDefault();
            return Ok(r);
        }
    }
}
