using System;

namespace ClunkerBot.Utilities
{
    class UnitConversionUtlities
    {
        public static double hPA_inHg(double hpaUnit, int round = 2)
        {
            return Math.Round((hpaUnit * 0.029529983071445), round);
        }
    }
}