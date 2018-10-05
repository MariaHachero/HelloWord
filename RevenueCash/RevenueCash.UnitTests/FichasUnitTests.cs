using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevenueCash.Models.Piezas;

namespace RevenueCash.UnitTests
{
    [TestClass]
    public class FichasUnitTests
    {
        [TestMethod]
        public void TestWhen_GeneratingPieceColor()
        {
            ColorFicha newColor = Ficha.GetRandomColorFicha();

            for (int vez = 1; vez <= 100; vez++)
            {
                ColorFicha otroColor = Ficha.GetRandomColorFicha();
                if (otroColor != newColor)
                    return;
            }

            Assert.Fail("No se generaron dos color distintos en 100 pasadas");
        }

        [TestMethod]
        public void TestWhen_GeneratingPieces()
        {
            Ficha newFicha = Ficha.GeneratePiece();

            for (int vez = 1; vez <= 100; vez++)
            {
                Ficha otraFicha = Ficha.GeneratePiece();
                if (otraFicha.Color != newFicha.Color)
                    return;
            }

            Assert.Fail("No se generaron dos fichas de color distinto en 100 pasadas");
        }
    }
}
