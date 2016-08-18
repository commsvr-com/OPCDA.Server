//Map.cs - zawiera obiekty i metody realizujące identyfikację obiektów na schemacie
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace SchemaGenerator
{
    static class Map
    {
        public static int[,] surfaceMap;
        /// <summary>
        /// Zaznacza przebieg prostej linii (kanał) w tabeli linesMap w celu późniejszego testowania zawierania kursora
        /// </summary>
        /// <param name="lin">linia prosta która ma zostać naniesiona na mapę</param>
        /// <param name="idx">numer indeksu identyfikujący dana linię w tabeli</param>
        static public void WriteLineOnMap(SimpleLine lin, int idx)
        {
            if (lin.x1 <= GraphGUIMainForm.pW & lin.x2 <= GraphGUIMainForm.pW & lin.y1 <= GraphGUIMainForm.pH & lin.y2 <= GraphGUIMainForm.pH & lin.y1 == lin.y2) // zapobiega błędowi IndexOutOfArray po wyjściu linii poza obręb panelu rysowania
            {
                // nanoszenie linii na mapę (szerokość linii na mapie = 5)
                for (int i = lin.x1; i <= lin.x2; i++)
                {
                    surfaceMap[lin.y1 - 2, i] = 3000 + idx;
                    surfaceMap[lin.y1 - 1, i] = 3000 + idx;
                    surfaceMap[lin.y1, i] = 3000 + idx;
                    surfaceMap[lin.y1 + 1, i] = 3000 + idx;
                    surfaceMap[lin.y1 + 2, i] = 3000 + idx;
                }
            }
        }
        /// <summary>
        /// Zaznacza przebieg linii w tabeli linesMap w celu późniejszego testowania zawierania kursora
        /// </summary>
        /// <param name="lin">linia która ma zostać naniesiona na mapę</param>
        /// <param name="idx">numer indeksu identyfikujący dana linię w tabeli</param>
        static public void WriteLineOnMap(Line lin, int idx)
        {
            if (lin.x1 <= GraphGUIMainForm.pW & lin.x2 <= GraphGUIMainForm.pW & lin.y1 <= GraphGUIMainForm.pH & lin.y2 <= GraphGUIMainForm.pH & lin.yzal <= GraphGUIMainForm.pH) // zapobiega błędowi IndexOutOfArray po wyjściu linii poza obręb panelu rysowania
            {
                // nanoszenie linii na mapę (szerokość linii na mapie = 5)
                for (int i = lin.yzal; i <= lin.y1; i++)
                {
                    surfaceMap[i, lin.x1 - 2] = 8000 + idx;
                    surfaceMap[i, lin.x1 - 1] = 8000 + idx;
                    surfaceMap[i, lin.x1] = 8000 + idx;
                    surfaceMap[i, lin.x1 + 1] = 8000 + idx;
                    surfaceMap[i, lin.x1 + 2] = 8000 + idx;
                }
                for (int i = lin.y2; i <= lin.yzal; i++)
                {
                    surfaceMap[i, lin.x2 - 2] = 8000 + idx;
                    surfaceMap[i, lin.x2 - 1] = 8000 + idx;
                    surfaceMap[i, lin.x2] = 8000 + idx;
                    surfaceMap[i, lin.x2 + 1] = 8000 + idx;
                    surfaceMap[i, lin.x2 + 2] = 8000 + idx;
                }
                // pomocnicze
                int xx1 = lin.x1;
                int xx2 = lin.x2;
                // Ustawiamy zmienne tak aby xx1<=xx2

                if (lin.x1 > lin.x2)
                {
                    xx1 = lin.x2;
                    xx2 = lin.x1;
                }
                for (int i = xx1; i <= xx2; i++)
                {
                    surfaceMap[lin.yzal - 2, i] = 8000 + idx;
                    surfaceMap[lin.yzal - 1, i] = 8000 + idx;
                    surfaceMap[lin.yzal, i] = 8000 + idx;
                    surfaceMap[lin.yzal + 1, i] = 8000 + idx;
                    surfaceMap[lin.yzal + 2, i] = 8000 + idx;
                }
            }
        }
        /// <summary>
        /// Zaznacza miejsce zajmowane przez blok w tabeli linesMap w celu późniejszego testowania zawierania kursora
        /// </summary>
        /// <param name="sb">obiekt który ma zostać naniesiony na mapę</param>
        /// <param name="idx">numer indexu identyfikujący dany blok w tabeli</param>
        static public void WriteBlockOnMap(SimpleBlock sb, int idx)
        {
            int sign = 0;
            switch (sb.type)
            {
                case blockType.server:
                    sign = 1000;
                    break;
                case blockType.lan:
                    sign = 2000;
                    break;
                case blockType.channel:
                    sign = 4000 + idx;
                    break;
                case blockType.protocol:
                    sign = 5000 + idx;
                    break;
                case blockType.segment:
                    sign = 6000 + idx;
                    break;
                case blockType.station:
                    sign = 7000 + idx;
                    break;
            }
            if (sb.x > 0 & sb.y > 0 & sb.x < GraphGUIMainForm.pW - sb.width & sb.y < GraphGUIMainForm.pH - sb.height) // zapobiega błędowi IndexOutOfArray po wyjściu linii poza obręb panelu rysowania
            {
                for (int i = sb.y; i <= sb.y + sb.height; i++)
                {
                    for (int j = sb.x; j <= sb.x + sb.width; j++)
                    {
                        surfaceMap[i, j] = sign;
                    }
                }
            }
        }
        /// <summary>
        /// Funkcja wskazująca nad jakim elementem znajduje się kursor myszy
        /// </summary>
        /// <param name="e">dane o położeniu myszy</param>
        /// <returns>tablica 2 elementowa</returns>
        static public int[] MapContainTest(MouseEventArgs e)
        {
            // zwracana informacja (tablica) o elemencie schematu: [0]-typ [1]-id [2]-(odcinek linii)
            // [0]: 0->nothing, 1->server, 2->scada icon, 3->channel line, 4->channel label, 5->protocol, 6->segment, 7->station, 8->line
            // [1]: nr indexu danego elementu w odpowiedniej tabeli
            // [2]: 1->odcinek początkowy, 2->odcinek środkowy, 3->odcinek końcowy
            int[] containInfo = new int[3] { 0, 0, 0 };
            if (!(e.X >= 0 & e.Y >= 0 & e.X <= GraphGUIMainForm.pW & e.Y <= GraphGUIMainForm.pH)) // zapobiega błędowi IndexOutOfArray tablicy linesMap w metodzie ContainTest
            {
                return containInfo; // zwraca same zera
            }
            else if (surfaceMap[e.Y, e.X] > 0)
            {
                if (surfaceMap[e.Y, e.X] == 1000) // server block
                {
                    containInfo[0] = (int)itemType.server;
                }
                else if (surfaceMap[e.Y, e.X] == 2000) // SCADA Lan block
                {
                    containInfo[0] = (int)itemType.scadaLan;
                }
                else if (surfaceMap[e.Y, e.X] >= 3000 & surfaceMap[e.Y, e.X] < 4000) // channel simple line
                {
                    containInfo[0] = (int)itemType.channelLine;
                    containInfo[1] = surfaceMap[e.Y, e.X] - 3000;
                }
                else if (surfaceMap[e.Y, e.X] >= 4000 & surfaceMap[e.Y, e.X] < 5000) // channel label block
                {
                    containInfo[0] = (int)itemType.channelLabel;
                    containInfo[1] = surfaceMap[e.Y, e.X] - 4000;
                }
                else if (surfaceMap[e.Y, e.X] >= 5000 & surfaceMap[e.Y, e.X] < 6000) // protocol block
                {
                    containInfo[0] = (int)itemType.protocol;
                    containInfo[1] = surfaceMap[e.Y, e.X] - 5000;
                }
                else if (surfaceMap[e.Y, e.X] >= 6000 & surfaceMap[e.Y, e.X] < 7000) // segment block
                {
                    containInfo[0] = (int)itemType.segment;
                    containInfo[1] = surfaceMap[e.Y, e.X] - 6000;
                }
                else if (surfaceMap[e.Y, e.X] >= 7000 & surfaceMap[e.Y, e.X] < 8000) // station block
                {
                    containInfo[0] = (int)itemType.station;
                    containInfo[1] = surfaceMap[e.Y, e.X] - 7000;
                }
                else if (surfaceMap[e.Y, e.X] >= 8000) // linia
                {
                    containInfo[0] = (int)itemType.line;
                    containInfo[1] = surfaceMap[e.Y, e.X] - 8000;
                    switch (GraphGUIMainForm.line[containInfo[1]].IdentifyLineSection(e.X, e.Y))
                    {
                        case linePart.downSection:
                            containInfo[2] = (int)linePart.downSection;
                            break;
                        case linePart.middleSection:
                            containInfo[2] = (int)linePart.middleSection;
                            break;
                        case linePart.upperSection:
                            containInfo[2] = (int)linePart.upperSection;
                            break;
                    }
                }
            }
            return containInfo;
        }//ContainTest
    }
}
