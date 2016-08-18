<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:doc="http://tempuri.org/ComunicationNet.xsd">

<xsl:param name="id"/>

  <xsl:template match="/doc:ComunicationNet">
    <html>
      <body bgcolor="LightSteelBlue" topmargin="2" bottommargin="2" leftmargin="2" rightmargin="2">
	
		    <table width="100%" border="1"  bordercolor="LightSteelBlue" style="background-color: white; font-size: 11px;" cellspacing="0">
			    <tr bordercolor="cornflowerblue">
				    <td width="120"> Nazwa protokołu </td>
				    <td align="center"> <xsl:value-of select="doc:Protocol[doc:ProtocolID=$id]/doc:Name"/> </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> ID protokołu </td>
				    <td align="center"> <xsl:value-of select="doc:Protocol[doc:ProtocolID=$id]/doc:ProtocolID"/> </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Czas na odpowiedź </td>
				    <td align="center"> <xsl:value-of select="doc:Protocol[doc:ProtocolID=$id]/doc:ResponseTimeOut"/> ms </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Czas na kolejną ramkę </td>
				    <td align="center"> <xsl:value-of select="doc:Protocol[doc:ProtocolID=$id]/doc:FrameTimeOut"/> ms </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Czas na kolejny znak </td>
				    <td align="center"> <xsl:value-of select="doc:Protocol[doc:ProtocolID=$id]/doc:CharacterTimeOut"/> µs </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Liczba wznowień </td>
				    <td align="center"> <xsl:value-of select="doc:Protocol[doc:ProtocolID=$id]/doc:MaxNumberOfRetries"/> </td>
			    </tr>
			    <tr bordercolor="cornflowerblue">
				    <td> Protokół testowy </td>
				    <td align="center"> <xsl:value-of select="doc:Protocol[doc:ProtocolID=$id]/doc:ProtocolType"/> </td>
			    </tr>
		    </table>
		
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>