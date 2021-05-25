using Microsoft.AspNetCore.Mvc;
using Reservation.API.Entities;
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
            Dictionary<string, int> vagonYeniKisiler = new Dictionary<string, int>();
            foreach(var vagon in tren.Vagonlar)
            {
                var bosYer = vagon.Kapasite * 70 / 100 - vagon.DoluKoltukAdet;
                if(bosYer < 1)
                    continue;
                if(!request.KisilerFarkliVagonlaraYerlestirilebilir && bosYer > request.RezervasyonYapilacakKisiSayisi)
                {
                    response.YerlesimAyrinti.Add(new VagonYerlesimResponse { VagonAdi = vagon.Ad, KisiSayisi = request.RezervasyonYapilacakKisiSayisi });
                    response.RezervasyonYapilabilir = true;
                    vagonYeniKisiler.Add(vagon.Ad, request.RezervasyonYapilacakKisiSayisi);
                    VagonKapsiteGuncelle(tren.Ad, vagonYeniKisiler);
                    return response;
                }
                else if(request.KisilerFarkliVagonlaraYerlestirilebilir)
                {
                    if(bosYer > request.RezervasyonYapilacakKisiSayisi)
                    {
                        response.YerlesimAyrinti.Add(new VagonYerlesimResponse { VagonAdi = vagon.Ad, KisiSayisi = request.RezervasyonYapilacakKisiSayisi });
                        response.RezervasyonYapilabilir = true;
                        vagonYeniKisiler.Add(vagon.Ad, request.RezervasyonYapilacakKisiSayisi);
                        VagonKapsiteGuncelle(tren.Ad, vagonYeniKisiler);
                        return response;
                    }
                    response.YerlesimAyrinti.Add(new VagonYerlesimResponse { VagonAdi = vagon.Ad, KisiSayisi = bosYer });
                    request.RezervasyonYapilacakKisiSayisi -= bosYer;
                    vagonYeniKisiler.Add(vagon.Ad, bosYer);
                }
            }
            if(request.RezervasyonYapilacakKisiSayisi > 0)
            {
                response.RezervasyonYapilabilir = false;
                response.YerlesimAyrinti.Clear();
            }
            else
            {
                VagonKapsiteGuncelle(tren.Ad, vagonYeniKisiler);
                response.RezervasyonYapilabilir = true;
            }
            return response;
        }
        private void VagonKapsiteGuncelle(string trenAdi, Dictionary<string, int> vagonYeniKisiler)
        {
            var toBeUpdatedTren = _reservationDbContext.Trens.First(t => t.Ad == trenAdi);
            foreach(var vagonKisiSayisi in vagonYeniKisiler)
                toBeUpdatedTren.Vagonlar.First(v => v.Ad == vagonKisiSayisi.Key).DoluKoltukAdet += vagonKisiSayisi.Value;
        }
    }
}
