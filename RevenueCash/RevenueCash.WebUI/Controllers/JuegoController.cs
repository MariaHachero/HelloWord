using RevenueCash.Models.Juego;
using RevenueCash.Models.Piezas;
using RevenueCash.ServicesLibrary.JuegosServices;
using RevenueCash.ServicesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RevenueCash.WebUI.Controllers
{
    public class JuegoController : Controller
    {
        private IJuegosServices _juegoServices;

        public JuegoController()
        {
            _juegoServices = new JuegoServices();
        }

        // GET: Juego
        public ActionResult Index()
        {
            if (Session["juegoActual"] != null)
            {
                Game gameActual = Session["juegoActual"] as Game;
                return View("Juego", gameActual);
            }
                
            return View();
        }

        public ActionResult Nuevo()
        {
            Game newGame = _juegoServices.ComenzarNuevoJuego(1);
            newGame.Board.FichasDisparo.Add(Models.Piezas.Posicion.Arriba, _juegoServices.GetFichasDisparo(newGame.Board.Size, Models.Piezas.Posicion.Arriba));
            newGame.Board.FichasDisparo.Add(Models.Piezas.Posicion.Abajo, _juegoServices.GetFichasDisparo(newGame.Board.Size, Models.Piezas.Posicion.Abajo));
            newGame.Board.FichasDisparo.Add(Models.Piezas.Posicion.Derecha, _juegoServices.GetFichasDisparo(newGame.Board.Size, Models.Piezas.Posicion.Derecha));
            newGame.Board.FichasDisparo.Add(Models.Piezas.Posicion.Izquierda, _juegoServices.GetFichasDisparo(newGame.Board.Size, Models.Piezas.Posicion.Izquierda));

            Session["juegoActual"] = newGame;

            return Redirect("/Juego");
        }


        public ActionResult LevelFinished()
        {
            Game juegoActual = Session["juegoActual"] as Game;
            return View(juegoActual);
        }
        public ActionResult Disparar(Posicion desdeDonde, int indice)
        {
            Game juegoActual = Session["juegoActual"] as Game;
            MovimientoFicha movimiento = _juegoServices.NuevoMovimiento(juegoActual, desdeDonde, indice);
            Session["juegoActual"] = movimiento.Juego;

            if (movimiento.Juego.NivelFinalizado)
            {
                return RedirectToAction("LevelFinished");
            }


            //return View("Juego", juegoResultado);
            // probar cambiando esto y poniendo lo de la vista porque es por algo de la url
            return Redirect("/Juego");
        }

        public ActionResult NextLevel()
        {
            Game juegoActual = Session["juegoActual"] as Game;
            juegoActual = _juegoServices.GetNextLevel(juegoActual);

            juegoActual.Board.FichasDisparo.Add(Models.Piezas.Posicion.Arriba, _juegoServices.GetFichasDisparo(juegoActual.Board.Size, Models.Piezas.Posicion.Arriba));
            juegoActual.Board.FichasDisparo.Add(Models.Piezas.Posicion.Abajo, _juegoServices.GetFichasDisparo(juegoActual.Board.Size, Models.Piezas.Posicion.Abajo));
            juegoActual.Board.FichasDisparo.Add(Models.Piezas.Posicion.Derecha, _juegoServices.GetFichasDisparo(juegoActual.Board.Size, Models.Piezas.Posicion.Derecha));
            juegoActual.Board.FichasDisparo.Add(Models.Piezas.Posicion.Izquierda, _juegoServices.GetFichasDisparo(juegoActual.Board.Size, Models.Piezas.Posicion.Izquierda));

            Session["juegoActual"] = juegoActual;

            return Redirect("/Juego");
        }
    }
}