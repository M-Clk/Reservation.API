using System.Collections.Generic;

namespace Reservation.API.Entities
{
    public class Tren
    {
        public string Ad { get; set; }
        public IEnumerable<Vagon> Vagonlar { get; set; }
    }
}
