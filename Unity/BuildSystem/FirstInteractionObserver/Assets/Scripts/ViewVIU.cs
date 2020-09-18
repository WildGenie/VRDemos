using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// View für eine eine analoge Uhr
/// mit Stunden- und Minutenzeiger
/// </summary>
public class ViewVIU : View
{
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist der Trigger der Controller.
    /// </summary>
    public ControllerButton theButton = ControllerButton.Trigger;

    /// <summary>
    /// In Awake erstellen wir den Controller und stellen
    /// die Verbindung zur Model-Klasse her.
    /// </summary>
    private void Awake()
    {
        Mod = new Model(false);
        Mod.Attach(this);

        // Listener für den Button registrieren
        ViveInput.AddListenerEx(HandRole.RightHand,
                                theButton,
                                ButtonEventType.Down,
                                Mod.Toggle);

        ViveInput.AddListenerEx(HandRole.RightHand,
                                theButton,
                                ButtonEventType.Up,
                                Mod.Toggle);

        ViveInput.AddListenerEx(HandRole.LeftHand,
                                theButton,
                                ButtonEventType.Down,
                                Mod.Toggle);

        ViveInput.AddListenerEx(HandRole.LeftHand,
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
        ViveInput.RemoveListenerEx(HandRole.RightHand,
                                   theButton,
                                   ButtonEventType.Down,
                                   Mod.Toggle);

        ViveInput.RemoveListenerEx(HandRole.RightHand,
                                   theButton,
                                   ButtonEventType.Up,
                                   Mod.Toggle);

        ViveInput.RemoveListenerEx(HandRole.LeftHand,
                                   theButton,
                                   ButtonEventType.Down,
                                   Mod.Toggle);

        ViveInput.RemoveListenerEx(HandRole.LeftHand,
                                   theButton,
                                   ButtonEventType.Up,
                                   Mod.Toggle);
    }
}
