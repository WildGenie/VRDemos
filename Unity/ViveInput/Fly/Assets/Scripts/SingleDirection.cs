//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Abstrakte Basisklasse für kontinuierliche Fortbewegung in immersiven Anwendungen,
/// abgeleitet von ContinousMovement.
/// 
/// Abgeleitete Klassen verwenden die Orientierung eines einzelnen GameObjects
/// in Unity für die Definition der Bewegungsrichtung.
/// </summary>
public abstract class SingleDirection : ContinousMovement
{
    [Header("Definition der Bewegungsrichtung")]
    /// <summary>
    /// Welches GameObject verwenden wir für die Definition der Richtung?
    /// 
    /// Sinnvoll ist einer der beiden Controller, aber auch andere
    /// GameObjects (wie der Kopf oder ein Vive Tracker) können verwendet werden.
    /// </summary>

    [Tooltip("GameObject, das die Bewegungsrichtung definiert")]
    public GameObject orientationObject;
}

