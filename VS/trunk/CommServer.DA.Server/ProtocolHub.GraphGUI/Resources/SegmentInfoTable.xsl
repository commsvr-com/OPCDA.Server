<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:doc="http://tempuri.org/ComunicationNet.xsd">

<xsl:param name="id"/>

  <xsl:template match="/doc:ComunicationNet">
    <html>
      <body bgcolor="LightSteelBlue" topmargin="2" bottommargin="2" leftmargin="2" rightmargin="2">
	
		    <table width="100%" border="1"  bordercolor="LightSteelBlue" style="background-color: white; font-size: 11px;" cellspacing="0">
			    <tr bordercolor="cornflowerblue">
				    <td width="145"> Nazwa segmentu </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:Name"/> </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> ID segmentu </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:SegmentID"/> </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Adres </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:Address"/> </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Okres skonowania </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:TimeScan"/> ms </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Podtrzymywanie połączenia </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:KeepConnect"/> </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Zdolność nawiązywania poł. </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:PickupConn"/> </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Czas transmisji </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:timeKeepConn"/> ms </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Czas do wznowienia poł. </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:TimeReconnect"/> ms </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Czas bezczynnego poł. </td>
				    <td align="center"> <xsl:value-of select="doc:Segments[doc:SegmentID=$id]/doc:TimeIdleKeepConn"/> ms </td>
			    </tr>
		    </table>
		
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
