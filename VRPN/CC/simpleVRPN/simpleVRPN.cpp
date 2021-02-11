// Simple VRPN
#include <iostream>

#include "vrpn_Button.h"
#include "vrpn_Analog.h"

void VRPN_CALLBACK handle_analog( void* userData,  const vrpn_ANALOGCB a ) 
{
    int nbChannels = a.num_channel;
    std::cout << "Analog : ";
    std::cout << a.channel[0] << " " << a.channel[1] << std::endl;
}

void VRPN_CALLBACK handle_button( void* userData,  const vrpn_BUTTONCB b ) 
{
    std::cout << "Button '" << b.button << "': "  << b.state << std::endl;
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
