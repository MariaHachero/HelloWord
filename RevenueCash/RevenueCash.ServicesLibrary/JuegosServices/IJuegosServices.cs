using RevenueCash.Models.Juego;
using RevenueCash.Models.Piezas;
using RevenueCash.ServicesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCash.ServicesLibrary.JuegosServices
{
    public interface IJuegosServices
    {
        Game ComenzarNuevoJuego(int nivel);

        Level GetLevel(int nivel);

        Celda DispararFicha(Game juego, Posicion desdeDonde, int indice);
        MovimientoFicha NuevoMovimiento(Game juego, Posicion desdeDonde, int indice);

        IList<FichaDisparo> GetFichasDisparo(int size, Posicion desdeDonde);
        Game GetNextLevel(Game actualGame);
    }
}
