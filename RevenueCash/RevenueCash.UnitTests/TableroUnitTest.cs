using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevenueCash.Models.Piezas;

namespace RevenueCash.UnitTests
{
    [TestClass]
    public class TableroUnitTest
    {
        [TestMethod]
        public void TestWhen_GeneratingNewBoard()
        {
            Tablero tablero = Tablero.GenerateBoard(15);

            Assert.AreEqual(15* 15, tablero.Celdas.Length);
            Assert.AreEqual(15, tablero.Celdas.GetLength(0));
        }
    }
}
