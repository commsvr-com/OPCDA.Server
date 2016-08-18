//Configuration.cs - przechowuje konfiguracje programu (właściwości linii, czcionek, użyte kolory)
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace SchemaGenerator
{
    static class Configuration
    {
        // rodzaje linii:
        public static Pen serverFrame = new Pen(Color.Red, 3);
        public static Pen iconFrame = new Pen(Color.LightBlue, 3);
        public static Pen labelFrame = new Pen(Color.Red, 2);
        public static Pen ethernetPen = new Pen(Color.DarkBlue, 2);
        public static Pen channelPen = new Pen(Color.DarkGreen, 2);
        public static Pen protocolPen = new Pen(Color.MediumSeaGreen, 2);
        public static Pen stationPen = new Pen(Color.Blue, 2);
        public static Brush segmentBr = Brushes.GreenYellow;
        public static Brush protocolBr = Brushes.LawnGreen;
        public static Brush channelBr = Brushes.YellowGreen;
        // kolory połączeń stacji
        public static Color[] stationColors = new Color[6] { Color.Orange, Color.Violet, Color.Chocolate, Color.DarkKhaki, Color.Plum, Color.Turquoise };
        // rodzaje czcionek
        public static Font labelFont;
        public static Font labelFont2;
        public static Font infoFont;
    }
}
