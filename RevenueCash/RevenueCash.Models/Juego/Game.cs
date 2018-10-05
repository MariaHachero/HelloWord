using RevenueCash.Models.Piezas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCash.Models.Juego
{
    public class Game
    {
        public Game()
        {
            this.State = GameState.SinComenzar;
            this.Score = 0;
            this.Difficulty = GameDifficulty.Easy;
        }

        public Game(GameDifficulty difficulty)
        {
            this.State = GameState.SinComenzar;
            this.Score = 0;
            this.Difficulty = difficulty;
        }

        public GameState State { get; set; }

        public GameDifficulty Difficulty { get; set; }

        public int Score { get; set; }

        public Tablero Board { get; set; }
        public Level CurrentLevel { get; set; }
        public bool NivelFinalizado
        {
            get
            {
                return (this.Board.FaltanRomper == 0 || !this.Board.QuedanCeldasLibres);
            }
        }


        public static Game GenerateNewGame(GameDifficulty difficulty)
        {
            Game newGame = new Game(difficulty);

            int size = 10;
            if (difficulty == GameDifficulty.Hard) size = 20;
            if (difficulty == GameDifficulty.Medium) size = 15;
            if (difficulty == GameDifficulty.HiperHard) size = 30;

            newGame.Board = Tablero.GenerateBoard(size);
            return newGame;
        }
    }
}
