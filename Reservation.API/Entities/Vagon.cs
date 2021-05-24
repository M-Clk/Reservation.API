using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.Entities
{
    public class Vagon
    {
        public string Ad { get; set; }
        public int Kapasite { get; set; }
        public int DoluKoltukAdet { get; set; }
        public Tren Tren { get; set; }
    }
}
