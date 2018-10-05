using RevenueCash.Models.Juego;
using RevenueCash.Models.Piezas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCash.Models
{
    public static class Configuration
    {
        static Configuration()
        {
            Levels = new List<Level>();


            Level level1 = new Level()
            {
                StringLevel =

            @"XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXRXXXXX
            XXXXXRXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX",
                LevelNumber = 1
            };
            Levels.Add(level1);




            Level level2 = new Level()
            {
                StringLevel =

           @"XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXRXRXXX
            XXXXRXRXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX",
                LevelNumber = 2
            };
            level1.NextLevel = level2;
            Levels.Add(level2);


            Level level3 = new Level()
            {
                StringLevel =

            @"XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXRXRXXX
            XXXXRXRXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX
            XXXXXXXXXX",
                LevelNumber = 2
            };
            level1.NextLevel = level3;
            Levels.Add(level3);
        }
        public static IList<Level> Levels { get; set; }

    }
}
