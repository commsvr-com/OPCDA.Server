//Line.cs - implementuje typy linii występujących w schemacie
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace SchemaGenerator
{
    public enum tip : int { none, dot, reddot, arrow, redarrow }; // reprezentuje zakończenie linii
    public enum lineType : int { another, chan_prot, prot_segm, segm_stat }; // reprezentuje typ linii
    public enum linePart : int // reprezentuje odcinek linii
    { 
        downSection = 1,
        middleSection = 2, 
        upperSection = 3
    };
    /// <summary>
    /// Klasa reprezentująca prostą linię
    /// </summary>
    public class SimpleLine
    {
        public int x1; // współrzędna X początku linii
        public int y1; // współrzędna Y początku linii
        public int x2; // współrzędna X końca linii
        public int y2; // współrzędna Y końca linii
        public Pen p; // określa wygląd linii (kolor i grubość)
        /// <summary>
        /// konstruktor bezparametrowy
        /// </summary>
        public SimpleLine()
        {
        }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="x1">współrzędna X początku linii</param>
        /// <param name="y1">współrzędna Y początku linii</param>
        /// <param name="x2">współrzędna X końca linii</param>
        /// <param name="y2">współrzędna Y końca linii</param>
        /// <param name="p">określa wygląd linii (kolor i grubość)</param>
        public SimpleLine(int x1, int y1, int x2, int y2, Pen p)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.p = p;
        }
        /// <summary>
        /// Funkcja sprawdzająca czy zmieniana wartość nie przekroczyła limitów (zapewnia że punkt podłączenia linni nie wychodzi poza obręb danej linii)
        /// </summary>
        /// <param name="value">edytowana wartość</param>
        /// <param name="offset">przesunięcie</param>
        /// <returns>nowa wartość</returns>
        public int CheckLineLimits(int value, int offset)
        {
            value = value + offset;
            int width = x2 - x1;
            if (value < 10)
            {
                value = 10;
            }
            else if (value > width -10)
            {
                value = width - 10;
            }
            return value;
        }
        /// <summary>
        /// Rysuje daną linię
        /// </summary>
        /// <param name="g">obszar rysowania klasy Graphics</param>
        public void DrawLine(Graphics g)
        {
            g.DrawLine(p, x1, y1, x2, y2);
        }
    }

    /// <summary>
    /// Klasa reprezentująca połączenie (linia łamana z odpowiednim zakończeniem)
    /// Linia ta zawsze rysowana jest od dołu (początku) do góry (końca)
    /// </summary>
    public class Line : SimpleLine
    {
        public int yzal; // współrzędna Y załamania linii
        public int id1; // wskazuje na numer indeksu elementu do którego podłaczony jest początek linii
        public int offset1; // przesunięcie socketa od lewej strony powyższego elementu
        public int id2; // wskazuje na numer indeksu elementu do którego podłaczony jest koniec linii
        public int offset2; // przesunięcie socketa od lewej strony powyższego elementu
        public tip begtip; // reprezentuje zakończenie linii na jej początku
        public tip endtip; // reprezentuje zakończenie linii na jej końcu
        public lineType type; // określa typ linii
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="x1">współrzędna X początku linii</param>
        /// <param name="y1">współrzędna Y początku linii</param>
        /// <param name="yzal">współrzędna Y załamania linii</param>
        /// <param name="x2">współrzędna X końca linii</param>
        /// <param name="y2">współrzędna Y końca linii</param>
        /// <param name="p">określa wygląd linii (kolor i grubość)</param>
        /// <param name="begtip">reprezentuje zakończenie linii na jej początku</param>
        /// <param name="endtip">reprezentuje zakończenie linii na jej końcu</param>
        public Line(int x1, int y1, int yzal, int x2, int y2, Pen p, tip begtip, tip endtip)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.yzal = yzal;
            this.x2 = x2;
            this.y2 = y2;
            this.p = p;
            this.begtip = begtip;
            this.endtip = endtip;
            this.type = lineType.another;
        }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id1">numer indeksu elementu do którego podłaczony jest początek linii</param>
        /// <param name="offset1">przesunięcie socketa od lewej strony powyższego elementu</param>
        /// <param name="yzal">współrzędna Y załamania linii</param>
        /// <param name="id2">numer indeksu elementu do którego podłaczony jest koniec linii</param>
        /// <param name="offset2">przesunięcie socketa od lewej strony powyższego elementu</param>
        /// <param name="p">określa wygląd linii (kolor i grubość)</param>
        /// <param name="begtip">reprezentuje zakończenie linii na jej początku</param>
        /// <param name="endtip">reprezentuje zakończenie linii na jej końcu</param>
        public Line(int id1, int offset1, int yzal, int id2, int offset2, Pen p, tip begtip, tip endtip, lineType type)
        {
            this.id1 = id1;
            this.offset1 = offset1;
            this.id2 = id2;
            this.offset2 = offset2;
            this.yzal = yzal;
            this.p = p;
            this.begtip = begtip;
            this.endtip = endtip;
            this.type = type;
            switch (type)
            {
                case lineType.chan_prot:
                    this.x1 = 0;
                    this.y1 = 0;
                    this.x2 = 0;
                    this.y2 = 0;
                    break;
                case lineType.prot_segm:
                    this.x1 = 0;
                    this.y1 = 0;
                    this.x2 = 0;
                    this.y2 = 0;
                    break;
                case lineType.segm_stat:
                    this.x1 = 0;
                    this.y1 = 0;
                    this.x2 = 0;
                    this.y2 = 0;
                    break;
            }
        }
        /// <summary>
        /// Rysuje daną linię
        /// </summary>
        /// <param name="g">obszar rysowania klasy Graphics</param>
        /// <param name="isActive">true = linia aktywna rysowana linią przerywaną, false = zwykła linia ciągła</param>
        public void DrawLine(Graphics g, bool isActive)
        {
            if (type == lineType.chan_prot)
            {
                x1 = GraphGUIMainForm.serverChannelSocket[id1].X + offset1;
                y1 = GraphGUIMainForm.serverChannelSocket[id1].Y;
                x2 = GraphGUIMainForm.protocol[id2].rec.Left + offset2;
                y2 = GraphGUIMainForm.protocol[id2].rec.Bottom;
            }
            else if (type == lineType.prot_segm)
            {
                x1 = GraphGUIMainForm.protocol[id1].rec.Left + offset1;
                y1 = GraphGUIMainForm.protocol[id1].rec.Top;
                x2 = GraphGUIMainForm.segment[id2].rec.Left + offset2;
                y2 = GraphGUIMainForm.segment[id2].rec.Bottom;
            }
            else if (type == lineType.segm_stat)
            {
                x1 = GraphGUIMainForm.segment[id1].rec.Left + offset1;
                y1 = GraphGUIMainForm.segment[id1].rec.Top;
                x2 = GraphGUIMainForm.station[id2].rec.Left + offset2;
                y2 = GraphGUIMainForm.station[id2].rec.Bottom;
            }
            if (isActive)
            {
                p.DashStyle = DashStyle.Dot;
            }
            else
            {
                p.DashStyle = DashStyle.Solid;
            }
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(x1, y1, x1, yzal);
            gp.AddLine(x2, yzal, x2, y2);
            g.DrawPath(p, gp);
            switch (begtip)
            {
                case tip.dot:
                    PrintDot(g, x1, y1, false);
                    break;
                case tip.reddot:
                    PrintDot(g, x1, y1, true);
                    break;
                case tip.arrow:
                    PrintArrow(g, x1, y1, false, false); // strzałka skierowana do dołu
                    break;
                case tip.redarrow:
                    PrintArrow(g, x1, y1, false, true); // strzałka skierowana do dołu (czerwona)
                    break;
            }
            switch (endtip)
            {
                case tip.dot:
                    PrintDot(g, x2, y2, false);
                    break;
                case tip.reddot:
                    PrintDot(g, x2, y2, true);
                    break;
                case tip.arrow:
                    PrintArrow(g, x2, y2, true, false); // strzałka skierowana do góry
                    break;
                case tip.redarrow:
                    PrintArrow(g, x2, y2, true, true); // strzałka skierowana do góry (czerwona)
                    break;
            }
        }
        /// <summary>
        /// Rysuje zamalowane koła symbolizujące węzły połaczeń
        /// </summary>
        /// <param name="g">obszar rysowania klasy Graphics</param>
        /// <param name="x">współrzędna X środka węzła</param>
        /// <param name="y">współrzędna Y środka węzła</param>
        /// <param name="isRed">true = kropka czerwona, false = kropka koloru takiego jak linia</param>
        private void PrintDot(Graphics g, int x, int y, bool isRed)
        {
            Rectangle r = new Rectangle();
            r.X = x1 - (int)p.Width * 2 - 1;
            r.Y = y1 - (int)p.Width * 2 - 1;
            r.Size = new Size((int)p.Width * 4, (int)p.Width * 4);
            if (isRed)
            {
                g.FillEllipse(Brushes.Red, r);
            }
            else
            {
                g.FillEllipse(p.Brush, r);
            }
        }
        /// <summary>
        /// Rysuje groty strzałek na liniach łączących
        /// </summary>
        /// <param name="g">obszar rysowania klasy Graphics</param>
        /// <param name="x">współrzędna x punktu wskazywanego przez grot strzałki</param>
        /// <param name="y">współrzędna y punktu wskazywanego przez grot strzałki</param>
        /// <param name="isUp">true = strzałka skierowana do góry, false = do dołu</param>
        /// <param name="isRed">true = strzałka czerwona, false = strzałka koloru takiego jak linia</param>
        private void PrintArrow(Graphics g, int x, int y, bool isUp, bool isRed)
        {
            //p.LineJoin = LineJoin.Round;
            GraphicsPath gp1 = new GraphicsPath();
            if (isUp) // strzałka do góry
            {
                gp1.AddLine(x, y, x - 5, y + 5);
                gp1.AddLine(x - 5, y + 5, x + 5, y + 5);
            }
            else // strzałka do dołu
            {
                gp1.AddLine(x, y, x - 5, y - 5);
                gp1.AddLine(x - 5, y - 5, x + 5, y - 5);
            }
            g.DrawPath(p, gp1);
            if (isRed)
            {
                g.FillPath(Brushes.Red, gp1);
            }
            else
            {
                g.FillPath(p.Brush, gp1);
            }
        }
        /// <summary>
        /// Identyfikuje do którego odcinka linii (dolnego, środkowego-poziomego, czy górnego) należy punkt (x,y)
        /// </summary>
        /// <param name="x">współrzędna X punktu</param>
        /// <param name="y">współrzędna Y punktu</param>
        /// <returns></returns>
        public linePart IdentifyLineSection(int x, int y)
        {
            if (x >= (x1 - 2) & x <= (x1 + 2) & y >= yzal & y <= y1) return linePart.downSection;
            else if (x >= (x2 - 2) & x <= (x2 + 2) & y >= y2 & y <= yzal) return linePart.upperSection;
            else return linePart.middleSection;
        }
    }
}