//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Laufen (Walk) mit dem Vive Input Utility
/// 
/// Bei Fly können wir eine Richtung verwenden, die
/// alle drei Weltkoordinatenachsen verwenden.
/// Walk beschränkt die Bewegungsrichtung auf eine Richtung
/// mit konstantem y-Wert.
/// 
/// Wir verwenden den Controller, den wir bereits in der Basisklasse
/// auswählen können und berechnen die Orthogonalprojektion 
/// dieser Richtung in die Ebene y=0.
/// </summary>
public class Walk : Locomotion
{
     /// <summary>
    /// Wir fragen die Orientierung des Geräts ab, das wir für die
    /// Richtungsdefinition verwenden und übertragen diese Orientierung
    /// auf das von uns manipulierte GameObject (typischer Weise die Kamera).
    /// 
    /// Wir projizieren diese Richtung in die y=0 Ebene und normieren sie anschließend!
    /// </summary>
    protected override void Move()
    {
        Vector3 walkDir = new Vector3(flyOrientation.transform.forward.x, 0.0f, flyOrientation.transform.forward.z);
        walkDir.Normalize();
        transform.Translate(reverseGear * speed * walkDir);
    }
}
