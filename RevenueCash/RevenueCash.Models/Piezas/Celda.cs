using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCash.Models.Piezas
{
    public class Celda
    {
        public Celda(int rowIndex, int colIndex)
        {
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
            this.Ficha = null;
        }

        public Celda(int rowIndex, int colIndex, Ficha ficha) : this(rowIndex, colIndex)
        {
            this.Ficha = ficha;
        }

        public int RowIndex { get; private set; }

        public int ColIndex { get; private set; }

        

        public Ficha Ficha { get; set; }

    }
}
