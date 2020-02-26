/*!
 * Wir verwenden den Dummy-Tracker vrpn_Tracker_NULL
 * aus der Konfiguration als Demonstration für die
 * Verarbeitung von Tracker-Daten.
 *
 * Der Tracker ist in der Konfiguration als Tracker0@localhost
 * ansprechbar und liefert auf zwei Sensoren immer
 * die Einheitsmatrix!
 *
 * Alternativ können wir Tracker1@localhost verwenden,
 * damit erhalten wir die Daten des vrpn_Tracker_Spin.
 * Dieser Tracker liefert eine Rotation um eine Achse.
 * Aktuell ist die y-Achse in der Konfiguration eingestellt.
 *
 * Wir verwenden auch die Tastatur, um mit der ESC-Taste
 * die Anwendung zu beenden!
 */
#include <iostream>

#include <vrpn_Button.h>
#include <vrpn_Tracker.h>

// Flag für das Beenden der Anwendung
int done = 0;

// Instanzen für die Tastatur
vrpn_Button_Remote *vrpnButton;
// Die Tracker-Instanz
vrpn_Tracker_Remote *vrpnTracker;

// Callbacks für die Geräte
void VRPN_CALLBACK handle_button( void* userData,  const vrpn_BUTTONCB b ) 
{
    std::cout << "Button '" << b.button << "': "  << b.state << std::endl;
    if (b.button == 1 && b.state== 1)
        std::cout << "If You release the Esc button, the program will exit!" << std::endl;
    if (b.button == 1 && b.state == 0) {
         std::cout << "You hit the Esc button, the program will exit!" << std::endl;
         std::cout << "Bye!" << std::endl;         
         //done = 1;
    }
}

void VRPN_CALLBACK handle_tracker(void* userData, const vrpn_TRACKERCB t)
{
	// Es gibt zwei "Sensoren"
	std::cout << "Sensor " << t.sensor << std::endl;
	std::cout << "Position:" << std::endl;
	std::cout << t.pos[0] << "," << t.pos[1] << "," << t.pos[2] << std::endl;
	std::cout << "Orientierung als Quaternion:" << std::endl;
	std::cout << t.quat[0] << "," << t.quat[1] << "," << t.quat[2] << "," << t.quat[3] << std::endl;
}

// Geräte initialisieren
void init()
{
	vrpnButton  = new vrpn_Button_Remote("Keyboard0@localhost");
	//vrpnTracker = new vrpn_Tracker_Remote("Tracker0@localhost");
	vrpnTracker = new vrpn_Tracker_Remote("Tracker1@localhost");
	// Callbacks registrieren
	vrpnButton->register_change_handler(0, handle_button);
	vrpnTracker->register_change_handler(0, handle_tracker);
}

// Beenden der Kommunikation und der Event-Loop
void shutdown() 
{
	if (vrpnButton)
		delete vrpnButton;
	if (vrpnTracker)
		delete vrpnTracker;

	std::cout << "Shutting Down!" << std::endl;
}

int main(int argc, char* argv[]) 
{
	init();

  std::cout << "-----" << std::endl;
  std::cout << "Working with tracker data" << std::endl;
  std::cout << "we always get the identy matrix!" << std::endl;
  std::cout << "Use ESC to stop the application!" << std::endl;
  std::cout << "-----" << std::endl;

  // Die Event-Loop starten
  while( !done ) 
  {
        vrpnButton->mainloop();
		vrpnTracker->mainloop();

		// 1ms nichts tun (aus VRPN-demos)
		vrpn_SleepMsecs(1);
  }

  shutdown();

  exit(0);
}
