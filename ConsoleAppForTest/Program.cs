using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VB_EF;

namespace ConsoleAppForTest
{
    class Program
    {
        static void Main(string[] args)
        {
             VB_ModelContainer context = new VB_ModelContainer();

            var q = from r in context.Rooms
                    select r;

            foreach (Room room in q)
            {
                Console.WriteLine(room.IdRoom + " : " + room.Description);

            }

            Console.ReadKey();
        }
    }
}
