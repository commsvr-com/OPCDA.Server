dir ..\Deliverables\CommserverSetup\Release\
cd ..\Deliverables\CommserverSetup\Release\  
dir
msiexec /a "CommServerSetup.msi" /qb TARGETDIR="C:\VS\svn\svn.CommServer.DA\CommServer.DA.Server\CommServer.DA.Server\Deliverables\CommserverSetup\Release\CommServerSetup.content"

