//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Abstrakte Basisklasse für Locomotion in VR-Anwendungen
/// </summary>
public abstract class Locomotion : MonoBehaviour
{
    [Header("Geräte und Buttons")]
    /// <summary>
    /// Welches GameObject verwenden wir für die Definition der Richtung?
    /// 
    /// Sinnvoll ist einer der beiden Controller, aber auch andere
    /// GameObjects (wie der Kopf) können verwendet werden.
    /// </summary>
    [Tooltip("Welches GameObject verwenden wir für die Definition der Bewegungsrichtung?")]
    public GameObject flyOrientation;
    /// <summary>
    /// Welchen Controller verwenden wir für die Fortbewegung?
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

    /// <summary>
    /// Wir können die Bewegungsrichtung um 180 Grad drehen - einen Rückwärtsgang einlegen.
    /// 
    /// Default ist "Menu"
    /// </summary>
    [Tooltip("Welchen Button verwenden wir für den Rückwärtsgang?")]
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
    [Header("Parameter für die Locomotion")]
    [Tooltip("Faktor für die Geschwindigkeit der Bewegung")]
    [Range(0.001f, 0.2f)]
    public float speed = 0.005f;

    /// <summary>
    /// Delta für Beschleunigen und Abbremsen
    /// </summary>
    protected float deltaSpeed = 0.001f;

    /// <summary>
    /// Konstante, die entscheidet, ob wir uns vorwärts oder rückwärts bewegen.
    /// </summary>
    protected int reverseGear = 1;

    /// <summary>
    /// Die Callbacks für Rückwärtsgang, Beschleunigung und Abbremsen registrieren.
    /// </summary>
    protected void Awake()
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
    protected void OnDestroy()
    {
        ViveInput.RemoveListenerEx(this.flyControl, decButton, ButtonEventType.Down, DecreaseSpeed);
        ViveInput.RemoveListenerEx(this.flyControl, accButton, ButtonEventType.Down, IncreaseSpeed);
        ViveInput.RemoveListenerEx(this.flyControl, reverseButton, ButtonEventType.Down, ToggleDirection);
    }

    /// <summary>
    /// Beschleunigen
    /// </summary>
    protected void IncreaseSpeed()
    {
        speed -= this.deltaSpeed;
    }

    /// <summary>
    /// Abbremsen
    /// </summary>
    protected void DecreaseSpeed()
    {
        speed += this.deltaSpeed;
    }


    /// <summary>
    /// Rückwärtsfahren
    /// </summary>
    protected void ToggleDirection()
    {
        reverseGear = -reverseGear;
    }


    /// <summary>
    /// In Update abfragen, ob der Button gedrückt wurde
    /// und wir fliegen.
    /// </summary>
    protected void Update()
    {
        if (ViveInput.GetPress(this.flyControl, this.flyButton))
            Move();
    }

    /// <summary>
    /// In dieser Funktion implementieren die abgeleiteten Klassen
    /// wie mit Hilfe einer Bewegungsrichtung und einer Bahngeschwindigkeit
    /// die Fortbewegung durchgeführt wird.
    /// </summary>
    protected abstract void Move();

}
