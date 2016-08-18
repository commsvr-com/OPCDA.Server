//GraphGUIMainForm_engine.cs - zawiera całą funkcjonalność aplikacji
//Autor: Paweł Grącki
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Resources;
namespace SchemaGenerator
{
    public enum itemType : int // reprezentuje odcinek linii
    {
        nothing = 0,
        server = 1,
        scadaLan = 2,
        channelLine = 3,
        channelLabel = 4,
        protocol = 5,
        segment = 6,
        station = 7,
        line = 8
    };
    /// <summary>
    /// Klasa ta zawiera całą funkcjonalnośc aplikacji
    /// </summary>
    public partial class GraphGUIMainForm
    {
        // powierzchnia do rysowania (panel)
        private static Graphics g;
        // manager zasobów
        public static ResourceManager resources;
        // stałe zawierające adresy URL plików
        public static string configFile; // zawiera URL otwartego pliku konfiguracji
        public static string schemaFile = string.Empty; // zawiera URL otwartego pliku schematu
        // stałe przydatne w rysowaniu
        public static int pW; // szerokość panelu
        public static int pH; // wysokość panelu
        public static int iconSz;
        public static int halfIconSz;
        public static Size iconSize; // rozmiar bloku ikony
        public static Size iconPicSize; // rozmiar obrazka wewnątrz ikony (po odjęciu grubości ramki)
        public static Size serverSize; // rozmiar bloku servera
        public static Size serverPicSize; // rozmiar obrazka wewnątrz bloku servera (po odjęciu grubości ramki)
        public static int channelLineLenght; // przechowuje długośc linii kanału
        public static int protocolBlockWidth; // przechowuje szerokość bloku protokołu
        public static int segmentBlockWidth; // przechowuje szerokość bloku segmentu
        // ikonki
        public static Bitmap commServerPic;
        public static Bitmap stationPic;
        public static Bitmap scadaPic;
        // punkty połączeń
        public static Point[] serverChannelSocket; // punkty podłączenia kanałów do ikonki servera
        public static Point ethSock1, ethSock2; // punkty połączeniowe serwera do sieci LAN (OPC Conn.)
        // Figury zapamiętane na potrzeby testowania zawierania kursora w blokach schematu i liniach
        public static Icon server;
        public static Icon scadaLan;
        public static SimpleLine[] channelLine;
        public static Block[] channel;
        public static Block[] protocol;
        public static Block[] segment;
        public static Icon[] station;
        public static Line[] line;
        // Zmienne potrzebne do obsługi edycji schematu myszką
        public static int[] lineLimitation;
        // Wskazuje na figurę aktywną (TRUE) (wyświetlane są informacje o tym bloku) - tabl. ma wymiar [5,maximum]
        public static bool[,] activeItem;
        // przydatne zmienne
        private int x1, x2, y1, y2; // zmienne pomocne przy wyznaczaniu miejsca rysowania w zależności od rozmiarów okna i ikon
        private double l;
        private int pom1, pom2; // zmienne pomocnicze wykorzystywane do przechowywania wartości wyliczanych
        private string pomString; // pomocnicza znakowa (do wyswietlania etykiet)
        /// <summary>
        /// Funkcja initjalizuje statyczne tablice aplikacji na podstawie informacji o wczytanej konfiguracji
        /// </summary>
        private void PrepareStaticTables()
        {
            // punkty połączeń
            serverChannelSocket = new Point[Data.comunicationNet1.Channels.Rows.Count];
            // elementy na schemacie
            channelLine = new SimpleLine[Data.comunicationNet1.Channels.Rows.Count];
            channel = new Block[Data.comunicationNet1.Channels.Rows.Count];
            protocol = new Block[Data.comunicationNet1.Protocol.Rows.Count];
            segment = new Block[Data.comunicationNet1.Segments.Rows.Count];
            station = new Icon[Data.comunicationNet1.Station.Rows.Count];
            line = new Line[Data.comunicationNet1.Protocol.Rows.Count + Data.comunicationNet1.Segments.Rows.Count + Data.comunicationNet1.Interfaces.Rows.Count];
            // tablica informacji o elemencie "aktywnym"
            int maximum = Utils.Max(Data.comunicationNet1.Channels.Rows.Count, Data.comunicationNet1.Protocol.Rows.Count, Data.comunicationNet1.Segments.Rows.Count, Data.comunicationNet1.Station.Rows.Count, Data.comunicationNet1.Interfaces.Rows.Count);
            activeItem = new bool[8, maximum];
            Array.Clear(activeItem, 0, activeItem.Length);
            lineLimitation = new int[4];
        }
        /// <summary>
        /// Ustawia attrybuty schematu takie jak ikony, czcionki etykiet, zmienne opisujące rozmiar panelu itp.
        /// </summary>
        private void SetSchemaAttributes()
        {
            // ustawienie czcionek etykiet
            Configuration.labelFont = new Font("Courier New", Properties.Settings.Default.labelFontSize);
            Configuration.labelFont2 = new Font("Georgia", Properties.Settings.Default.labelFont2Size);
            Configuration.infoFont = new Font("Tahoma", Properties.Settings.Default.infoFontSize);
            // dostosowywanie rozmiarów ikon
            pW = schemaPanel.ClientRectangle.Width;
            pH = schemaPanel.ClientRectangle.Height;
            // tworzy nową mapę połączeń
            Map.surfaceMap = new int[pH, pW];
            Array.Clear(Map.surfaceMap, 0, Map.surfaceMap.Length);
            // ustawiam wielkości ikon i bloków
            iconSz = (int)Math.Floor((double)(pH / 12));
            halfIconSz = (int)Math.Floor((double)(iconSz / 2));
            iconSize = new Size(iconSz, iconSz);
            iconPicSize = new Size(iconSz - (int)Configuration.labelFrame.Width * 2, iconSz - (int)Configuration.labelFrame.Width * 2);
            serverSize = new Size((int)Math.Floor((double)(iconSz * 2.5)), (int)Math.Floor((double)(iconSz * 2.5)));
            serverPicSize = new Size(serverSize.Width - (int)Configuration.serverFrame.Width * 2, serverSize.Height - (int)Configuration.serverFrame.Width * 2);
            channelLineLenght = pW - 430 - 3 * iconSz; // długość linii kanału
            protocolBlockWidth = (int)Math.Floor((double)(pW / (Data.comunicationNet1.Protocol.Rows.Count + 2))); // szerokość ramki protokołu
            segmentBlockWidth = (int)Math.Floor((double)(pW / (Data.comunicationNet1.Segments.Rows.Count + 2))); // szerokość ramki segmentu
            // wczytuje ikony schematu
            commServerPic = new Bitmap(global::SchemaGenerator.Properties.Resources.server, serverPicSize);
            stationPic = new Bitmap(global::SchemaGenerator.Properties.Resources.stationTransparent, iconPicSize);
            scadaPic = new Bitmap(global::SchemaGenerator.Properties.Resources.scada, new Size(iconSz * 2 - (int)Configuration.iconFrame.Width * 2, iconSz - (int)Configuration.iconFrame.Width * 2));
        }
        /// <summary>
        /// Generuje schemat na podstawie pliku konfiguracji i zapamiętuje położenie bloków i połączeń
        /// </summary>
        /// <param name="generacja">true=obliczanie punktów położenia bloków i linii wg. algorytmu rozmieszczeń; false=wyświetla schemat na podstawie zapamiętanych informacji</param>
        public void GenerateSchema(bool generacja)
        {
            if (Data.comunicationNet1.Channels.Rows.Count > 0) // zapobiega próbom generacji schematu przed wczytaniem bazy
            {
                SetSchemaAttributes();
                // rozpoczynamy rysowanie
                g = schemaPanel.CreateGraphics();
                g.Clear(System.Drawing.SystemColors.ControlLight);// czyści powierzchnię zapobiegając nałożeniom schematów
                try
                {
                    // przydatne zmienne
                    Point act = new Point(); // punkt aktualnego położenia "pióra"
                    Rectangle rec = new Rectangle(); // odzwierciedla prostokąt obramowania etykiet i czcionek - wykorzystywany do analizy położenia kurrora myszy
                    // ..:: COMM Server ::...................................................................................
                    server = new Icon(20, pH - 4 * iconSz, serverSize.Height, serverSize.Width, commServerPic, blockType.server);
                    server.DrawBlock(g, activeItem[0,0]);
                    Map.WriteBlockOnMap(server, 0);
                    // obliczam współrzędne punktów dołączania
                    y1 = server.rec.Top;
                    y2 = server.rec.Bottom;
                    x1 = server.rec.Left;
                    x2 = server.rec.Right;
                    l = (x2 - x1) / 3;
                    l = Math.Floor(l);
                    // wyznaczam punkty połączeniowe servera
                    ethSock1 = new Point(x1 + (int)l, y2);
                    ethSock2 = ethSock1;
                    ethSock2.X = ethSock2.X + (int)l;
                    l = (y2 - y1) / (Data.comunicationNet1.Channels.Rows.Count + 1);
                    l = Math.Floor(l);
                    for (int i = 1; i <= Data.comunicationNet1.Channels.Rows.Count; i++)
                    {
                        serverChannelSocket[i - 1] = new Point(x2, y1 + (int)l * i);
                    }
                    // ..:: SCADA & ADMIN Stations ::........................................................................
                    PrintLAN(scadaPic);
                    // ..:: PROTOCOLS ::.....................................................................................
                    if (generacja)
                    {
                        l = pW / (Data.comunicationNet1.Protocol.Rows.Count + 2);
                        l = Math.Floor(l);
                        act.Y = (int)Math.Floor((double)(pH / 2)); // odległośc od góry okna = 1/2 powierzchni rysowania
                        l = l / (Data.comunicationNet1.Protocol.Rows.Count + 1);
                        l = 2 * Math.Floor(l); // odstępy pomiedzy ramkami
                    }
                    for (int i = 0; i < Data.comunicationNet1.Protocol.Rows.Count; i++)
                    {
                        if (generacja)
                        {
                            act.X = (int)l * (i + 1) + protocolBlockWidth * i;
                            protocol[i] = new Block(act.X, act.Y, iconSz, protocolBlockWidth, Data.comunicationNet1.Protocol.Rows[i]["Name"].ToString(), blockType.protocol);
                        }
                        protocol[i].DrawBlock(g, activeItem[4,i]);
                        Map.WriteBlockOnMap(protocol[i], i);
                    }
                    // ..:: CHANNELS ::......................................................................................
                    for (int i = 0; i < Data.comunicationNet1.Channels.Rows.Count; i++)
                    {
                        pomString = Data.comunicationNet1.Channels.Rows[i]["Name"].ToString();
                        channelLine[i] = new SimpleLine(serverChannelSocket[i].X, serverChannelSocket[i].Y, serverChannelSocket[i].X + channelLineLenght, serverChannelSocket[i].Y, Configuration.channelPen);
                        channelLine[i].DrawLine(g);
                        channel[i] = new Block(serverChannelSocket[i].X + channelLineLenght + 5, serverChannelSocket[i].Y - 8, 17, pomString.Length * 9, pomString, blockType.channel);
                        channel[i].DrawBlock(g, activeItem[3,i]);
                        Map.WriteBlockOnMap(channel[i], i);
                    }
                    // ..:: SEGMENTS ::......................................................................................
                    if (generacja)
                    {
                        l = pW / (Data.comunicationNet1.Segments.Rows.Count + 2);
                        l = Math.Floor(l);
                        act.Y = (int)Math.Floor((double)(pH / 3)) + 5; // odległośc od góry okna = 1/3 powierzchni rysowania + 5 by kanały nie wchodziły na webBrowser'a
                        l = l / (Data.comunicationNet1.Segments.Rows.Count + 1);
                        l = 2 * Math.Floor(l); // odstępy pomiedzy ramkami
                    }
                    for (int i = 0; i < Data.comunicationNet1.Segments.Rows.Count; i++)
                    {
                        if (generacja)
                        {
                            act.X = (int)l * (i + 1) + segmentBlockWidth * i;
                            segment[i] = new Block(act.X, act.Y, iconSz, segmentBlockWidth, Data.comunicationNet1.Segments.Rows[i]["Name"].ToString(), blockType.segment);
                        }
                        segment[i].DrawBlock(g, activeItem[5,i]);
                        Map.WriteBlockOnMap(segment[i], i);
                    }
                    // ..:: STATIONS ::......................................................................................
                    if (generacja)
                    {
                        act.Y = 20;
                        rec.Size = new Size(iconSz + (int)Configuration.iconFrame.Width * 2, iconSz + (int)Configuration.iconFrame.Width * 2);
                        act.X = 0 - halfIconSz;
                        l = pW / (Data.comunicationNet1.Station.Rows.Count + 1); // odległość między lewymi górnymi narożnikami ikonek stacji
                    }
                    for (int i = 0; i < Data.comunicationNet1.Station.Rows.Count; i++)
                    {
                        if (generacja)
                        {
                            act.X = act.X + (int)l;
                            station[i] = new Icon(act.X, act.Y, iconSz, iconSz, stationPic, blockType.station);
                        }
                        Configuration.iconFrame.Color = Configuration.stationColors[i % 6];
                        station[i].DrawBlock(g, activeItem[6,i]);
                        Map.WriteBlockOnMap(station[i], i);
                        g.DrawString(Data.comunicationNet1.Station.Rows[i]["Name"].ToString(), Configuration.infoFont, Brushes.Blue, station[i].x, station[i].y - 20);
                    }
                    if (generacja)
                    {
                        GenerateLines(); // wymusza generowanie połączeń od podstaw
                    }
                    ConnectLines(); // rysuje połączenia
                    for (int i = 0; i < Data.comunicationNet1.Channels.Rows.Count; i++)
                    {
                        Map.WriteLineOnMap(channelLine[i], i); // kanały zaznaczamy na mapie na końcu aby nie przesłoniły ich linie
                    }
                }
                finally
                {
                    g.Dispose();
                }
            }
        }
        /// <summary>
        /// Funkcja wyznaczająca przebieg linii łączących na podstawie wewnętrzego algorytmu
        /// </summary>
        private void GenerateLines()
        {
            int[] protSockNumber = new int[Data.comunicationNet1.Protocol.Rows.Count]; // zawiera mnożniki przesunięcia X w ramach jednego bloku
            int[] statSockNumber = new int[Data.comunicationNet1.Station.Rows.Count]; // zawiera mnożniki przesunięcia X w ramach jednego bloku
            int[] segmSockNumber = new int[Data.comunicationNet1.Segments.Rows.Count]; // zawiera mnożniki przesunięcia X w ramach jednego bloku
            int j, k; // zmienna pomocnicza - licznik
            int l2 = 0, l3 = 0; // odstępy pomiędzy liniami
            // linie channel-protocol
            j = 0;
            for (int i = 0; i < Data.comunicationNet1.Channels.Rows.Count; i++)
            {
                foreach (DataRow dr in Data.comunicationNet1.Channels.Rows[i].GetChildRows("FK_Channels_Protocol"))
                {
                    // oblicza i zapamiętuje informacje kluczowe połączenia
                    pom2 = Data.comunicationNet1.Protocol.Rows.IndexOf(dr); // wyłuskuje nr indeksu krotki "dr" w ramach tabeli protocols
                    l = Math.Floor((double)(channelLineLenght / (Data.comunicationNet1.Protocol.Rows.Count + 1))); // odstępy (x) pomiedzy węzłami kanałów
                    l2 = (int)(Math.Floor((double)(pH - protocol[pom2].rec.Bottom - 4 * iconSz - 10) / Data.comunicationNet1.Protocol.Rows.Count)); // różnica między poziomami załamań linii
                    l3 = (int)(Math.Floor((double)protocolBlockWidth / (dr.GetParentRows("FK_Channels_Protocol").Length + 1))); // odstępy między socketami w ramach bloku protokołu
                    y1 = protocol[pom2].rec.Bottom + (j + 1) * l2; // współrzędna Y załamaina każdej z linii musi być inna
                    line[j] = new Line(i, (int)(l * (j + 1)), y1, pom2, l3, Configuration.channelPen, tip.reddot, tip.arrow, lineType.chan_prot);
                    j++;
                }
            }
            // linie protocol-segment
            // resetuję mnozniki odległości socketów
            for (int i = 0; i < Data.comunicationNet1.Protocol.Rows.Count; i++)
            {
                protSockNumber[i] = 1;
            }
            for (int i = 0; i < Data.comunicationNet1.Segments.Rows.Count; i++)
            {
                k = 1;
                foreach (DataRow dr in Data.comunicationNet1.Segments.Rows[i].GetParentRows("FK_Protocol_Segments"))
                {
                    // oblicza i zapamiętuje informacje kluczowe połączenia
                    pom1 = Data.comunicationNet1.Protocol.Rows.IndexOf(dr); // wyłuskuje nr indeksu krotki "dr" w ramach tabeli protocols
                    l = (int)Math.Floor((double)(protocol[pom1].rec.Right - protocol[pom1].rec.Left) / (Data.comunicationNet1.Protocol.Rows[pom1].GetChildRows("FK_Protocol_Segments").Length + 1));
                    l2 = (int)Math.Floor(((double)protocol[pom1].rec.Top - segment[i].rec.Bottom - 20) / (Data.comunicationNet1.Segments.Rows.Count + 1)); // odstępy pomiędzy poziomami linii
                    l3 = (int)(Math.Floor((double)segmentBlockWidth / (Data.comunicationNet1.Segments.Rows[i].GetParentRows("FK_Protocol_Segments").Length + 1))); // odstępy między socketami w ramach bloku segmentu
                    y1 = segment[i].rec.Bottom + (i + 1) * l2 + 5;// współrzędna Y załamaina każdej z linii musi być inna
                    line[j] = new Line(pom1, (int)(protSockNumber[pom1] * l), y1, i, k * l3, Configuration.protocolPen, tip.redarrow, tip.arrow, lineType.prot_segm);
                    protSockNumber[pom1]++; // mnożnik odległości między punktami połaczeniowymi jednego bloku;
                    k++;
                    j++;
                }
            }
            // linie segment-station
            // resetuję mnozniki odległości socketów
            for (int i = 0; i < Data.comunicationNet1.Station.Rows.Count; i++)
            {
                statSockNumber[i] = 1;
            }
            for (int i = 0; i < Data.comunicationNet1.Segments.Rows.Count; i++)
            {
                segmSockNumber[i] = 1;
            }
            k = 0;
            foreach (DataRow dr in Data.comunicationNet1.Interfaces.Rows)
            {
                // oblicza i zapamiętuje informacje kluczowe połączenia
                pom1 = Data.comunicationNet1.Segments.Rows.IndexOf(dr.GetParentRow("FK_Segments_Interfaces")); // wyłuskuje nr indeksu krotki "dr" w ramach tabeli segments
                pom2 = Data.comunicationNet1.Station.Rows.IndexOf(dr.GetParentRow("FK_Station_Interfaces")); // wyłuskuje nr indeksu krotki "dr" w ramach tabeli stations
                l = (int)Math.Floor((double)(segment[pom1].rec.Right - segment[pom1].rec.Left) / (Data.comunicationNet1.Segments.Rows[pom1].GetChildRows("FK_Segments_Interfaces").Length + 1));
                l2 = (int)Math.Floor((double)(segment[pom1].rec.Top - station[pom2].rec.Bottom - 20) / (Data.comunicationNet1.Interfaces.Rows.Count + 1)); // odstępy pomiędzy poziomami linii
                l3 = (int)Math.Floor((double)(iconSz + 2 * (int)Configuration.iconFrame.Width) / (Data.comunicationNet1.Station.Rows[pom2].GetChildRows("FK_Station_Interfaces").Length + 1));
                y1 = station[pom2].rec.Bottom + (k + 1) * l2 + 5;// współrzędna Y załamaina każdej z linii musi być inna
                line[j] = new Line(pom1, (int)(segmSockNumber[pom1] * l), y1, pom2, (int)(statSockNumber[pom2] * l3), Configuration.stationPen, tip.redarrow, tip.redarrow, lineType.segm_stat);
                segmSockNumber[pom1]++; // mnożnik odległości między punktami połaczeniowymi jednego bloku
                statSockNumber[pom2]++; // mnożnik odległości między punktami połaczeniowymi jednego bloku
                k++;
                j++;
            }
        }
        /// <summary>
        /// Funkcja rysująca rodziny połączeń na podstawie schemaDataBase
        /// </summary>
        private void ConnectLines()
        {
            int j; // zmienna pomocnicza - licznik
            int k; // pomocnicza
            // linie channel-protocol
            j = 0;
            for (int i = 0; i < Data.comunicationNet1.Channels.Rows.Count; i++)
            {
                foreach (DataRow dr in Data.comunicationNet1.Channels.Rows[i].GetChildRows("FK_Channels_Protocol"))
                {
                    line[j].DrawLine(g, false);
                    Map.WriteLineOnMap(line[j], j);
                    j++;
                }
            }
            // linie protocol-segment
            for (int i = 0; i < Data.comunicationNet1.Segments.Rows.Count; i++)
            {
                foreach (DataRow dr in Data.comunicationNet1.Segments.Rows[i].GetParentRows("FK_Protocol_Segments"))
                {
                    line[j].DrawLine(g, false);
                    Map.WriteLineOnMap(line[j], j);
                    j++;
                }
            }
            // linie segment-station
            k = 0;
            foreach (DataRow dr in Data.comunicationNet1.Interfaces.Rows)
            {
                Configuration.stationPen.Color = Configuration.stationColors[line[j].id2 % 6];
                line[j].DrawLine(g, activeItem[7,k]);
                Map.WriteLineOnMap(line[j], j);
                j++;
                k++;
            }
            lineLimitation[0] = pH;
            lineLimitation[1] = 0;
            lineLimitation[2] = pH;
            lineLimitation[3] = 0;
            foreach (Line lin in line)
            {
                if ((lin.type == lineType.chan_prot) & lin.yzal < lineLimitation[0]) lineLimitation[0] = lin.yzal;
                if ((lin.type == lineType.prot_segm) & lin.yzal > lineLimitation[1]) lineLimitation[1] = lin.yzal;
                if ((lin.type == lineType.prot_segm) & lin.yzal < lineLimitation[2]) lineLimitation[2] = lin.yzal;
                if ((lin.type == lineType.segm_stat) & lin.yzal > lineLimitation[3]) lineLimitation[3] = lin.yzal;
            }
        }
        /// <summary>
        /// Rysuje sieć OPC Connectivity wraz z etykietą
        /// </summary>
        /// <param name="scadaPic">obrazek sieci</param>
        private void PrintLAN(Bitmap scadaPic)
        {
            Configuration.iconFrame.Color = Color.LightBlue;
            scadaLan = new Icon(5 * iconSz, pH - iconSz - 13, iconSz, 2 * iconSz, scadaPic, blockType.lan);
            scadaLan.DrawBlock(g, false);
            Map.WriteBlockOnMap(scadaLan, 0);
            // obliczam współrzędne punktów połączenia stacji do serwera
            y1 = scadaLan.rec.Top;
            y2 = scadaLan.rec.Bottom;
            x1 = scadaLan.rec.Left;
            l = (y2 - y1) / 2;
            l = Math.Floor(l);
            Line scadaLine = new Line(x1, y1 + (int)l, y1 + (int)l, ethSock1.X, ethSock1.Y, Configuration.ethernetPen, tip.none, tip.arrow);
            scadaLine.DrawLine(g, false);
            g.DrawString("OPC Connectivity", Configuration.infoFont, Brushes.Blue, ethSock1.X + 10, y1 + (int)l - 20);
        }
    }
}
