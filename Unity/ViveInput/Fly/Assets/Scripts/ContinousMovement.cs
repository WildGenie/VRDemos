//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Abstrakte Basisklasse für kontinuierliche Fortbewegung in immersiven Anwendungen
/// </summary>
public abstract class ContinousMovement : MonoBehaviour
{
    [Header("Trigger Devices")]
    /// <summary>
    /// Welchen Controller verwenden wir für das Triggern der Fortbewegung?
    /// 
    /// Als Default verwenden wir den Controller in der rechten Hand,
    /// also "RightHand" im "ViveCameraRig".
    /// </summary>
    [Tooltip("Rechter oder linker Controller für die Taste?")]
    public HandRole moveHand = HandRole.RightHand;
    /// <summary>
    /// Der verwendete Button, der die Bewegung auslöst, kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist "Trigger"
    /// </summary>
    [Tooltip("Welcher Button verwenden?\n Wir bewegen uns so lange der Button gedrückt ist.")]
    public ControllerButton moveButton = ControllerButton.Trigger;

    [Header("Bahngeschwindigkeit")]
    /// <summary>
    /// Maximal mögliche Geschwindigkeit
    /// </summary>
    [Tooltip("Maximal mögliche Bahngeschwindigkeit")]
    [Range(0.001f, 1.0f)]
    public float vMax = 0.2f;
    /// <summary>
    /// Button auf dem Controller für das Abbremsen der Fortbewegung.
    /// 
    /// Default ist "Pad"
    /// </summary>
    [Tooltip("Button für das Abbremsen")]
    public ControllerButton decButton = ControllerButton.Pad;
    /// <summary>
    /// Button auf dem Controller für das Beschleunigen der Fortbewegung.
    /// 
    /// Default ist "Grip"
    /// </summary>
    [Tooltip("Button für das Beschleunigen")]
    public ControllerButton accButton = ControllerButton.Grip;

    /// <summary>
    /// Bewegungsrichtung als Instanz von Vector3.
    /// 
    /// <remarks>
    /// Dieser Vektor hat immer euklidische Länge 1.
    /// </remarks>
    /// </summary>
    protected Vector3 movingDirection = new Vector3(0.0f, 0.0f, 0.0f);
    /// <summary>
    /// Delta für Beschleunigen und Abbremsen
    /// </summary>
    protected float deltaV = 0.001f;
    /// <summary>
    /// Variable, die die Geschwindigkeit der Fortbewegung steuert.
    /// </summary>
    protected float v = 0.005f;

    /// <summary>
    /// Die Callbacks für Rückwärtsgang, Beschleunigung und Abbremsen registrieren.
    /// </summary>
    protected void Awake()
    {
        ViveInput.AddListenerEx(moveHand, decButton, ButtonEventType.Down, DecreaseSpeed);
        ViveInput.AddListenerEx(moveHand, accButton, ButtonEventType.Down, IncreaseSpeed);
    }

    /// <summary>
    /// Die Callbacks wieder abhängen.
    /// </summary>
    protected void OnDestroy()
    {
        ViveInput.RemoveListenerEx(moveHand, decButton, ButtonEventType.Down, DecreaseSpeed);
        ViveInput.RemoveListenerEx(moveHand, accButton, ButtonEventType.Down, IncreaseSpeed);
    }

    /// <summary>
    /// Beschleunigen
    /// </summary>
    protected void IncreaseSpeed()
    {
        v = Mathf.Clamp(v + deltaV, 0.0f, vMax);
    }

    /// <summary>
    /// Abbremsen
    /// </summary>
    protected void DecreaseSpeed()
    {
        v = Mathf.Clamp(v - deltaV, 0.0f, vMax);
    }

    /// <summary>
    /// In dieser Funktion implementieren die abgeleiteten Klassen
    /// die Bewegungsrichtung.
    /// 
    /// <remarks>
    /// Ergebnis dieser Funktion ist immer eine Instanz von Vector3
    /// mit euklidischer Länge 1.
    /// </remarks>
    /// </summary>
    protected abstract void ComputeMovingDirection();

    /// <summary>
    /// In Update abfragen, ob der Button gedrückt wurde
    /// und wir fliegen.
    /// </summary>
    protected void Update()
    {
        if (ViveInput.GetPress(moveHand, moveButton))
        {
            ComputeMovingDirection();
            Move();
        }
    }

    /// <summary>
    /// In dieser Funktion implementieren die abgeleiteten Klassen
    /// wie mit Hilfe einer Bewegungsrichtung und einer Bahngeschwindigkeit
    /// die Fortbewegung durchgeführt wird.
    /// </summary>
    protected void Move()
    {
        transform.Translate(v * movingDirection);
    }

}
