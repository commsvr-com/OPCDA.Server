//Block.cs - implementuje typy bloków występujących w schemacie
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace SchemaGenerator
{
    public enum blockType : int // reprezentuje typ bloku
    {
        server = 1,
        lan = 2,
        channel = 3,
        protocol = 4,
        segment = 5,
        station = 6
    };
    /// <summary>
    /// Klasa reprezentująca pusty blok
    /// </summary>
    public class SimpleBlock
    {
        public int x; // współrzędna X lewego górnego narożnika bloku
        public int y; // współrzędna Y lewego górnego narożnika bloku
        public int height; // wysokość bloku
        public int width; // szerokość bloku
        public Rectangle rec; // przechowuje dane o bloku
        public Brush brush; // określa kolor wnetrza bloku
        public blockType type; // określa typ bloku
        /// <summary>
        /// Funkcja sprawdzająca czy zmieniana wartość nie przekroczyła limitów (zapewnia że punkt podłączenia linni nie wychodzi poza obręb danego bloku)
        /// </summary>
        /// <param name="value">edytowana wartość</param>
        /// <param name="offset">przesunięcie</param>
        /// <returns>nowa wartość</returns>
        public int CheckBlockLimits(int value, int offset)
        {
            value = value + offset;
            if (value < 0)
            {
                value = 0;
            }
            else if (value > this.width)
            {
                value = this.width;
            }
            return value;
        }
    }

    /// <summary>
    /// Klasa reprezentująca blok wypełniony etykietą
    /// </summary>
    public class Block : SimpleBlock
    {
        public string text; // przechowuje tekst etykiety bloku
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="x">współrzędna X lewego górnego narożnika bloku</param>
        /// <param name="y">współrzędna Y lewego górnego narożnika bloku</param>
        /// <param name="height">wysokość bloku</param>
        /// <param name="width">szerokość bloku</param>
        /// <param name="text">treść etykiety bloku</param>
        /// <param name="type">typ bloku</param>
        public Block(int x, int y, int height, int width, string text, blockType type)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            this.rec = new Rectangle(x, y, width, height);
            this.type = type;
            switch (type)
            {
                case blockType.protocol:
                    this.text = "Protokół: \n" + text;
                    this.brush = Configuration.protocolBr;
                    break;
                case blockType.segment:
                    this.text = "Segment: \n" + text;
                    this.brush = Configuration.segmentBr;
                    break;
                case blockType.channel:
                    this.text = text;
                    this.brush = Configuration.channelBr;
                    break;
            }
        }
        /// <summary>
        /// Rysuje dany blok
        /// </summary>
        /// <param name="g">obszar rysowania klasy Graphics</param>
        /// <param name="isActive">true = blok aktywny obramowany linią przerywaną, false = zwykły blok obramowany linią ciągłą</param>
        public void DrawBlock(Graphics g, bool isActive)
        {
            this.rec = new Rectangle(x, y, width, height);
            if (isActive)
            {
                Configuration.labelFrame.DashStyle = DashStyle.Dot;
            }
            else
            {
                Configuration.labelFrame.DashStyle = DashStyle.Solid;
            }
            g.DrawRectangle(Configuration.labelFrame, rec);
            g.FillRectangle(brush, rec.X + 1, rec.Y + 1, rec.Width - 2, rec.Height - 2);
            switch(type)
            {
            case blockType.protocol:
                g.DrawString(text, Configuration.labelFont2, Brushes.Black, rec.X + 5, rec.Y + (int)Math.Floor((double)(GraphGUIMainForm.iconSz / 5)));
                break;
            case blockType.segment:
                g.DrawString(text, Configuration.labelFont2, Brushes.Black, rec.X + 5, rec.Y + (int)Math.Floor((double)(GraphGUIMainForm.iconSz / 5)));
                break;
            case blockType.channel:
                g.DrawString(text, Configuration.labelFont, Brushes.Black, rec.X + 5, rec.Y + 2);
                break;
            }
        }
    }

    /// <summary>
    /// Klasa reprezentująca blok wypełniony ikoną
    /// </summary>
    public class Icon : SimpleBlock
    {
        public Bitmap pic; // obrazek ikonki
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="x">współrzędna X lewego górnego narożnika bloku</param>
        /// <param name="y">współrzędna Y lewego górnego narożnika bloku</param>
        /// <param name="height">wysokość bloku</param>
        /// <param name="width">szerokość bloku</param>
        /// <param name="pic">treść etykiety bloku</param>
        /// <param name="type">typ bloku</param>
        public Icon(int x, int y, int height, int width, Bitmap pic, blockType type)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            this.rec = new Rectangle(x, y, width, height);
            this.type = type;
            this.pic = pic;
        }
        /// <summary>
        /// Rysuje dany blok z ikonką
        /// </summary>
        /// <param name="g">obszar rysowania klasy Graphics</param>
        /// <param name="isActive">true = blok aktywny obramowany linią przerywaną, false = zwykły blok obramowany linią ciągłą</param>
        public void DrawBlock(Graphics g, bool isActive)
        {
            this.rec = new Rectangle(x, y, width, height);
            Pen tempFrame = null; // pomocnicza typu Pen opisująca ramkę
            switch (type)
            {
                case blockType.server:
                    tempFrame = Configuration.serverFrame;
                    break;
                case blockType.station:
                    tempFrame = Configuration.labelFrame;
                    break;
                case blockType.lan:
                    tempFrame = Configuration.iconFrame;
                    break;
            }
            if (isActive)
            {
                tempFrame.DashStyle = DashStyle.Dot;
            }
            else
            {
                tempFrame.DashStyle = DashStyle.Solid;
            }
            g.DrawRectangle(tempFrame, rec);
            if (type == blockType.station)
            {
                g.FillRectangle(Configuration.iconFrame.Brush, rec.X + 1, rec.Y + 1, rec.Width - 2, rec.Height - 2);
            }
            g.DrawImage(pic, x + (int)tempFrame.Width, y + (int)tempFrame.Width);
        }

    }
}
