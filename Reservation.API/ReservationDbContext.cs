using Microsoft.EntityFrameworkCore;
using Reservation.API.Entities;
using System.Collections.Generic;

namespace Reservation.API
{
    public class ReservationDbContext :DbContext
    {
        public ReservationDbContext()
        {
            Trens = new List<Tren>
            {
                new Tren{
                    Ad = "Guney Kurtalan",
                    Vagonlar = new List<Vagon>
                    {
                        new Vagon
                        {
                            Ad = "1 Numara",
                            DoluKoltukAdet = 49,
                            Kapasite = 75
                        },
                        new Vagon
                        {
                            Ad = "2 Numara",
                            DoluKoltukAdet = 29,
                            Kapasite = 100,
                        },
                        new Vagon
                        {
                            Ad = "3 Numara",
                            DoluKoltukAdet = 0,
                            Kapasite = 100,
                        },
                        new Vagon
                        {
                            Ad = "4 Numara",
                            DoluKoltukAdet = 46,
                            Kapasite = 50,
                        },
                        new Vagon
                        {
                            Ad = "5 Numara",
                            DoluKoltukAdet = 60,
                            Kapasite = 100,
                        },
                    }
                },
                new Tren{
                    Ad = "Dogu Express",
                    Vagonlar = new List<Vagon>
                    {
                        new Vagon
                        {
                            Ad = "1 Numara",
                            DoluKoltukAdet = 0,
                            Kapasite = 60,
                        },
                        new Vagon
                        {
                            Ad = "2 Numara",
                            DoluKoltukAdet = 20,
                            Kapasite = 100,
                        },
                        new Vagon
                        {
                            Ad = "3 Numara",
                            DoluKoltukAdet = 100,
                            Kapasite = 100,
                        },
                        new Vagon
                        {
                            Ad = "4 Numara",
                            DoluKoltukAdet = 78,
                            Kapasite = 100,
                        },
                        new Vagon
                        {
                            Ad = "5 Numara",
                            DoluKoltukAdet = 60,
                            Kapasite = 80,
                        },
                    }
                }
            };
        }
        public IEnumerable<Tren> Trens { get; set; }
    }
}
