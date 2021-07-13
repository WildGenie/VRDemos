//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Fliegen 
/// 
/// Als Bewegungsrichtung verwenden wir die Orientierung
/// des Controllers, der durch die Basisklasse Locomotion festgelegt ist.
/// </summary>
public class Fly : Locomotion
{

    /// <summary>
    /// Wir fragen die Orientierung des Geräts ab, das wir für die
    /// Richtungsdefinition verwenden und übertragen diese Orientierung
    /// auf das von uns manipulierte GameObject (typischer Weise die Kamera).
    /// </summary>
    protected override void Move()
    {
        transform.Translate(reverseGear * speed * flyOrientation.transform.forward);
    }
}
