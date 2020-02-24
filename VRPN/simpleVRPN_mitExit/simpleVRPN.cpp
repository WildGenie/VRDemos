// Simple VRPN
#include <iostream>

#include "vrpn_Button.h"
#include "vrpn_Analog.h"

void VRPN_CALLBACK handle_analog( void* userData,  const vrpn_ANALOGCB a ) 
{
    int nbChannels = a.num_channel;
    std::cout << "Analog : ";
    std::cout << a.channel[0] << " " << a.channel[1] << std::endl;

        // Experiment: wenn Maus weit genug links, dann stop des Programms
        if (a.channel[0] < 0.1)
              std::cout << "If You move the mouse further to the left the program will exit!" << std::endl;
        if (a.channel[0] < 0.05)
              exit(0);
}

void VRPN_CALLBACK handle_button( void* userData,  const vrpn_BUTTONCB b ) 
{
    std::cout << "Button '" << b.button << "': "  << b.state << std::endl;
    if (b.button == 1 && b.state== 1)
        std::cout << "If You release the Esc button, the program will exit!" << std::endl;
    if (b.button == 1 && b.state == 0) {
         std::cout << "You hit the Esc button, the program will exit!" << std::endl;
         std::cout << "Bye!" << std::endl;         
         exit(0);
    }
}

int main(int argc, char* argv[]) {
   vrpn_Analog_Remote* vrpnAnalog =
     new vrpn_Analog_Remote("Mouse0@localhost");
   vrpn_Button_Remote* vrpnButton =
     new vrpn_Button_Remote("Keyboard0@localhost");

  vrpnAnalog->register_change_handler(0, handle_analog);
  vrpnButton->register_change_handler(0, handle_button);
  std::cout << "-----" << std::endl;
  std::cout << "Move the mouse and use the keyboard!" << std::endl;
  std::cout << "-----" << std::endl;

  while(1) {
        vrpnAnalog->mainloop();
        vrpnButton->mainloop();
  }
  return 0;
}
