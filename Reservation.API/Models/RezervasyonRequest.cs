using Reservation.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.API.Models
{
    public class RezervasyonRequest
    {
        public Tren Tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}
