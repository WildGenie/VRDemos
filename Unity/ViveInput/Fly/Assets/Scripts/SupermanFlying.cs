using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Fliegen mit dem Vive Input Utility
/// 
/// Wir verwenden eine Hand, die im Inspector
/// der Szene einstellbar ist,
/// Dafault ist rechts.
/// Und die Position des Objekts, das wir
/// als Kopf definiert. 
/// Der Verbindungsvektor dieser beiden Objekte
/// definiert die Richtung der Bewegung,
/// die Länge des Verbindungsvektors die
/// Geschwindigkeit.
/// </summary>
public class SupermanFlying : MonoBehaviour
{
    [Header("Geräte und Buttons")]
    /// <summary>
    /// Welches Szenenobjekt verändern wir?
    /// 
    /// Als Default, was sonst, verändern wir die Kopfposition.
    /// Verwenden wir VIU, ist dies durch das GameObject
    /// "Camera" im ViveCameraRig gegeben.
    /// </summary>
    [Tooltip("Welches GameObject verwenden wir als Kopf?")]
    public GameObject head;

    /// <summary>
    /// Der verwendete Button, der die Bewegung auslöst, 
    /// Kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist "Trigger"
    /// </summary>
    [Tooltip("Welchen Button verwenden wir für das Fliegen?\n Wir fliegen so lange der Button gedrückt ist.")]
    public ControllerButton flyButton = ControllerButton.FullTrigger;

    /// <summary>
    /// Welchen Controller verwenden wir für den Button?
    /// 
    /// Als Default verwenden wir den Controller in der rechten Hand,
    /// also "RightHand" im "ViveCameraRig".
    /// 
    /// Todo: das müssten wir aus der rightHand abfragen können,
    /// denn dort hängt die Klasse ViveRoleSetter dran ...
    /// </summary>
    [Tooltip("Welchen Controller verwenden wir?")]
    public HandRole flyControl = HandRole.RightHand;

    /// <summary>
    /// GameObject, mit dessen Orientierung wir die Richtung der
    /// Fortbewegung festlegen.
    /// 
    /// Als Default verwenden wir "RightHand" im "ViveCameraRig".
    /// Wir benötigen dieses GameObject, da wir einen transform-Knoten
    /// haben müssen.
    /// </summary>
    [Tooltip("Welches GameObject definiert mit seiner Position die Richtung und die Geschwindigkeit des Fliegens?")]
    public GameObject flyHand;

    /// <summary>
    /// In FixedUpdate abfragen, ob der Button gedrückt wurde
    /// und wir fliegen.
    /// </summary>
    void Update()
    {
        if (ViveInput.GetPress(flyControl, flyButton))
            Move();
    }

    /// <summary>
    /// Wir fragen die Position der verwendeten Hand und des Kopfs ab,
    /// berechnen daraus die Bewegungsrichtung und aus der Länge dieses
    /// Vektors die Geschwindigkeit.
    /// </summary>
    private void Move()
    {
        Vector3 dir = flyHand.transform.position - head.transform.position;
        float mag = dir.magnitude;
        dir.Normalize();

        transform.Translate(mag * Time.deltaTime * dir);
    }
}
