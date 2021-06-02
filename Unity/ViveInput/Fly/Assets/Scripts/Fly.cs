//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
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
public class Fly : MonoBehaviour
{
    [Header("Geräte und Buttons")]
    /// <summary>
    /// Welches GameObject verwenden wir für die Definition der Richtung?
    /// 
    /// Sinnvoll ist einer der beiden Controller, aber auch andere
    /// GameObjects (wie der Kopf) können verwendet werden.
    /// </summary>
    [Tooltip("Welches GameObject verwenden wir für die Definition der Richtung?")]
    public GameObject flyOrientation;
    /// <summary>
    /// Welchen Controller verwenden wir für die Locomotion?
    /// 
    /// Als Default verwenden wir den Controller in der rechten Hand,
    /// also "RightHand" im "ViveCameraRig".
    /// </summary>
    [Tooltip("Rechter oder linker Controller für die Taste?")]
    public HandRole flyControl = HandRole.RightHand;

    /// <summary>
    /// Der verwendete Button, der die Bewegung auslöst, kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist "Trigger"
    /// </summary>
    [Tooltip("Welchen Button verwenden wir für das Fliegen?\n Wir fliegen so lange der Button gedrückt ist.")]
    public ControllerButton flyButton = ControllerButton.Trigger;


    public ControllerButton reverseButton = ControllerButton.Pad;

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
    [Range(0.001f, 0.2f)]
    public float speed = 0.005f;

    /// <summary>
    /// Delta für Beschleunigen und Abbremsen
    /// </summary>
    private float deltaSpeed = 0.001f;

    private int reverseGear = 1;

    /// <summary>
    /// Die Callbacks für Beschleunigung und Abbremsen registrieren.
    /// </summary>
    private void Awake()
    {
        ViveInput.AddListenerEx(this.flyControl, decButton, ButtonEventType.Down, DecreaseSpeed);
        ViveInput.AddListenerEx(this.flyControl, accButton, ButtonEventType.Down, IncreaseSpeed);
        ViveInput.AddListenerEx(this.flyControl, reverseButton, ButtonEventType.Down, ToggleDirection);

        if (reverseGear == 1)
            Debug.Log("Vorwärtsgang eingeschaltet");
    }

    /// <summary>
    /// Die Callbacks wieder abhängen.
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(this.flyControl, decButton, ButtonEventType.Down, DecreaseSpeed);
        ViveInput.RemoveListenerEx(this.flyControl, accButton, ButtonEventType.Down, IncreaseSpeed);
        ViveInput.RemoveListenerEx(this.flyControl, reverseButton, ButtonEventType.Down, ToggleDirection);
    }

    /// <summary>
    /// Beschleunigen
    /// </summary>
    private void IncreaseSpeed()
    {
        speed -= this.deltaSpeed;
        Debug.Log("speed verkleinert auf " + this.speed);
    }

    /// <summary>
    /// Abbremsen
    /// </summary>
    private void DecreaseSpeed()
    {
        speed += this.deltaSpeed;
        Debug.Log("speed vergrößert auf " + this.speed);
    }


    /// <summary>
    /// Rückwärtsfahren
    /// </summary>
    private void ToggleDirection()
    {
        reverseGear = -reverseGear;

        if (reverseGear == 1)
            Debug.Log("Vorwärtsgang");
        else
            Debug.Log("Rückwärtsgang");

    }


    /// <summary>
    /// In Update abfragen, ob der Button gedrückt wurde
    /// und wir fliegen.
    /// </summary>
    void FixedUpdate()
    {

        if (ViveInput.GetPress(this.flyControl, this.flyButton))
            Move();
    }

    /// <summary>
    /// Wir fragen die Orientierung des Geräts ab, das wir für die
    /// Richtungsdefinition verwenden und übertragen diese Orientierung
    /// auf das von uns manipulierte GameObject (typischer Weise die Kamera).
    /// </summary>
    private void Move()
    {
        transform.Translate(reverseGear * speed * flyOrientation.transform.forward);
    }
}
