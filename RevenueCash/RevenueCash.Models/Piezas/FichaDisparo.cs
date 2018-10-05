using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCash.Models.Piezas
{
    public class FichaDisparo : Ficha
    {
        public FichaDisparo(ColorFicha color, Posicion desdeDonde, int indice) : base(color)
        {
            DesdeDonde = desdeDonde;
            Indice = indice;
        }

        public Posicion DesdeDonde { get; set; }

        public int Indice { get; set; }
    }
}
