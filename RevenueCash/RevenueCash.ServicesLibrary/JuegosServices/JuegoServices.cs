using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevenueCash.Models.Juego;
using RevenueCash.Models.Piezas;
using RevenueCash.ServicesLibrary.Models;
using RevenueCash.Models;

namespace RevenueCash.ServicesLibrary.JuegosServices
{
    public class JuegoServices : IJuegosServices
    {
        public Game ComenzarNuevoJuego(int nivel)
        {
            Game newGame = new Game(GameDifficulty.Easy);
            newGame.CurrentLevel = this.GetLevel(1);
            newGame.Board = Tablero.GenerateBoardFromString(newGame.CurrentLevel.StringLevel);
            return newGame;
        }

        public Celda DispararFicha(Game juego, Posicion desdeDonde, int indice)
        {
            if(desdeDonde == Posicion.Arriba)
            {
                // el primero vamos a recorrer de arriba a abajo hasta que se encuentre con alguno
                int indiceTope = -1;
                for(int fila = 0; fila < juego.Board.Size; fila++)
                {
                    //significa que tenemos una ficha
                    if(juego.Board.Celdas[fila, indice].Ficha != null)
                    {
                        if (fila == 0) return null;
                        indiceTope = fila - 1;
                        break;
                    }
                }
                if (indiceTope == -1) return null;
                return juego.Board.Celdas[indiceTope, indice];
            }
            //de izquierda a derecha
            if (desdeDonde == Posicion.Izquierda)
            {
                // el primero vamos a recorrer de arriba a abajo hasta que se encuentre con alguno
                int indiceTope = -1;
                for (int columna = 0; columna < juego.Board.Size; columna++)
                {
                    //significa que tenemos una ficha
                    if (juego.Board.Celdas[indice, columna].Ficha != null)
                    {
                        if (columna == 0) return null;
                        indiceTope = columna - 1;
                        break;
                    }
                }
                if (indiceTope == -1) return null;
                return juego.Board.Celdas[indice, indiceTope];
            }
            //de abajo a arriba
            if (desdeDonde == Posicion.Abajo)
            {
                // el primero vamos a recorrer de arriba a abajo hasta que se encuentre con alguno
                int indiceTope = -1;
                for (int fila = juego.Board.Size -1; fila >= 0; fila--)
                {
                    //significa que tenemos una ficha
                    if (juego.Board.Celdas[fila, indice].Ficha != null)
                    {
                        if (fila == juego.Board.Size -1) return null;
                        indiceTope = fila + 1;
                        break;
                    }
                }
                if (indiceTope == -1) return null;
                return juego.Board.Celdas[indiceTope, indice];
            }

            if (desdeDonde == Posicion.Derecha)
            {
                // el primero vamos a recorrer de arriba a abajo hasta que se encuentre con alguno
                int indiceTope = -1;
                for (int columna = juego.Board.Size - 1; columna >= 0; columna--)
                {
                    //significa que tenemos una ficha
                    if (juego.Board.Celdas[indice, columna].Ficha != null)
                    {
                        if (columna == juego.Board.Size - 1) return null;
                        indiceTope = columna + 1;
                        break;
                    }
                }
                if (indiceTope == -1) return null;
                return juego.Board.Celdas[indice, indiceTope];
            }
            return null;
            //juego.Board.FichasDisparo[desdeDonde][indice].Color = Ficha.GetRandomColorFicha();
            //return juego;
        }

        public Level GetLevel(int level)
        {
          if(level <= Configuration.Levels.Count)
            {
                return Configuration.Levels[level - 1];
            }
          //maria : crear la excepcion
            throw new Exception("Level not found");
        }

        public IList<FichaDisparo> GetFichasDisparo(int size, Posicion desdeDonde)
        {
            IList<FichaDisparo> fichas = new List<FichaDisparo>();
            for(int indice = 0; indice < size; indice++)
            {
                FichaDisparo ficha = new FichaDisparo(Ficha.GetRandomColorFicha(), desdeDonde, indice);
                fichas.Add(ficha);
            }
            return fichas;
        }

        public MovimientoFicha NuevoMovimiento(Game juego, Posicion desdeDonde, int indice)
        {

            MovimientoFicha movimeinto = new MovimientoFicha();
            Celda dondePonerFicha = this.DispararFicha(juego, desdeDonde, indice);

            if (dondePonerFicha != null)
            {
                IList<Celda> celdasARomper = juego.Board.NuevoMovimiento(dondePonerFicha.RowIndex, dondePonerFicha.ColIndex, new Ficha(juego.Board.FichasDisparo[desdeDonde][indice].Color));
                if(celdasARomper != null)
                {
                    movimeinto.CeldasRotas = celdasARomper;
                    movimeinto.PuntosGanados = celdasARomper.Count() * 10;

                    //
                    juego.Score += movimeinto.PuntosGanados;

                    //para romper las celdas
                    foreach (var celda in celdasARomper)
                    {
                        juego.Board.Celdas[celda.RowIndex, celda.ColIndex].Ficha = null;
                    }
                }
                //sustituir la ficha
                juego.Board.FichasDisparo[desdeDonde][indice].Color = Ficha.GetRandomColorFicha();

            }

            movimeinto.Juego = juego;
            return movimeinto;
        }

        public Game GetNextLevel(Game actualGame)
        {
            actualGame.Score = 0;
            actualGame.CurrentLevel = actualGame.CurrentLevel.NextLevel;
            actualGame.Board = Tablero.GenerateBoardFromString(actualGame.CurrentLevel.StringLevel);

            return actualGame;
        }
    }
}
