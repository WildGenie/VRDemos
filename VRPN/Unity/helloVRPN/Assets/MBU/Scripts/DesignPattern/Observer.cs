//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

namespace VRKL.MBU
{
    /// <summary>
    /// Abstrakte Basis-Klasse für einen Observer
    /// im Observer-Pattern
    /// <remarks>
    /// In Gamma enthält die Basisklasse für den Observer
    /// eine Funktion "Update". Da wir unsere Implementierung
    /// in Unity verwenden werden ändern wir diesen Bezeichner in
    /// <code>Refresh</code>, um Verwechslungen zu vermeiden.
    /// </remarks>
    /// </summary>
    public abstract class Observer : MonoBehaviour
    {
        /// <summary>
        /// Wir führen ein Update für den Observer durch.
        /// </summary>
        public abstract void Refresh();
    }
}
