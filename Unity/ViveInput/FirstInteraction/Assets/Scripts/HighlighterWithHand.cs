using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Highlighter für ein GameObject,
/// abhängig von einem Tastendruck auf einem Controller.
/// Diese Klasse lässt zu die verwendete Hand
/// (links/rechts) im Inspektor auszuwechseln.
/// 
/// Wir verwenden das Observer-Pattern.
/// <remarks>
/// Version mit Vive Input Utility
/// 
/// Die Basisklasse enthält die vom VR-Package
/// unabhängigen Bestandteile!
/// </remarks>
/// </summary>
public class HighlighterWithHand : View
{
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist der Trigger der Controller.
    /// </summary>
    [Tooltip("Welcher Button auf dem Controller soll verewendet werden?")]
    public ControllerButton theButton = ControllerButton.Trigger;

    /// <summary>
    /// Welche Hand wollen wir verwenden?
    /// 
    /// Default ist die rechte Hand.
    /// </summary>
    [Tooltip("Welcher Controller (links/rechts) soll für das Highlight verwendet werden?")]
    public HandRole mainHand = HandRole.RightHand;

    /// <summary>
    /// In Awake erstellen wir den Controller und stellen
    /// die Verbindung zur Model-Klasse her.
    /// </summary>
    private void Awake()
    {
        Mod = new Model(false);
        Mod.Attach(this);

        // Listener für den Button registrieren
        ViveInput.AddListenerEx(mainHand,
                                theButton,
                                ButtonEventType.Down,
                                Mod.Toggle);

        ViveInput.AddListenerEx(mainHand,
                                theButton,
                                ButtonEventType.Up,
                                Mod.Toggle);
    }

    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(mainHand,
                                   theButton,
                                   ButtonEventType.Down,
                                   Mod.Toggle);

        ViveInput.RemoveListenerEx(mainHand,
                                   theButton,
                                   ButtonEventType.Up,
                                   Mod.Toggle);
    }
}
