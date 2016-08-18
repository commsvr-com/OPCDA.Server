//Data.cs - zawiera obsługę lokalnych bazy danych (DataSet)
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.Data;
namespace SchemaGenerator
{
    static class Data
    {
        public static ComunicationNet comunicationNet1 = new ComunicationNet();
        public static SchemaDataSet schemaDataSet1 = new SchemaDataSet();
        /// <summary>
        /// Funkcja inicjalizująca obiekty DataSet
        /// </summary>
        public static void PrepareDataSets()
        {
            ((System.ComponentModel.ISupportInitialize)(comunicationNet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(schemaDataSet1)).BeginInit();
            comunicationNet1.DataSetName = "ComunicationNet";
            comunicationNet1.Locale = new System.Globalization.CultureInfo("en-US");
            comunicationNet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            schemaDataSet1.DataSetName = "SchemaDataSet";
            schemaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            ((System.ComponentModel.ISupportInitialize)(comunicationNet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(schemaDataSet1)).EndInit();
        }
        /// <summary>
        /// Funkcja wczytująca dane z pliku konfiguracyjnego do lokalnej bazy danych (dataSet)
        /// </summary>
        /// <param name="configFile">url pliku konfiguracyjnego w formacie XML</param>
        public static void LoadConfigFile(string configFile)
        {
            // Czyszczę obiekt dataSet i wczytuję do niego plik konfiguracyjny
            comunicationNet1.Clear();
            comunicationNet1.ReadXml(configFile, XmlReadMode.IgnoreSchema);
        }
        /// <summary>
        /// Funkcja wczytująca dane z pliku schematu do lokalnej bazy danych (dataSet)
        /// </summary>
        /// <param name="configFile">url pliku konfiguracyjnego w formacie XML</param>
        public static void LoadSchemaFile(string schemaFile)
        {
            // Wczytuję dane schematu do obiektu DataSet
            schemaDataSet1.Clear();
            schemaDataSet1.ReadXml(schemaFile, XmlReadMode.IgnoreSchema);
            GraphGUIMainForm.configFile = schemaDataSet1.configFileName.Rows[0]["name"].ToString();
            LoadConfigFile(GraphGUIMainForm.configFile);
        }
        /// <summary>
        /// Funkcja wczytująca dane o położeniu elemantów do odpowiednich tablic
        /// </summary>
        public static void LoadSchemaData()
        {
            for (int i = 0; i < comunicationNet1.Protocol.Rows.Count; i++)
            {
                int x = int.Parse(schemaDataSet1.protocolUnitPoint.Rows[i]["x"].ToString());
                int y = int.Parse(schemaDataSet1.protocolUnitPoint.Rows[i]["y"].ToString());
                GraphGUIMainForm.protocol[i] = new Block(x, y, GraphGUIMainForm.iconSz, GraphGUIMainForm.protocolBlockWidth, comunicationNet1.Protocol.Rows[i]["Name"].ToString(), blockType.protocol);
            }
            for (int i = 0; i < comunicationNet1.Segments.Rows.Count; i++)
            {
                int x = int.Parse(schemaDataSet1.segmentUnitPoint.Rows[i]["x"].ToString());
                int y = int.Parse(schemaDataSet1.segmentUnitPoint.Rows[i]["y"].ToString());
                GraphGUIMainForm.segment[i] = new Block(x, y, GraphGUIMainForm.iconSz, GraphGUIMainForm.segmentBlockWidth, comunicationNet1.Segments.Rows[i]["Name"].ToString(), blockType.segment);
            }
            for (int i = 0; i < comunicationNet1.Station.Rows.Count; i++)
            {
                int x = int.Parse(schemaDataSet1.stationUnitPoint.Rows[i]["x"].ToString());
                int y = int.Parse(schemaDataSet1.stationUnitPoint.Rows[i]["y"].ToString());
                GraphGUIMainForm.station[i] = new Icon(x, y, GraphGUIMainForm.iconSz, GraphGUIMainForm.iconSz, GraphGUIMainForm.stationPic, blockType.station);
            }
            int j = 0;
            for (int i = 0; i < comunicationNet1.Protocol.Rows.Count; i++)
            {
                int id1 = int.Parse(schemaDataSet1.channelProtocolLines.Rows[i]["Id1"].ToString());
                int offset1 = int.Parse(schemaDataSet1.channelProtocolLines.Rows[i]["No1"].ToString());
                int yzal = int.Parse(schemaDataSet1.channelProtocolLines.Rows[i]["y"].ToString());
                int id2 = int.Parse(schemaDataSet1.channelProtocolLines.Rows[i]["Id2"].ToString());
                int offset2 = int.Parse(schemaDataSet1.channelProtocolLines.Rows[i]["No2"].ToString());
                GraphGUIMainForm.line[j] = new Line(id1, offset1, yzal, id2, offset2, Configuration.channelPen, tip.reddot, tip.arrow, lineType.chan_prot);
                j++;
            }
            for (int i = 0; i < comunicationNet1.Segments.Rows.Count; i++)
            {
                int id1 = int.Parse(schemaDataSet1.protocolSegmentLines.Rows[i]["Id1"].ToString());
                int offset1 = int.Parse(schemaDataSet1.protocolSegmentLines.Rows[i]["No1"].ToString());
                int yzal = int.Parse(schemaDataSet1.protocolSegmentLines.Rows[i]["y"].ToString());
                int id2 = int.Parse(schemaDataSet1.protocolSegmentLines.Rows[i]["Id2"].ToString());
                int offset2 = int.Parse(schemaDataSet1.protocolSegmentLines.Rows[i]["No2"].ToString());
                GraphGUIMainForm.line[j] = new Line(id1, offset1, yzal, id2, offset2, Configuration.protocolPen, tip.redarrow, tip.arrow, lineType.prot_segm);
                j++;
            }
            for (int i = 0; i < comunicationNet1.Interfaces.Rows.Count; i++)
            {
                int id1 = int.Parse(schemaDataSet1.segmentStationLines.Rows[i]["Id1"].ToString());
                int offset1 = int.Parse(schemaDataSet1.segmentStationLines.Rows[i]["No1"].ToString());
                int yzal = int.Parse(schemaDataSet1.segmentStationLines.Rows[i]["y"].ToString());
                int id2 = int.Parse(schemaDataSet1.segmentStationLines.Rows[i]["Id2"].ToString());
                int offset2 = int.Parse(schemaDataSet1.segmentStationLines.Rows[i]["No2"].ToString());
                GraphGUIMainForm.line[j] = new Line(id1, offset1, yzal, id2, offset2, Configuration.stationPen, tip.redarrow, tip.redarrow, lineType.segm_stat);
                j++;
            }
        }
        /// <summary>
        /// Funkcja wczytująca dane z pliku schematu do lokalnej bazy danych (dataSet)
        /// </summary>
        /// <param name="configFile">url pliku konfiguracyjnego w formacie XML</param>
        public static void SaveSchemaFile(string url)
        {
            schemaDataSet1.Clear();
            // zapamiętuje rozmiar okna w configDataBase
            schemaDataSet1.configFileName.AddconfigFileNameRow(GraphGUIMainForm.configFile);
            // zapamiętuje rozmiar okna w configDataBase
            schemaDataSet1.panelSize.AddpanelSizeRow(GraphGUIMainForm.pH, GraphGUIMainForm.pW);
            // dane o położeniu elementów (bloków i ikon)
            foreach (Block blo in GraphGUIMainForm.protocol)
            {
                schemaDataSet1.protocolUnitPoint.AddprotocolUnitPointRow(blo.x, blo.y);
            }
            foreach (Block blo in GraphGUIMainForm.segment)
            {
                schemaDataSet1.segmentUnitPoint.AddsegmentUnitPointRow(blo.x, blo.y);
            }
            foreach (Icon ico in GraphGUIMainForm.station)
            {
                schemaDataSet1.stationUnitPoint.AddstationUnitPointRow(ico.x, ico.y);
            }
            // dane o położeniu linii
            foreach (Line lin in GraphGUIMainForm.line)
            {
                switch (lin.type)
                {
                    case lineType.chan_prot:
                        schemaDataSet1.channelProtocolLines.AddchannelProtocolLinesRow(lin.id1, lin.offset1, lin.yzal, lin.id2, lin.offset2);
                        break;
                    case lineType.prot_segm:
                        schemaDataSet1.protocolSegmentLines.AddprotocolSegmentLinesRow(lin.id1, lin.offset1, lin.yzal, lin.id2, lin.offset2);
                        break;
                    case lineType.segm_stat:
                        schemaDataSet1.segmentStationLines.AddsegmentStationLinesRow(lin.id1, lin.offset1, lin.yzal, lin.id2, lin.offset2);
                        break;
                }
            }
            schemaDataSet1.AcceptChanges();
            schemaDataSet1.WriteXml(url);
        }
    }
}