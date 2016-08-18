//MouseUse.cs - zawiera metody obsługi myszy (edycji i wyświetlania informacji o elementach schematu)
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml.Schema;
namespace SchemaGenerator
{
    static class MouseUse
    {
        private static Point lastPoint; // przechowuje położenie wskaźnika myszy dla metod obsługujących edycję schematu
        private static int[] selectedBlock = new int[3]; // przechowuje rodzaj i nr indeksu przesówanego elementu
        private static int[] selectedDropBlock = new int[3]; // przechowuje rodzaj i nr indeksu elementu na którym zwolniono przycisk myszy
        //private int pom1; // pomocnicza
        /// <summary>
        /// Odpowiada za wyświetlenie informacji o klikanym bloku schematu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static public void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (Data.comunicationNet1.Channels.Rows.Count > 0) // zapobiega próbom wywołania przed wczytaniem bazy
            {
                int[] containInfo = new int[2];
                containInfo = Map.MapContainTest(e);
                if (containInfo[0] != 0) // jeśli containInfo[0]=0 to wskaznik myszy nie wskazuje żadnego elementu schematu
                {
                    Program.f1.webBrowser1.Document.OpenNew(false);
                    if (containInfo[0] == (int)itemType.server) // ikona servera
                    {
                        Array.Clear(GraphGUIMainForm.activeItem, 0, GraphGUIMainForm.activeItem.Length);
                        GraphGUIMainForm.activeItem[0, 0] = true;
                        // Informacja o pliku:
                        FileInfo fi = new FileInfo(GraphGUIMainForm.configFile);
                        string stronka = (string)GraphGUIMainForm.resources.GetObject("FileInfoTable");
                        // wypełniam odpowiednie pola informacjami
                        stronka = stronka.Replace("XXXparam1", fi.Name.ToString());
                        stronka = stronka.Replace("XXXparam2", fi.CreationTime.ToString());
                        stronka = stronka.Replace("XXXparam3", fi.Length.ToString() + " bytes");
                        stronka = stronka.Replace("XXXparam4", fi.Attributes.ToString());
                        Program.f1.webBrowser1.Document.Write(stronka);
                    }
                    else
                    {
                        bool flag = false;
                        XslCompiledTransform xstl = new XslCompiledTransform();
                        XPathDocument xpdoc = new XPathDocument(GraphGUIMainForm.configFile);
                        XsltArgumentList args = new XsltArgumentList();
                        StringWriter sw = new StringWriter();
                        TextReader tr;
                        XmlReader xmlr;
                        string xslFile = "";
                        Array.Clear(GraphGUIMainForm.activeItem, 0, GraphGUIMainForm.activeItem.Length);
                        if (containInfo[0] == (int)itemType.channelLabel)
                        {
                            GraphGUIMainForm.activeItem[3, containInfo[1]] = true;
                            xslFile = (string)GraphGUIMainForm.resources.GetObject("ChannelInfoTable");
                            args.AddParam("id", string.Empty, Data.comunicationNet1.Channels.Rows[containInfo[1]]["ChannelID"].ToString());
                            flag = true;
                        }
                        else if (containInfo[0] == (int)itemType.protocol)
                        {
                            GraphGUIMainForm.activeItem[4, containInfo[1]] = true;
                            xslFile = (string)GraphGUIMainForm.resources.GetObject("ProtocolInfoTable");
                            args.AddParam("id", string.Empty, Data.comunicationNet1.Protocol.Rows[containInfo[1]]["ProtocolID"].ToString());
                            flag = true;
                        }
                        else if (containInfo[0] == (int)itemType.segment)
                        {
                            GraphGUIMainForm.activeItem[5, containInfo[1]] = true;
                            xslFile = (string)GraphGUIMainForm.resources.GetObject("SegmentInfoTable");
                            args.AddParam("id", string.Empty, Data.comunicationNet1.Segments.Rows[containInfo[1]]["SegmentID"].ToString());
                            flag = true;
                        }
                        else if (containInfo[0] == (int)itemType.station)
                        {
                            GraphGUIMainForm.activeItem[6, containInfo[1]] = true;
                            xslFile = (string)GraphGUIMainForm.resources.GetObject("StationInfoTable");
                            args.AddParam("id", string.Empty, Data.comunicationNet1.Station.Rows[containInfo[1]]["StationID"].ToString());
                            flag = true;
                        }
                        else if (containInfo[0] == (int)itemType.line & GraphGUIMainForm.line[containInfo[1]].type == lineType.segm_stat) // linia segment-station
                        {
                            GraphGUIMainForm.activeItem[7, containInfo[1] - Data.comunicationNet1.Protocol.Rows.Count - Data.comunicationNet1.Segments.Rows.Count] = true;
                            xslFile = (string)GraphGUIMainForm.resources.GetObject("InterfaceInfoTable");
                            args.AddParam("id", string.Empty, Data.comunicationNet1.Interfaces.Rows[containInfo[1] - Data.comunicationNet1.Protocol.Rows.Count - Data.comunicationNet1.Segments.Rows.Count]["Name"].ToString());
                            flag = true;
                        }
                        if (flag)
                        {
                            tr = new StringReader(xslFile);
                            xmlr = new XmlTextReader(tr);
                            xstl.Load(xmlr);
                            xstl.Transform(xpdoc, args, sw);
                            string doc = sw.ToString();
                            Program.f1.webBrowser1.Document.Write(doc);
                        }
                    }
                    Program.f1.GenerateSchema(false); // odświeżenie widoku
                }
                else // czyści okienko informacyjne farmularza
                {
                    Array.Clear(GraphGUIMainForm.activeItem, 0, GraphGUIMainForm.activeItem.Length);
                    string blank = (string)GraphGUIMainForm.resources.GetObject("Blank");
                    Program.f1.webBrowser1.Document.Write(blank);
                    Program.f1.webBrowser1.Refresh();
                }
            } // if
        } // OnMouseClick
        /// <summary>
        /// Odpowiada za obsługę edycji schematu (przesówanie elementów myszką)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (Data.comunicationNet1.Channels.Rows.Count > 0) // zapobiega próbom wywołania przed wczytaniem bazy
            {
                selectedBlock = Map.MapContainTest(e);
                if (selectedBlock[0] != 0) // jeśli {0,0} to wskaznik myszy nie wskazuje żadnego elementu schematu
                {
                    lastPoint = new Point(e.X, e.Y);
                }
            }
        }
        /// <summary>
        /// Odpowiada za obsługę edycji schematu (przesówanie elementów myszką)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastPoint != Point.Empty)
            {
                int xOffset, yOffset;
                int pom1;
                xOffset = e.X - lastPoint.X;
                yOffset = e.Y - lastPoint.Y;
                if (selectedBlock[0] == (int)itemType.protocol) // protocol block move
                {
                    Program.f1.Cursor = Cursors.SizeAll;
                    GraphGUIMainForm.protocol[selectedBlock[1]].x = CheckMoveLimits(true, GraphGUIMainForm.protocol[selectedBlock[1]].x, xOffset);
                    pom1 = CheckMoveLimits(false, GraphGUIMainForm.protocol[selectedBlock[1]].y, yOffset);
                    foreach (Block blo in GraphGUIMainForm.protocol)
                    {
                        blo.y = pom1;
                    }
                }
                else if (selectedBlock[0] == (int)itemType.segment) // segment block move
                {
                    Program.f1.Cursor = Cursors.SizeAll;
                    GraphGUIMainForm.segment[selectedBlock[1]].x = CheckMoveLimits(true, GraphGUIMainForm.segment[selectedBlock[1]].x, xOffset);
                    pom1 = CheckMoveLimits(false, GraphGUIMainForm.segment[selectedBlock[1]].y, yOffset);
                    foreach (Block blo in GraphGUIMainForm.segment)
                    {
                        blo.y = pom1;
                    }
                }
                else if (selectedBlock[0] == (int)itemType.station) // station block move
                {
                    Program.f1.Cursor = Cursors.SizeWE;
                    GraphGUIMainForm.station[selectedBlock[1]].x = CheckMoveLimits(true, GraphGUIMainForm.station[selectedBlock[1]].x, xOffset);
                }
                else if (selectedBlock[0] == (int)itemType.line) // lines move
                {
                    selectedDropBlock = Map.MapContainTest(e);
                    int lineID = selectedBlock[1];
                    switch (GraphGUIMainForm.line[lineID].type)
                    {
                        case lineType.chan_prot:
                            if (selectedBlock[2] == (int)linePart.downSection) // odcinek 1 połączenia
                            {
                                if (selectedDropBlock[0] == (int)itemType.channelLine)
                                {
                                    Program.f1.Cursor = Cursors.PanNorth;
                                }
                                else
                                {
                                    Program.f1.Cursor = Cursors.SizeWE;
                                }
                                GraphGUIMainForm.line[lineID].offset1 = GraphGUIMainForm.channelLine[GraphGUIMainForm.line[lineID].id1].CheckLineLimits(GraphGUIMainForm.line[lineID].offset1, xOffset);
                            }
                            else if (selectedBlock[2] == (int)linePart.middleSection) // odcinek 2 (poziomy) połączenia
                            {
                                Program.f1.Cursor = Cursors.SizeNS;
                                GraphGUIMainForm.line[lineID].yzal = CheckMoveLimits(false, GraphGUIMainForm.line[lineID].yzal, yOffset);
                            }
                            else if (selectedBlock[2] == (int)linePart.upperSection) // odcinek 3 połączenia
                            {
                                Program.f1.Cursor = Cursors.SizeWE;
                                GraphGUIMainForm.line[lineID].offset2 = GraphGUIMainForm.protocol[GraphGUIMainForm.line[lineID].id2].CheckBlockLimits(GraphGUIMainForm.line[lineID].offset2, xOffset);
                            }
                            break;
                        case lineType.prot_segm:
                            if (selectedBlock[2] == (int)linePart.downSection) // odcinek 1 połączenia
                            {
                                if (selectedDropBlock[0] == (int)itemType.protocol)
                                {
                                    Program.f1.Cursor = Cursors.PanNorth;
                                }
                                else
                                {
                                    Program.f1.Cursor = Cursors.SizeWE;
                                }
                                GraphGUIMainForm.line[lineID].offset1 = GraphGUIMainForm.protocol[GraphGUIMainForm.line[lineID].id1].CheckBlockLimits(GraphGUIMainForm.line[lineID].offset1, xOffset);
                            }
                            else if (selectedBlock[2] == (int)linePart.middleSection) // odcinek 2 (poziomy) połączenia
                            {
                                Program.f1.Cursor = Cursors.SizeNS;
                                GraphGUIMainForm.line[lineID].yzal = CheckMoveLimits(false, GraphGUIMainForm.line[lineID].yzal, yOffset);
                            }
                            else if (selectedBlock[2] == (int)linePart.upperSection) // odcinek 3 połączenia
                            {
                                Program.f1.Cursor = Cursors.SizeWE;
                                GraphGUIMainForm.line[lineID].offset2 = GraphGUIMainForm.segment[GraphGUIMainForm.line[lineID].id2].CheckBlockLimits(GraphGUIMainForm.line[lineID].offset2, xOffset);
                            }
                            break;
                        case lineType.segm_stat:
                            if (selectedBlock[2] == (int)linePart.downSection) // odcinek 1 połączenia
                            {
                                if (selectedDropBlock[0] == (int)itemType.segment)
                                {
                                    Program.f1.Cursor = Cursors.PanNorth;
                                }
                                else
                                {
                                    Program.f1.Cursor = Cursors.SizeWE;
                                }
                                GraphGUIMainForm.line[lineID].offset1 = GraphGUIMainForm.segment[GraphGUIMainForm.line[lineID].id1].CheckBlockLimits(GraphGUIMainForm.line[lineID].offset1, xOffset);
                            }
                            else if (selectedBlock[2] == (int)linePart.middleSection) // odcinek 2 (poziomy) połączenia
                            {
                                Program.f1.Cursor = Cursors.SizeNS;
                                GraphGUIMainForm.line[lineID].yzal = CheckMoveLimits(false, GraphGUIMainForm.line[lineID].yzal, yOffset);
                            }
                            else if (selectedBlock[2] == (int)linePart.upperSection) // odcinek 3 połączenia
                            {
                                if (selectedDropBlock[0] == (int)itemType.station)
                                {
                                    Program.f1.Cursor = Cursors.PanSouth;
                                }
                                else
                                {
                                    Program.f1.Cursor = Cursors.SizeWE;
                                }
                                GraphGUIMainForm.line[lineID].offset2 = GraphGUIMainForm.station[GraphGUIMainForm.line[lineID].id1].CheckBlockLimits(GraphGUIMainForm.line[lineID].offset2, xOffset);
                            }
                            break;
                    }
                }
                //drtemp2.AcceptChanges();
                lastPoint = new Point(e.X, e.Y);
                Program.f1.GenerateSchema(false); // odświeżenie widoku
            }
        }
        /// <summary>
        /// Odpowiada za obsługę edycji schematu (przesówanie elementów myszką)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static public void OnMouseUp(object sender, MouseEventArgs e)
        {
            Program.f1.Cursor = Cursors.Default;
            DataRow drtemp1 = null;
            bool flag = false; // flaga zapobegająca wywołaniu DataRow.AcceptChanges() przed zmianami
            if (selectedBlock[0] == (int)itemType.line) // lines edit
            {
                selectedDropBlock = Map.MapContainTest(e);
                int idx = selectedBlock[1];
                int destinationID = selectedDropBlock[1];
                switch (GraphGUIMainForm.line[idx].type)
                {
                    case lineType.chan_prot:
                        if (selectedBlock[2] == (int)linePart.downSection) // odcinek 1 połączenia
                        {
                            if (selectedDropBlock[0] == (int)itemType.channelLine)
                            {
                                // aktualizacja bazy danych - zmiana punktu zaczepienia
                                drtemp1 = Data.comunicationNet1.Protocol.Rows[idx];
                                drtemp1["ChannelID"] = Data.comunicationNet1.Channels.Rows[destinationID]["ChannelID"];
                                // aktualizacja danych schematu
                                GraphGUIMainForm.line[idx].id1 = destinationID;
                                GraphGUIMainForm.line[idx].offset1 = e.X - GraphGUIMainForm.serverChannelSocket[0].X;
                                // baza danych zmieniona - włączam opcję aktualizacji bazy danych konfiguracji
                                Program.f1.updateCfgFileToolStripMenuItem.Enabled = true;
                                flag = true;
                            }
                        }
                        break;
                    case lineType.prot_segm:
                        if (selectedBlock[2] == (int)linePart.downSection) // odcinek 1 połączenia
                        {
                            if (selectedDropBlock[0] == (int)itemType.protocol)
                            {
                                // aktualizacja bazy danych - zmiana punktu zaczepienia
                                drtemp1 = Data.comunicationNet1.Segments.Rows[idx - Data.comunicationNet1.Channels.Rows.Count];
                                drtemp1["ProtocolID"] = Data.comunicationNet1.Protocol.Rows[destinationID]["ProtocolID"];
                                // aktualizacja danych schematu
                                GraphGUIMainForm.line[idx].id1 = destinationID;
                                GraphGUIMainForm.line[idx].offset1 = e.X - GraphGUIMainForm.protocol[destinationID].rec.Left;
                                // baza danych zmieniona - włączam opcję aktualizacji bazy danych konfiguracji
                                Program.f1.updateCfgFileToolStripMenuItem.Enabled = true;
                                flag = true;
                            }
                        }
                        break;
                    case lineType.segm_stat:
                        if (selectedBlock[2] == (int)linePart.downSection) // odcinek 1 połączenia
                        {
                            if (selectedDropBlock[0] == (int)itemType.segment)
                            {
                                // aktualizacja bazy danych - zmiana punktu zaczepienia
                                drtemp1 = Data.comunicationNet1.Interfaces.Rows[idx - Data.comunicationNet1.Protocol.Rows.Count - Data.comunicationNet1.Segments.Rows.Count];
                                drtemp1["SegmentID"] = Data.comunicationNet1.Segments.Rows[destinationID]["SegmentID"];
                                // aktualizacja danych schematu
                                GraphGUIMainForm.line[idx].id1 = destinationID;
                                GraphGUIMainForm.line[idx].offset1 = e.X - GraphGUIMainForm.segment[destinationID].rec.Left;
                                // baza danych zmieniona - włączam opcję aktualizacji bazy danych konfiguracji
                                Program.f1.updateCfgFileToolStripMenuItem.Enabled = true;
                                flag = true;
                            }
                        }
                        else if (selectedBlock[2] == (int)linePart.upperSection)// odcinek 3 połączenia
                        {
                            if (selectedDropBlock[0] == (int)itemType.station)
                            {
                                // aktualizacja bazy danych - zmiana punktu zaczepienia
                                drtemp1 = Data.comunicationNet1.Interfaces.Rows[idx - Data.comunicationNet1.Protocol.Rows.Count - Data.comunicationNet1.Segments.Rows.Count];
                                drtemp1["StationID"] = Data.comunicationNet1.Station.Rows[destinationID]["StationID"];
                                // aktualizacja danych schematu
                                GraphGUIMainForm.line[idx].id2 = destinationID;
                                GraphGUIMainForm.line[idx].offset2 = e.X - GraphGUIMainForm.station[destinationID].rec.Left;
                                // baza danych zmieniona - włączam opcję aktualizacji bazy danych konfiguracji
                                Program.f1.updateCfgFileToolStripMenuItem.Enabled = true;
                                flag = true;
                            }
                        }
                        break;
                }
                if (flag)
                {
                    drtemp1.AcceptChanges();
                }
            }
            lastPoint = Point.Empty;
            selectedBlock[0] = 0;
            selectedBlock[1] = 0;
            selectedBlock[2] = 0;
            Program.f1.GenerateSchema(false); // odświeżenie widoku
        }
        /// <summary>
        /// Funkcja sprawdzająca czy jakiś element schematu nie nachodzi na inny lub nie wyszedł poza powierzchnię rysowania
        /// </summary>
        /// <param name="OXtest">true = test na współrzędnych X, false = test na współrzędnych Y</param>
        /// <param name="value">edytowana wartość</param>
        /// <param name="offset">przesunięcie</param>
        /// <returns>nowa wartość</returns>
        static public int CheckMoveLimits(bool OXtest, int value, int offset)
        {
            value = value + offset;
            int min = 0, max = 0;
            if (OXtest)
            {
                switch (selectedBlock[0])
                {
                    case (int)itemType.protocol:
                        if (selectedBlock[1] > 0) min = GraphGUIMainForm.protocol[selectedBlock[1] - 1].rec.Right + 5;
                        else min = 5;
                        if (selectedBlock[1] < GraphGUIMainForm.protocol.Length - 1) max = GraphGUIMainForm.protocol[selectedBlock[1] + 1].rec.Left - GraphGUIMainForm.protocol[selectedBlock[1]].width - 5;
                        else max = GraphGUIMainForm.pW - GraphGUIMainForm.protocol[selectedBlock[1]].width - 5;
                        break;
                    case (int)itemType.segment:
                        if (selectedBlock[1] > 0) min = GraphGUIMainForm.segment[selectedBlock[1] - 1].rec.Right + 5;
                        else min = 5;
                        if (selectedBlock[1] < GraphGUIMainForm.segment.Length - 1) max = GraphGUIMainForm.segment[selectedBlock[1] + 1].rec.Left - GraphGUIMainForm.segment[selectedBlock[1]].width - 5;
                        else max = GraphGUIMainForm.pW - GraphGUIMainForm.segment[selectedBlock[1]].width - 5;
                        break;
                    case (int)itemType.station:
                        if (selectedBlock[1] > 0) min = GraphGUIMainForm.station[selectedBlock[1] - 1].rec.Right + 5;
                        else min = 5;
                        if (selectedBlock[1] < GraphGUIMainForm.station.Length - 1) max = GraphGUIMainForm.station[selectedBlock[1] + 1].rec.Left - GraphGUIMainForm.station[selectedBlock[1]].width - 5;
                        else max = GraphGUIMainForm.pW - GraphGUIMainForm.station[selectedBlock[1]].width - 5;
                        break;
                    default:
                        Console.WriteLine("CheckSurfaceLimits - Default case 1");
                        break;
                }
            }
            else
            {
                switch (selectedBlock[0])
                {
                    case (int)itemType.protocol:
                        max = GraphGUIMainForm.lineLimitation[0] - GraphGUIMainForm.protocol[selectedBlock[1]].height - 10;
                        min = GraphGUIMainForm.lineLimitation[1] + 10;
                        break;
                    case (int)itemType.segment:
                        max = GraphGUIMainForm.lineLimitation[2] - GraphGUIMainForm.segment[selectedBlock[1]].height - 10;
                        min = GraphGUIMainForm.lineLimitation[3] + 10;
                        break;
                    case (int)itemType.line:
                        if (GraphGUIMainForm.line[selectedBlock[1]].type == lineType.chan_prot) max = GraphGUIMainForm.server.rec.Top - 5;
                        else max = GraphGUIMainForm.line[selectedBlock[1]].y1 - 10;
                        min = GraphGUIMainForm.line[selectedBlock[1]].y2 + 10;
                        break;
                }
            }
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }
    }
}
