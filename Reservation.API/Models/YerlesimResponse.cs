using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.Models
{
    public class YerlesimResponse
    {
        public bool RezervasyonYapilabilir { get; set; }
        public List<VagonYerlesimResponse> YerlesimAyrinti { get; set; }
    }
}
