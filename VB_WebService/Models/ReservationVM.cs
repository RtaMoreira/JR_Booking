using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VB_WebService.Models
{
    public class ReservationVM
    {
        public string firstname;
        public string lastname;
        public DateTime checkIn;
        public DateTime checkOut;
        public int numberGuest;
        public decimal finalPrice;
        public List<int> roomSelected;
    }
}