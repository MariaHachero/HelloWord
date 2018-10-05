using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCash.Models.Piezas
{
    public class Tablero
    {
        public Tablero()
        {
            this.FichasDisparo = new Dictionary<Posicion, IList<FichaDisparo>>();
        }

        public int Size { get; set; }

        public Celda[,] Celdas { get; set; }

        public static Tablero GenerateBoard(int size)
        {
            Tablero tablero = new Tablero();
            tablero.Size = size;
            tablero.Celdas = new Celda[size, size];

            return tablero;
        }

        public IDictionary<Posicion, IList<FichaDisparo>> FichasDisparo { get; set; }

        public IList<FichaDisparo> FichasArriba { get; set; }

        public IList<FichaDisparo> FichasAbajo { get; set; }

        IList<Celda> celdasYaChequeadas = new List<Celda>();
        //solo se le pone el get porque no queremos setearla en ningún momento
        public int FaltanRomper
        {
            get
            {
                int cantidad = 0;
                foreach(var celda in Celdas)
                {
                    if (celda.Ficha != null) cantidad++;
                }
                return cantidad;
            }
        }

        public bool QuedanCeldasLibres
        {
            get
            {
                foreach (var celda in Celdas)
                {
                    if (celda.Ficha == null) return true;
                }
                return false;
            }
        }

        public IList<Celda> NuevoMovimiento(int rowIndex, int colIndex, Ficha fichaNueva)
        {
            celdasYaChequeadas.Clear();
            IList<Celda> celdasARomper = new List<Celda>();

            this.Celdas[rowIndex, colIndex].Ficha = fichaNueva;
            celdasARomper.Add(this.Celdas[rowIndex, colIndex]);
            celdasYaChequeadas.Add(this.Celdas[rowIndex, colIndex]);

            checkCelda(rowIndex, colIndex - 1, fichaNueva, celdasARomper);
            checkCelda(rowIndex, colIndex + 1, fichaNueva, celdasARomper);
            checkCelda(rowIndex - 1, colIndex, fichaNueva, celdasARomper);
            checkCelda(rowIndex + 1, colIndex, fichaNueva, celdasARomper);
            
            if (celdasARomper.Count >= 3)
                return celdasARomper;
            else
                return null;

        }

        private bool yaSeChequeoCelda(int rowIndex, int colIndex)
        {
            return celdasYaChequeadas.Count(t => t.RowIndex == rowIndex && t.ColIndex == colIndex) > 0;
        }

        private void checkCelda(int rowIndex, int colIndex, Ficha fichaNueva, IList<Celda> celdasARomper)
        {
            if (yaSeChequeoCelda(rowIndex, colIndex)) return;

            if (rowIndex < 0 || colIndex < 0 || rowIndex > this.Size - 1 || colIndex > this.Size - 1)
                return;

            if (this.Celdas[rowIndex, colIndex].Ficha != null && this.Celdas[rowIndex, colIndex].Ficha.Color == fichaNueva.Color) { 
                celdasARomper.Add(this.Celdas[rowIndex, colIndex]);
                this.celdasYaChequeadas.Add(this.Celdas[rowIndex, colIndex]);
                checkCelda(rowIndex - 1, colIndex, fichaNueva, celdasARomper);
                checkCelda(rowIndex + 1, colIndex, fichaNueva, celdasARomper);
                checkCelda(rowIndex, colIndex - 1, fichaNueva, celdasARomper);
                checkCelda(rowIndex, colIndex + 1, fichaNueva, celdasARomper);
            }
        }

        public static Tablero GenerateBoardFromString(string tableroStr)
        {
            string[] lineas = tableroStr.Split('\n');
            Tablero t = Tablero.GenerateBoard(lineas.Length);

            int filaIndex = 0;
            foreach(string linea in lineas)
            {
                string lineaLimpia = linea.Trim().ToUpper();
                int colIndex = 0;
                foreach(char ficharChar in lineaLimpia)
                {
                    if (ficharChar == 'X') t.Celdas[filaIndex, colIndex] = new Celda(filaIndex, colIndex);
                    if (ficharChar == '1') t.Celdas[filaIndex, colIndex] = new Celda(filaIndex, colIndex, new Ficha(ColorFicha.Rojo));
                    if (ficharChar == '2') t.Celdas[filaIndex, colIndex] = new Celda(filaIndex, colIndex, new Ficha(ColorFicha.Azul));
                    if (ficharChar == '3') t.Celdas[filaIndex, colIndex] = new Celda(filaIndex, colIndex, new Ficha(ColorFicha.Verde));
                    if (ficharChar == '4') t.Celdas[filaIndex, colIndex] = new Celda(filaIndex, colIndex, new Ficha(ColorFicha.Amarillo));
                    if (ficharChar == 'R') t.Celdas[filaIndex, colIndex] = new Celda(filaIndex, colIndex, new Ficha(Ficha.GetRandomColorFicha()));

                    colIndex++;
                }

                filaIndex++;
            }

            return t;
        }

        public override string ToString()
        {
            string tableroStr = "";

            for(var filaIndex = 0; filaIndex < this.Size; filaIndex++)
            {
                for(var columnaIndex = 0; columnaIndex < this.Size; columnaIndex++)
                {
                    if (this.Celdas[filaIndex, columnaIndex].Ficha == null)
                        tableroStr += "X";
                    else
                        tableroStr += ((int)this.Celdas[filaIndex, columnaIndex].Ficha.Color).ToString();
                }

                tableroStr += "\n";
            }

            return tableroStr;
        }
    }
}
