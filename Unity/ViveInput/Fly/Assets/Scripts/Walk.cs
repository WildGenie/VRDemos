//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Laufen (Walk) mit dem Vive Input Utility
/// 
/// Bei Fly können wir eine Richtung verwenden, die
/// alle drei Weltkoordinatenachsen enthält.
/// Walk beschränkt die Bewegungsrichtung auf eine Richtung
/// mit konstantem y-Wert.
/// 
/// Wir verwenden den Controller, den wir bereits in der Basisklasse
/// auswählen können und berechnen die Orthogonalprojektion 
/// dieser Richtung in die Ebene y=0.
/// </summary>
public class Walk : SingleDirection
{
    /// <summary>
    /// Wir fragen den Vektor forward des Koordinatensystems des Geräts ab, das wir für die
    /// Richtungsdefinition verwenden und definieren damit die Bewegungsrichtung.
    /// <remarks>
    /// Wir projizieren diese Richtung in die y=0 Ebene und normieren sie anschließend!
    /// </remarks>
    /// </summary>
    protected override void ComputeMovingDirection()
    {
        movingDirection = new Vector3(orientationObject.transform.forward.x, 0.0f, orientationObject.transform.forward.z);
        movingDirection.Normalize();
    }
}
