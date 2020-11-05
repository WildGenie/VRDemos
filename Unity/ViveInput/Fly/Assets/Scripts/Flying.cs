using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Fliegen mit dem Vive Input Utility
/// 
/// Wir verwenden eine Hand, die im Inspector
/// der Szene einstellbar ist. 
/// Dafault ist rechts.
/// 
/// Als Bewegungsrichtung verwenden wir die Orientierung
/// des Controllers, den wir auswählen.
/// 
/// Mit Trackpad oben und unten können wir beschleunigen
/// bzw. abbremsen.
/// </summary>
public class Flying : MonoBehaviour
{
    [Header("Geräte und Buttons")]
    /// <summary>
    /// Welchen Controller verwenden wir für die Locomotion?
    /// 
    /// Als Default verwenden wir den Controller in der rechten Hand,
    /// also "RightHand" im "ViveCameraRig".
    /// </summary>
    [Tooltip("Rechter oder linker Controller?")]
    public HandRole flyControl = HandRole.RightHand;

    /// <summary>
    /// Welches GameObject verwenden wir für die Definition der Richtung?
    /// 
    /// Sinnvoll ist einer der beiden Controller, aber auch andere
    /// GameObjects (wie der Kopf) können verwendet werden.
    /// </summary>
    [Tooltip("Welches GameObject verwenden wir für die Definition der Richtung?\nSinnvoll ist einer der Controller, aber auch andere Objekte sind zulässig.")]
    public GameObject flyOrientation;

    /// <summary>
    /// Der verwendete Button, der die Bewegung auslöst, kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist "Trigger"
    /// </summary>
    [Tooltip("Welchen Button verwenden wir für das Fliegen?\n Wir fliegen so lange der Button gedrückt ist.")]
    public ControllerButton flyButton = ControllerButton.FullTrigger;

    /// <summary>
    /// Button auf dem Controller für das Abbremsen der Fortbewegung.
    /// 
    /// Default ist "DPad"
    /// </summary>
    [Tooltip("Mit welchem Button können bremsen?")]
    public ControllerButton decButton = ControllerButton.DPadDown;

    /// <summary>
    /// Button auf dem Controller für das Beschleunigen der Fortbewegung.
    /// 
    /// Default ist "DPad"
    /// </summary>
    [Tooltip("Mit welchem Button können beschleunigen?")]
    public ControllerButton accButton = ControllerButton.DPadUp;

    /// <summary>
    /// Variable, die die Geschwindigkeit der Fortbewegung steuert.
    /// </summary>
    [Header("Parameter für das Travelling")]
    [Tooltip("Faktor für die Geschwindigkeit der Bewegung")]
    [Range(0.1f, 2.0f)]
    public float speed = 0.5f;

    /// <summary>
    /// Delta für Beschleunigen und Abbremsen
    /// </summary>
    private float deltaSpeed = 0.005f;

    /// <summary>
    /// Die Callbacks für Beschleunigung und Abbremsen registrieren.
    /// </summary>
    private void Awake()
    {
        ViveInput.AddListenerEx(this.flyControl, decButton, ButtonEventType.Down, DecreaseSpeed);
        ViveInput.AddListenerEx(this.flyControl, accButton, ButtonEventType.Down, IncreaseSpeed);
    }

    /// <summary>
    /// Die Callbacks wieder abhängen.
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(this.flyControl, decButton, ButtonEventType.Down, DecreaseSpeed);
        ViveInput.RemoveListenerEx(this.flyControl, accButton, ButtonEventType.Down, IncreaseSpeed);
    }

    /// <summary>
    /// Beschleunigen
    /// </summary>
    private void IncreaseSpeed()
    {
        speed -= this.deltaSpeed;
        Debug.Log("speed verkleinert auf " + this.speed);
    }

    private void DecreaseSpeed()
    {
        speed += this.deltaSpeed;
        Debug.Log("speed vergrößert auf " + this.speed);
    }

    /// <summary>
    /// In Update abfragen, ob der Button gedrückt wurde
    /// und wir fliegen.
    /// </summary>
    void FixedUpdate()
    {
        if (ViveInput.GetPress(this.flyControl, this.flyButton))
            Fly();
    }

    /// <summary>
    /// Wir fragen die Orientierung des Geräts ab, das wir für die
    /// Richtungsdefinition verwenden und übertragen diese Orientierung
    /// auf das von uns manipulierte GameObject (typischer Weise die Kamera).
    /// </summary>
    private void Fly()
    {
        Quaternion moveOrientation = flyOrientation.transform.rotation;
        Vector3 dir = (moveOrientation * Vector3.forward).normalized;

        //Change: Removed update of the players rotation in dependence of the child object (controller).
        //Reason: Updating the players roation changes the orientation of the child object and thus consecutive the orientation of the parent object.
        //transform.rotation = moveOrientation;
        transform.Translate(speed * Time.deltaTime * dir);
    }
}
