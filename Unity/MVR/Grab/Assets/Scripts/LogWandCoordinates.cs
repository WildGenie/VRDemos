/// InteractionTest
/// <summary>
/// Entwickelt aus dem Sample aus dem Users Guide von MiddleVR
/// 
/// Ein Wand-Button wird verwendet, um die Farbe eines Objekts
/// zu wechseln.
/// 
/// Der verwendete Wand-Button und auch die Farbe, auf die gewechselt werden
/// soll kann im Editor eingestellt werden.
/// </summary>

using UnityEngine;
using MiddleVR_Unity3D;

public class LogWandCoordinates : MonoBehaviour {

    /// <summary>
    /// Welcher Wandbutton soll verwendet werden? Default ist die Nummer 2, 
    /// das entspricht der Taste 1 auf dem Keyboard in der Default-Konfiguration.
    /// </summary>
    public uint buttonNbr = 2;

    protected void Start()
    {
    }

    protected void Update()
    {
        logCoordinates();
    }

    /// <summary>
    /// Wir schreiben die Koordinaten des Wand in die Konsole, um zu
    /// überprüfen, ob die Konfiguration mit Tracker und Wand funktioniert.
    /// </summary>
    private void logCoordinates()
    {
        bool toggled;
        float x, y;
        var deviceMgr = MiddleVR.VRDeviceMgr;
        if (deviceMgr != null)
        {
            toggled = deviceMgr.IsWandButtonToggled(buttonNbr);

            if (toggled)
            {
                x = deviceMgr.GetWandHorizontalAxisValue();
                y = deviceMgr.GetWandVerticalAxisValue();
                Debug.unityLogger.Log("Die aktuellen Koordinaten des Wand");
                Debug.unityLogger.Log("x: " + x + " , y: " + y);
            }
        }
    }
}
