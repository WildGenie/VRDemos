/*!
 * Logging von Werten eines VRPN-Servers für den
 * späteren Replay.
 *
 * Diese Anwendung ist an das Beispiel printvals.C
 * in der VRPN-Distribution angelehnt!
 *
 * Voraussetzung für die Verwendung dieser Anwendung
 * ist ein VRPN-Server, als Default wird
 * localhost erwartet.
 *
 * An diesem VRPN-Server sollten eine Maus
 * und eine Tastatus verbunden sein, die
 * Daten liefern.
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

        // Experiment: wenn Maus weit genug links, dann stop des Programms
        if (a.channel[0] < 0.1)
              std::cout << "If You move the mouse further to the left the program will exit!" << std::endl;
        if (a.channel[0] < 0.05)
              done = 1;
}

void VRPN_CALLBACK handle_button( void* userData,  const vrpn_BUTTONCB b ) 
{
    std::cout << "Button '" << b.button << "': "  << b.state << std::endl;
    if (b.button == 1 && b.state == 1)
        std::cout << "If you release the Esc button, the program will exit!" << std::endl;
    if (b.button == 1 && b.state == 0) {
         std::cout << "You hit the Esc button, the program will exit!" << std::endl;
         std::cout << "Bye!" << std::endl;         
         done = 1;
    }
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
void init(const char * local_in_logfile)
{
	c = vrpn_get_connection_by_name("Button0@localhost",
		                            local_in_logfile);

	fc = new vrpn_File_Controller(c);

	vrpnAnalog = new vrpn_Analog_Remote("Mouse0@localhost");
	vrpnButton = new vrpn_Button_Remote("Keyboard0@localhost");
	vrpnButton2 = new vrpn_Button_Remote("Mouse0@localhost");
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

	if (vrpnAnalog)
		delete vrpnAnalog;
	if (vrpnButton)
		delete vrpnButton;
	if (vrpnButton2)
		delete vrpnButton2;
	if (c)
		delete c;

	std::cout << "Shutting Down!" << std::endl;
	done = 1;
}

void handle_cntl_c(int) {
	done = 1;
}

int main(int argc, char* argv[]) 
{
	const char * local_in_logfile = "vrpn.log";
	init(local_in_logfile);

	// signal handler so logfiles get closed right
	signal(SIGINT, handle_cntl_c);

    std::cout << "-----" << std::endl;
	std::cout << "Logging the data!" << std::endl;
    std::cout << "Move the mouse and use the keyboard!" << std::endl;
    std::cout << "-----" << std::endl;

    // Die Event-Loop starten
    while( !done ) 
    {
		c->mainloop();
		vrpnAnalog->mainloop();
        vrpnButton->mainloop();
		vrpnButton2->mainloop();

		// 1ms nichts tun (aus VRPN-demos)
		vrpn_SleepMsecs(1);
    }

    shutdown();

  exit(0);
}
