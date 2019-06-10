using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB_EF
{
    public class ReservationDB
    {
        static VB_ModelContainer context = new VB_ModelContainer();

        public static String AddReservation(Reservation r)
        {

            foreach (Room room in r.Rooms)
            {

                //Control if rooms not already taken during the process
                if (RoomDB.verifyRoomStillAvailable(room.IdRoom, r.CheckIn, r.CheckOut) == false)
                {
                    return "Une de vos chambres n'est plus disponible";
                }
            }

            context.Reservations.Add(r);
            context.SaveChanges();
            return null;
        }

        public static int DeleteReservation(int idReservation, string firstname, string lastname)
        {
            var q = from res in context.Reservations
                    where res.IdReservation == idReservation
                    && res.Firstname.Equals(firstname)
                    && res.Lastname.Equals(lastname)
                    select res;

            if (q.Count() > 0)
            {
                context.Database.ExecuteSqlCommand("DELETE FROM ReservationRoom WHERE Reservations_IdReservation = @id", new SqlParameter("@id", idReservation));
                context.Database.ExecuteSqlCommand("DELETE FROM Reservations WHERE IdReservation = @id AND Firstname = @Firstname AND Lastname = @Lastname", new SqlParameter("@id", idReservation), new SqlParameter("@Firstname", firstname), new SqlParameter("@Lastname", lastname));
                context.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public static Reservation GetReservationWithFirstname(string firstname, string lastname, DateTime checkIn)
        {
            var q = from res in context.Reservations
                    where res.Firstname.Equals(firstname)
                    && res.Lastname.Equals(lastname)
                    && res.CheckIn == checkIn
                    select res;
            Reservation r = q.FirstOrDefault();
            return r;
        }
    }
}
