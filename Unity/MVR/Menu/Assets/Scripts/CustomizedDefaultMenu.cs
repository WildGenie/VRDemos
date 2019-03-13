/* CustomizedDefaultMenu
 *
 * Basiert auf dem MiddleVR-Sample. Angepasst von Manfred Brill.
 * 
 * Idee: man könnte auch eine Liste von Strings als public
 * deklarieren, und damit im Editor festlegen lassen, welche
 * Einträge aus dem Default-Menu gelöscht werden.
 */

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class CustomizedDefaultMenu : MonoBehaviour
{
    // Start waits on VRMenu creation with a coroutine
    protected IEnumerator Start()
    {
        MVRTools.RegisterCommands(this);

        VRMenu MiddleVRMenu = null;
        while (MiddleVRMenu == null || MiddleVRMenu.menu == null)
        {
            // Wait for VRMenu to be created
            yield return null;
            MiddleVRMenu = FindObjectOfType(typeof(VRMenu)) as VRMenu;
        }

        AddButton(MiddleVRMenu, "Eintrag 1", "ButtonHandler1", 0);
        AddButton(MiddleVRMenu, "Eintrag 2", "ButtonHandler2", 1);
        AddSeparator(MiddleVRMenu, 2);
        AddSlider(MiddleVRMenu, "Valuator", "SliderHandler", position: 3);
        AddSeparator(MiddleVRMenu, 4);
        // Alles bis auf Exit Simulation aus dem Menu entfernen
        RemoveItem(MiddleVRMenu, "Navigation");
        RemoveItem(MiddleVRMenu, "Manipulation");
        RemoveItem(MiddleVRMenu, "Hand");
        RemoveItem(MiddleVRMenu, "General");
        RemoveItem(MiddleVRMenu, "Reset");
        //MoveItems(MiddleVRMenu);

        // End coroutine
        yield break;
    }

    [VRCommand]
    private void ButtonHandler1()
    {
        Debug.Log("Menu-Eintrag 1 wurde ausgewählt");
    }

    [VRCommand]
    private void ButtonCommandHandler2()
    {
        Debug.Log("Menu-Eintrag 2 wurde ausgewählt");
    }

    [VRCommand]
    private void SliderHandler(float iValue)
    {
        Debug.Log("Slider value : " + iValue);
    }

    private void AddButton(VRMenu iVRMenu, string menutext, string commandHandler, uint position)
    {
        // Add a button at the start of the menu
        var button = new vrWidgetButton("VRMenu.MyCustomButton", iVRMenu.menu, menutext, MVRTools.GetCommand(commandHandler));
        iVRMenu.menu.SetChildIndex(button, position);
        MVRTools.RegisterObject(this, button);
   }

    private void AddSlider(VRMenu iVRMenu, string slidertext, string commandHandler, uint position)
    {
        // Add a button at the start of the menu
        var slider = new vrWidgetSlider("VRMenu.MyCustomSlider", iVRMenu.menu, slidertext, MVRTools.GetCommand(commandHandler), 
                                         5.0f, 0.0f, 10.0f, 1.0f);
        iVRMenu.menu.SetChildIndex(slider, position);
        MVRTools.RegisterObject(this, slider);
    }

    private void AddSeparator(VRMenu iVRMenu, uint position)
    {
        vrWidgetSeparator separator = new vrWidgetSeparator("VRMenu.MyCustomSeparator", iVRMenu.menu);
        iVRMenu.menu.SetChildIndex(separator, position);
        MVRTools.RegisterObject(this, separator);
    }


    /// <summary>
    /// Eintrag mit dem übergebenen Pattern aus dem Menu entfernen.
    /// </summary>
    /// <param name="iVRMenu">Menu</param>
    /// <param name="pattern">Muster, nach dem gesucht wird</param>
    private void RemoveItem(VRMenu iVRMenu, string pattern)
    {
        for (uint i = 0; i < iVRMenu.menu.GetChildrenNb(); ++i)
        {
            var widget = iVRMenu.menu.GetChild(i);
            if( widget.GetLabel().Contains(pattern))
            {
                iVRMenu.menu.RemoveChild(widget);
                break;
            }
        }   
    }

    private void MoveItems(VRMenu iVRMmenu)
    {
        // Move every menu item under a sub menu
        var subMenu = new vrWidgetMenu("VRMenu.MyNewSubMenu", null, "MiddleVR Menu");

        while (iVRMmenu.menu.GetChildrenNb() > 0)
        {
            var widget = iVRMmenu.menu.GetChild(0);
            widget.SetParent(subMenu);
        }

        subMenu.SetParent(iVRMmenu.menu);
    }
}
