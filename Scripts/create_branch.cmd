rem//  $LastChangedDate$
rem//  $Rev$
rem//  $LastChangedBy$
rem//  $URL$
rem//  $Id$
if "%1"=="" goto ERROR
set branchtype=%2
if "%branchtype%"=="" goto setbranch

:dothejob
svn mkdir svn://svnserver.hq.cas.com.pl/VS/%branchtype%/CommServer/%1  -m "created new CommServer tag  %1 (release folder)"
svn copy svn://svnserver.hq.cas.com.pl/VS/trunk/PR21-CommServer svn://svnserver.hq.cas.com.pl/VS/%branchtype%/CommServer/%1/PR21-CommServer -m "created new CommServer tag %1 (project PR21-CommServer)"
svn copy svn://svnserver.hq.cas.com.pl/VS/trunk/PR24-Biblioteka svn://svnserver.hq.cas.com.pl/VS/%branchtype%/CommServer/%1/PR24-Biblioteka -m "created new CommServer  tag %1 (project PR24-Biblioteka)"
svn copy svn://svnserver.hq.cas.com.pl/VS/trunk/PR30-OPCViever svn://svnserver.hq.cas.com.pl/VS/%branchtype%/CommServer/%1/PR30-OPCViever -m "created new CommServer tag %1 (project PR30-OPCViever)"
svn copy svn://svnserver.hq.cas.com.pl/VS/trunk/PR39-CommonResources svn://svnserver.hq.cas.com.pl/VS/%branchtype%/CommServer/%1/PR39-CommonResources -m "created new CommServer tag %1 (project PR39-CommonResources)"
svn copy svn://svnserver.hq.cas.com.pl/VS/trunk/ImageLibrary svn://svnserver.hq.cas.com.pl/VS/%branchtype%/CommServer/%1/ImageLibrary -m "created new CommServer tag %1 (project ImageLibrary)"
svn copy svn://svnserver.hq.cas.com.pl/VS/trunk/CommonBinaries svn://svnserver.hq.cas.com.pl/VS/%branchtype%/CommServer/%1/CommonBinaries -m "created new CommServer tag %1 (project CommonBinaries)"

goto EXIT

:setbranch
set branchtype=branches
goto dothejob
:ERROR
echo Parametr must be set
:EXIT
