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
    public class RoomController : ApiController
    {
        private static readonly HttpClient Httpclient;

        [Route("rooms")]
        [HttpGet]
        public IHttpActionResult GetAllRooms()
        {
            return Ok(EF_RoomManager.GetAllRooms());
        }
    }
}
