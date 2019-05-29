using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RoomDTO
    {
        public int IdRoom { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public short Type { get; set; }
        public decimal Price { get; set; }
        public bool HasTV { get; set; }
        public bool HasHairDryer { get; set; }
        public int Hotel_IdHotel { get; set; }

        public RoomDTO(int IdRoom, int Number, string Description, short Type, decimal Price, bool HasTV, bool HasHairDryer, int Hotel_IdHotel)
        {
            this.IdRoom = IdRoom;
            this.Number = Number;
            this.Description = Description;
            this.Type = Type;
            this.Price = Price;
            this.HasTV = HasTV;
            this.HasHairDryer = HasHairDryer;
            this.Hotel_IdHotel = Hotel_IdHotel;
        }

    }
}
