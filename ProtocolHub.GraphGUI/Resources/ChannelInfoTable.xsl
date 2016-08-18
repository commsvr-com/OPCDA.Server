<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:doc="http://tempuri.org/ComunicationNet.xsd">

<xsl:param name="id"/>

  <xsl:template match="/doc:ComunicationNet">
    <html>
      <body bgcolor="LightSteelBlue" topmargin="2" bottommargin="2" leftmargin="2" rightmargin="2">

        <table width="100%" border="1"  bordercolor="LightSteelBlue" style="background-color: white; font-size: 11px;" cellspacing="0">
          <tr bordercolor="cornflowerblue">
            <td width="80"> Nazwa kanału </td>
            <td align="center"><xsl:value-of select="doc:Channels[doc:ChannelID=$id]/doc:Name"/></td>
          </tr>
          <tr bordercolor="cornflowerblue">
            <td> ID kanału </td>
            <td align="center"><xsl:value-of select="doc:Channels[doc:ChannelID=$id]/doc:ChannelID"/></td>
          </tr>
        </table>

      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>