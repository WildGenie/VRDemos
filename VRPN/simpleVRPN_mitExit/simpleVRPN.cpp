/*!
 * Ein Hello World für die Verwendung von VRPN.
 * Voraussetzung für die Verwendung dieser Anwendung
 * ist ein VRPN-Server, als Default wird
 * localhost erwartet.
 *
 * An diesem VRPN-Server sollten eine Maus
 * und eine Tastatus verbunden sein, die
 * Daten liefern.
 *
 * Wir fragen die Gerätekoordinaten der Maus,
 * die Buttons der Maus und die
 * Tasten der Tastatur ab und geben
 * die empfangenen Werte auf der Konsole aus.
 */
#include <iostream>

#include <vrpn_Button.h>
#include <vrpn_Analog.h>

// Flag für das Beenden der Anwendung
int done = 0;

// Instanz eines analogen Geräts
vrpn_Analog_Remote *vrpnAnalog;
// Instanzen für zwei Button-Geräte
vrpn_Button_Remote *vrpnButton,
                   *vrpnButton2;

// Callbacks für die Geräte
void VRPN_CALLBACK handle_analog( void* userData,  const vrpn_ANALOGCB a ) 
{
    int nbChannels = a.num_channel;
    std::cout << "Analog : ";
    std::cout << a.channel[0] << " " << a.channel[1] << std::endl;

        // Ist die Maus weit genug links, dann stoppt die Anwendung:
        if (a.channel[0] < 0.1)
              std::cout << "If you move the mouse further to the left the program will exit!" << std::endl;
        if (a.channel[0] < 0.05)
              done = 1;
}

void VRPN_CALLBACK handle_button( void* userData,  const vrpn_BUTTONCB b ) 
{
    std::cout << "Button '" << b.button << "': "  << b.state << std::endl;
    if (b.button == 1 && b.state== 1)
        std::cout << "If You release the Esc button, the program will exit!" << std::endl;
    if (b.button == 1 && b.state == 0) {
         std::cout << "You released the Esc button, the program will exit!" << std::endl;
         std::cout << "Bye!" << std::endl;         
         done = 1;
    }
}

void VRPN_CALLBACK handle_button2(void* userData, const vrpn_BUTTONCB b)
{
	std::cout << "Button der Maus " << b.button << std::endl;
	std::cout << "Zustand:" << b.state << std::endl;
}

// Geräte initialisieren
void init()
{
	vrpnAnalog  = new vrpn_Analog_Remote("Mouse0@localhost");
	vrpnButton  = new vrpn_Button_Remote("Keyboard0@localhost");
	vrpnButton2 = new vrpn_Button_Remote("Mouse0@localhost");
	// Callbacks registrieren
	vrpnAnalog->register_change_handler(0, handle_analog);
	vrpnButton->register_change_handler(0, handle_button);
	vrpnButton2->register_change_handler(0, handle_button2);
}

// Beenden der Kommunikation und der Event-Loop
void shutdown() 
{
	if (vrpnAnalog)
		delete vrpnAnalog;
	if (vrpnButton)
		delete vrpnButton;
	if (vrpnButton2)
		delete vrpnButton2;

	std::cout << "Shutting Down!" << std::endl;
}

int main(int argc, char* argv[]) 
{
	init();

  std::cout << "-----" << std::endl;
  std::cout << "Move the mouse and use the keyboard!" << std::endl;
  std::cout << "Use the keys and buttons on the mouse and the keyboard!" << std::endl;
  std::cout << "-----" << std::endl;

  // Die Event-Loop starten
  while( !done ) 
  {
        vrpnAnalog->mainloop();
        vrpnButton->mainloop();
		vrpnButton2->mainloop();

		// 1ms nichts tun (aus VRPN-demos)
		vrpn_SleepMsecs(1);
  }

  shutdown();

  exit(0);
}
