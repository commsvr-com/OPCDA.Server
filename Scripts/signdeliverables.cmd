rem//  $LastChangedDate$
rem//  $Rev$
rem//  $LastChangedBy$
rem//  $URL$
rem//  $Id$
"%net20sdk%\signtool" sign /a ..\Deliverables\CommserverSetup\Release\CommServerSetup.msi 
"%net20sdk%\signtool" sign /a ..\Deliverables\CommserverSetup\Release\Setup.exe
rem "%net20sdk%\signtool.exe" sign /n "CAS" /a "..\Deliverables\CommserverSetup\release\CommServerSetup_4_00_02.exe"