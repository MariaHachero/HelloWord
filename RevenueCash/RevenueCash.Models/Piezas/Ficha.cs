using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCash.Models.Piezas
{
    public class Ficha
    {
        public Ficha()
        {

        }

        public Ficha(ColorFicha color)
        {
            this.Color = color;
        }

        public ColorFicha Color { get; set; }


        private static Random random = new Random();
        public static ColorFicha GetRandomColorFicha()
        {    
            int numeroRandom = random.Next(1, 5);
                         
            return (ColorFicha)numeroRandom;
        }

        public static Ficha GeneratePiece()
        {
            Ficha ficha = new Ficha();
            ficha.Color = Ficha.GetRandomColorFicha();
            return ficha;
        }
    }
}
