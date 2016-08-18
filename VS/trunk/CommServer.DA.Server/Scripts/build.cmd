rem//  $LastChangedDate$
rem//  $Rev$
rem//  $LastChangedBy$
rem//  $URL$
rem//  $Id$

set buildtype=%1
if "%buildtype%"=="" goto setbuildtype

:dothejob
cd ..
"%Windir%\Microsoft.NET\Framework\v3.5\msbuild"  .\CommServer.sln /t:build /p:Configuration=%buildtype%
cd .\Scripts
goto exit

:setbuildtype
set buildtype=Release
goto dothejob

:exit
rem call signdeliverables.cmd
