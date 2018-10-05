using RevenueCash.Models.Juego;
using RevenueCash.Models.Piezas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCash.ServicesLibrary.Models
{
    public class MovimientoFicha
    {
        public MovimientoFicha()
        {

        }
        public Game Juego { get; set; }
        public IList<Celda> CeldasRotas { get; set; }
        public int PuntosGanados { get; set; }
    }
}
