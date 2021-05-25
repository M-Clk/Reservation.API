using System.Collections.Generic;

namespace Reservation.API.Entities
{
    public class Tren
    {
        public string Ad { get; set; }
        public List<Vagon> Vagonlar { get; set; }
    }
}
