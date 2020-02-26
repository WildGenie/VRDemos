/*!
 * Replay von Werten eines VRPN-Servers aus einer Log-Datei
 *
 * Diese Anwendung ist an das Beispiel printvals.C
 * in der VRPN-Distribution angelehnt!
 *
 * Voraussetzung für die Verwendung dieser Anwendung
 * ist eine Datei, die Log-Ausgaben eines Servers enthält.
 *
 * Die Daten können mit dem Projekt simpleLogger erzeugt werden.
 */
#include <signal.h> 
#include <iostream>

#include <vrpn_Button.h>
#include <vrpn_Analog.h>
#include <vrpn_Connection.h>                           
#include <vrpn_BaseClass.h>
#include <vrpn_FileConnection.h>
#include <vrpn_FileController.h>

// Flag für das Beenden der Anwendung
int done = 0;

// Instanz eines analogen Geräts
vrpn_Analog_Remote *vrpnAnalog;
// Instanzen für zwei Button-Geräte
vrpn_Button_Remote *vrpnButton,
                   *vrpnButton2;
// Connection zum VRPN Server
vrpn_Connection *c;
// File Controller für die Log-Files
vrpn_File_Controller *fc;

// Callbacks für die Geräte und die Connection
void VRPN_CALLBACK handle_analog( void* userData,  const vrpn_ANALOGCB a ) 
{
    int nbChannels = a.num_channel;
    std::cout << "Analog : ";
    std::cout << "x= " << a.channel[0] << ", y= " << a.channel[1] << std::endl;
}

void VRPN_CALLBACK handle_button( void* userData,  const vrpn_BUTTONCB b ) 
{
    std::cout << "Button '" << b.button << "': "  << b.state << std::endl;
}

void VRPN_CALLBACK handle_button2(void* userData, const vrpn_BUTTONCB b)
{
	std::cout << "Button der Maus " << b.button << std::endl;
	std::cout << "Zustand:" << b.state << std::endl;
}

int VRPN_CALLBACK handle_gotConnection(void *, vrpn_HANDLERPARAM) 
{
	std::cout << "simpleLogger hat eine Verbindung zum Server" << std::endl;
	return 0;
}

// Geräte initialisieren
// Damit eine Verbindung zu einer Datei aufgemacht werden kann,
// muss der Parameter station_name als erstes "file:" enthalten.
// Zum Beispiel: station_name = "file:vrpn.log".
void init(const char * station_name)
{
	// Verbindung zur Datei erzeugen
	c = new vrpn_File_Connection(station_name);
	if (c)
	{
		std::cout << "---" << std::endl;
		std::cout << "Verbindung zur Datei ist aufgebaut!" << std::endl;
		std::cout << "---" << std::endl;
	}

	fc = new vrpn_File_Controller(c);

	vrpnAnalog = new vrpn_Analog_Remote("Mouse0@file:vrpn.log");
	vrpnButton = new vrpn_Button_Remote("Keyboard0@file:vrpn.log");
	vrpnButton2 = new vrpn_Button_Remote("Mouse0@file:vrpn.log");
	// Callbacks registrieren
	vrpnAnalog->register_change_handler(0, handle_analog);
	vrpnButton->register_change_handler(0, handle_button);
	vrpnButton2->register_change_handler(0, handle_button2);
}

// Beenden der Kommunikation und der Event-Loop
void shutdown() 
{
	const char * n; 
	long i;
	std::cout << "Shutting Down!" << std::endl;

	if (vrpnAnalog)
		delete vrpnAnalog;
	if (vrpnButton)
		delete vrpnButton;
	if (vrpnButton2)
		delete vrpnButton2;
	if (c)
		delete c;

	done = 1;
}

void handle_cntl_c(int) {
	done = 1;
}

int main(int argc, char* argv[]) 
{
	std::cout << "-----" << std::endl;
	std::cout << "Replay of data in the log-file vrpn.log" << std::endl;
	std::cout << "-----" << std::endl;
	const char * local_in_logfile = "vrpn.log";
	init(local_in_logfile);

	// signal für das korrekte Schließen der Datei
	signal(SIGINT, handle_cntl_c);

	std::cout << "-----" << std::endl;
	std::cout << "Replay startet!" << std::endl;
	std::cout << "-----" << std::endl;
    // Die Event-Loop starten
    while( !done ) 
    {
		c->mainloop();
		vrpnAnalog->mainloop();
        vrpnButton->mainloop();
		vrpnButton2->mainloop();

		// 1ms nichts tun (aus VRPN-demos)
		//vrpn_SleepMsecs(1);
    }

    shutdown();

  exit(0);
}
