//<summary>
//  Title   : CommServerMonitor Assembly info
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CAS;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle( "CAS.CommServerMonitor" )]
[assembly: AssemblyDescription( CommServerAssemblyVersionInfo.DescriptionAdd + "CommServer Performance Monitor" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "CAS" )]
[assembly: AssemblyProduct( "CommServer" )]
[assembly: AssemblyCopyright( "Copyright (C)2007, CAS LODZ POLAND." )]
[assembly: AssemblyTrademark( "CommServer" )]
[assembly: AssemblyCulture( "" )]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible( false )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid( "f185cedf-d0b6-4762-9d27-385764dff679" )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion( CommServerAssemblyVersionInfo.CurrentVersion )]
[assembly: AssemblyFileVersionAttribute( CommServerAssemblyVersionInfo.CurrentFileVersion )]
//  Historia:
//  _______________________________________________________________________________________________________
//  Date:       dd-mm-2005
//  Plik:       PR21-CommServer,  PR24-Biblioteka
//  Wersja:    
//  
//  Zmiany:
//    - dodano klucze do konfiguracji - brak poprawnego klucza uruchamia wersje demo 
//    - dodano podpisy
//    
//  Problemy:
//-.  Uzupełnic komentarze w AssembnlyInfo dla Plugin'ow
//-.  Sprawdzić jak są zaiplementowane kultury
//-.  CAS.OpcSvr.Da.NETServer.DeviceItem.Read zaiplementować bezpośrednie czytanie z urządzenia
//-.  BaseStation.DataQueue.DataDescription.InvalidateTag - nie zmiwenia jakości dla BaseStation.DataQueue.DataDescription.TagDataDescription.TagBit
//-.  BaseStation.DataQueue:  brak invalidacji tagow bitowych, po timeout dla tagu jrgo tagi bitowe tez powinny miec quality bad
//-.	zapis całych bloków (przy wpisywaniu przez OPC całej grupy).
//-.	monitorowanie stanu systemu i udostępnienie w danych w OPC;
//-.	sprawdzić odświeżanie stanu ikon – np. dla stacji jeżeli nie mają interfejsu o numerze 1 to zawsze będą pokazywane jako nie połączone
//-.	manual CHM
//-.	instalator
//-.	sprawdzić CAPI, Zaimplementować odbieranie telefonów w CAPI 
//-.	zamienić aktualną DLL na OPC NET API
//-.	compilance test
//-.	pluginy dla warstwy CommunicationLayer
//-.	w przypadku czytania z device lub pisania należy podjąć natychmiastową próbę połączenia, jęsli aktualnie wszystkie interfejsy są wyłączone z powodu błędu.
//-.	umożliwić ręczne włączanie i iwyłączanie interfejsów - przy włączaniu należy sprawdzić czy interfejs jest sprawny.
//-.	przy próbie ponownego połaczenia po błędzie należy zmniejszyć liczbę powtórzeń do pierwszego sukcesu.
//-.	zrealizować sliding window w protokołach SBUSi MBUS (patrz; opis w \\Hq.cas.com.pl\cas_dfs\Public\FTP\matrikon\matrikon_opc_server_for_modbus_user_manual.pdf  w Noise Tolerance
//-.	Commserver jako serwis i zarządzany przez WMI lub  OPC – podzielić Commserver na część zarządzającą i zarządzaną
//-.	zapis/odczyt asynchroniczny do portu RS.
//  _______________________________________________________________________________________________________
//  Date:       03-06-2005
//  Plik:       PR21-CommServer,  PR24-Biblioteka
//  Wersja:    
//  
//  Zmiany:
//    - dodano main menu 
//    - dodano raportowanie w HTML z obrazkiem
//    - dodano networkconfig do projektu
//    - dodano pytanie o zakonczenie aplikacji
//    - dodano podlaczanie sie i rozlaczanie sie od portu COM za kazdym razem i czyszczenie portu - gdy ten powinien byc nieaktywny
//    - dodano obsluge pluginow dla warstwy aplikacyjnej 
//    - zmieniono ze przelaczenie szybkie - wolne skanowanie nastepuje tylko przy przejsciu 0 -> 1 (zbocze narastajace))
//    - zmieniono network config - tak by był bardziej przyjazny dla uzytkownika
//    - dodano klucze do konfiguracji - brak poprawnego klucza uruchamia wersje demo 
//    - dodano podpisy
//    
//  Problemy:
//-.	zapis całych bloków (przy wpisywaniu przez OPC całej grupy).
//-.	monitorowanie stanu systemu i udostępnienie w danych w OPC;
//-.	występują błędy przy przełączaniu segmentu (może nie jest czyszczony bufor). Pojawiają się błędy Invalid Frames. Informacja typu CR_Invalid może pojawiać się:
//  a.	w sekwencji DLE nie pojawiło się 1 lub 0
//  b.	nieznany atrybut operacji
//  c.	podczas odczytywaniu telegramu (dot. Tylko części slave)
//  d.	jeśli odbierana ramka jest inna – niż taka na którą czekamy
//-.	sprawdzić odświeżanie stanu ikon – np. dla stacji jeżeli nie mają interfejsu o numerze 1 to zawsze będą pokazywane jako nie połączone
//-.	manual CHM
//-.	instalator
//-.	sprawdzić CAPI, Zaimplementować odbieranie telefonów w CAPI 
//-.	wyczyścić i sprawdzić SBUS – slave
//-.	zamienić aktualną DLL na OPC NET API
//-.	compilance test
//-.	uruchomić pluginy (uruchomiono dla aplicationLayer, oprocz symulatorow sieci)
//-.	w przypadku czytania z device lub pisania należy podjąć natychmiastową próbę połączenia, jęsli aktualnie wszystkie interfejsy są wyłączone z powodu błędu.
//-.	umożliwić ręczne włączanie i iwyłączanie interfejsów - przy włączaniu należy sprawdzić czy interfejs jest sprawny.
//-.	przy próbie ponownego połaczenia po błędzie należy zmniejszyć liczbę powtórzeń do pierwszego sukcesu.
//-.	zrealizować sliding window w protokołach SBUSi MBUS (patrz; opis w \\Hq.cas.com.pl\cas_dfs\Public\FTP\matrikon\matrikon_opc_server_for_modbus_user_manual.pdf  w Noise Tolerance
//-.	Commserver jako serwis i zarządzany przez WMI lub  OPC – podzielić Commserver na część zarządzającą i zarządzaną
//-.	zapis asynchroniczny do portu RS.
//  _______________________________________________________________________________________________________
//  Date:       13-04-2005
//  Plik:       PR2130-CommServer,  PR2433-Biblioteka
//  Wersja:    
//  Zainstalowany: Wieniawskiego na probach na wspolana siec 2005 
//  Zmiany:
//    - dodano protokol null symulatora 
//    - zmieniono null z short na long
//  Problemy:
//    - przetestowac Slave SBusa
//    - przetestowac CAPI
//    - Zaimplementowac zarządzanie za pośrednictwem COM/OPC lub WMI
//    - Podzielić na service i zarządzanie
//    - Zaimplementować odbieranie telefonów a CAPI
//  _______________________________________________________________________________________________________
//  Date:       01-04-2005
//  Plik:       PR2129-CommServer, 
//  Wersja:    2.5.1907.18464
//  Zainstalowany: Wieniawskiego na probach na wspolana siec 2005
//  Zmiany:
//    - poprawiono przelaczanie na fast scanning
//  Problemy:
//    - przetestowac Slave SBusa
//    - przetestowac CAPI
//    - Zaimplementowac zarządzanie za pośrednictwem COM/OPC lub WMI
//    - Podzielić na service i zarządzanie
//    - Zaimplementować odbieranie telefonów a CAPI
//  _______________________________________________________________________________________________________
//  Date:       23 grudnia 2004, 00:14:18
//  Plik:       PR2130-CommServer,  - chyba chodzi o PR2430-Biblioteka (MZ01-04-2005)
//  Wersja:     2.6.1818.429
//  Zainstalowany:
//  Zmiany:
//    Implementacja SBUS
//    Implementacja wybranych funkcji, by zapewnić kompatybilność z testami zgodności.
//    Wersja po testach W Norynbergii PR2129-CommServer i scaleniu z wersją PR21283-CommServer zainstalowaną na 
//    WIeniawskiego. 
//  _______________________________________________________________________________________________________
//  Date:       07-09-2004
//  Plik:       PR2125-CommServer, 
//  Wersja:    
//  Zainstalowany: Wieniawskiego 
//  Zmiany:
//    - dodano SBUS'a 
//              Przetestowano czesc MASTER
//            (wstepnie przetestowano czesc )
//    - dodano CAPI (wykonano wstepne proby komunikacji)
//    - podzielono na biblioteke DLL (RTLIB) i czesc programowa
//    - wprowadzono duze zmiany w ukladzie namespace'ow i klas
//
//  Problemy:
//    - przetestowac Slave SBusa
//    - przetestowac CAPI
//    - Zaimplementowac zarządzanie za pośrednictwem COM/OPC lub WMI
//    - Podzielić na service i zarządzanie
//    - Zaimplementować odbieranie telefonów a CAPI
//  _______________________________________________________________________________________________________
//  Date:       13-07-04
//  Plik:       PR2124-CommServer, 
//  Wersja:    
//  Zainstalowany: 
//  Zmiany:
//    -  private class BaseStation.DataQueue.Tag.TagBit - dodano funkcjonalnosc podzialu Tagow na bity   
//    -  zmieniono i dopisano komentarze   
//
//  Problemy:
//    - Zaimplementowac zarządzanie za pośrednictwem COM/OPC lub WMI
//    - Podzielić na service i zarządzanie
//    - Zaimplementować odbieranie telefonów a CAPI
//  _______________________________________________________________________________________________________
//  Date:       13-03-04
//  Plik:       PR2123-CommServer, 
//  Wersja:     2.3.1535.20388
//  Zainstalowany: Wieniawskiegp
//  Zmiany:
//    - public class RS_to_Serial: CommBase, BaseStation.ICommunicationLayer
//    - Channel: bląd przy inicjowaniu kilku protokołów dla jednego kanału
//    - MainForm: zsnchronizowalem dostęp do obiektu przez threds'y wywolujące events do zmiany stanu.
//    - internal class Channel: bląd przy inicjowaniu kilku protokołów dla jednego Channel
//    - internal class DataQueue: Do ochrony procesu obsługi danych wprowadzono watchdoga
//    - internal class Segment: Zmienny czas timeout dla Scanner, ponieważ normalny brak aktywności w 
//      segmencie wywala program - gdy segment nie jest aktywny - timeout jest wylaczany.
//    - public class Statistics Monitorowanie statystyk poprzez OPC.
//    - public class OPC_Interface Wszystkie operacje na OPC są zabezpieczone przed współbieżnym dostępem 
//      przez lock'a
//    - public class MonitoredThread Wprowadzenie funkcji wstrzymania zmiany czasu timeout - ResetWatchDog
//    - public class RS_to_Serial: bład przepełnienia bufora
//
//  Problemy:
//    - Zaimplementowac zarządzanie za pośrednictwem COM/OPC lub WMI
//    - Podzielić na service i zarządzanie
//    - Zaimplementować odbieranie telefonów a CAPI
//  _______________________________________________________________________________________________________
//  Data 11-11-2003; File: PR2121-CommServer, Version 1.2.1409.24240 - zainstalowana na ieniawskiego
//  Zmiany:
//    Wprowadzono WatchDoga dla Thread - po ustawienym czasie braku aktywnosci nastepuje Reboot
//    Wprowadzono Assert z mozliwoscia Reboot lub wsawienia do kolejki bledow w Menager
//  Problemy:
//    - Channel: bląd przy inicjowaniu kilku protokołów dla jednego kanału
//    - Blad prawdopodobnie przy jednoczesnej probie wykonania zapisu ze stacji klienckich (sprawdzi)
//    - Zaimplementowac zarządzanie za pośrednictwem COM lun WMI
//    - Podzielić na service i zarządzanie
//    - Zaimplementować odbieranie telefonów a CAPI

