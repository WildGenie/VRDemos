using UnityEngine;

/// InteractionTest
/// <summary>
/// Abgeleitet von View, analog zu ViewMVR und ViewStandalone.
/// 
/// Für MiddleVR fragen wir den DeviceManager ab und legen vorher fest,
/// welcher Button verwendet werden soll.
/// </summary>
public class ViewMVR : View
{

    /// <summary>
    /// Welcher Wandbutton soll verwendet werden? Default ist die Nummer 2, 
    /// das entspricht der Taste 1 auf dem Keyboard in der Default-Konfiguration.
    /// </summary>
    public uint buttonNbr = 2;

    /// <summary>
    /// In Awake erstellen wir den Controller und stellen
    /// die Verbindung zur Model-Klasse her.
    /// </summary>
    private void Awake()
    {
        Mod = new Model(false);
        Con = new Controller(Mod);
    }

    /// <summary>
    /// Update mit Anweisungen aus MiddleVR
    /// </summary>
    protected void Update()
    {
        /// MiddleVR DeviceManager abfragen
        var deviceMgr = MiddleVR.VRDeviceMgr;

        if (deviceMgr != null)
        {
            if (deviceMgr.IsWandButtonToggled(buttonNbr))
                Con.ToggleHighlight();

            Draw();
        }
    }
}

