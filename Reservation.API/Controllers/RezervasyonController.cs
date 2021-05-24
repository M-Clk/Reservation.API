using Microsoft.AspNetCore.Mvc;
using Reservation.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Reservation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RezervasyonController :Controller
    {
        ReservationDbContext _reservationDbContext;
        public RezervasyonController(ReservationDbContext reservationDbContext) => _reservationDbContext = reservationDbContext;

        public string Index()
        {
            return "Aktif Endpoint: Rezervasyon";
        }
        [HttpPost]
        public YerlesimResponse Index(RezervasyonRequest request)
        {
            var response = new YerlesimResponse { RezervasyonYapilabilir = false, YerlesimAyrinti = new List<VagonYerlesimResponse>() };
            var tren = _reservationDbContext.Trens.FirstOrDefault(t => t.Ad.Contains(request.Tren.Ad) && t.Vagonlar.Any(v => v.Kapasite * 70 / 100 > v.DoluKoltukAdet));
            if(tren == null)
                return response;

            foreach(var vagon in tren.Vagonlar)
            {
                var bosYer = vagon.Kapasite * 70 / 100 - vagon.DoluKoltukAdet;
                if(bosYer < 1)
                    continue;
                if(!request.KisilerFarkliVagonlaraYerlestirilebilir && bosYer > request.RezervasyonYapilacakKisiSayisi)
                {
                    response.YerlesimAyrinti.Add(new VagonYerlesimResponse { VagonAdi = vagon.Ad, KisiSayisi = bosYer });
                    return response;
                }
                else if(request.KisilerFarkliVagonlaraYerlestirilebilir)
                {
                    if(bosYer > request.RezervasyonYapilacakKisiSayisi)
                    {

                        response.YerlesimAyrinti.Add(new VagonYerlesimResponse { VagonAdi = vagon.Ad, KisiSayisi = request.RezervasyonYapilacakKisiSayisi });
                        return response;
                    }
                    response.YerlesimAyrinti.Add(new VagonYerlesimResponse { VagonAdi = vagon.Ad, KisiSayisi = bosYer });
                    request.RezervasyonYapilacakKisiSayisi -= bosYer;
                }
            }
            if(request.RezervasyonYapilacakKisiSayisi > 0)
            {
                response.RezervasyonYapilabilir = false;
                response.YerlesimAyrinti.Clear();
            }
            return response;
        }
    }
}
