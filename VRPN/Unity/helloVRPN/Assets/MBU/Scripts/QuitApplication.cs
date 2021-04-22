//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Namespace für allgemeine Unity-Assets
/// </summary>
namespace VRKL.MBU
{
    /// <summary>
    /// Die Anwendung mit der ESC-Taste beenden.
    /// <remarks>
    /// Wir verwenden ESC sowohl im Editor
    /// als auch ein einem Build.
    /// </remarks>
    /// </summary>
    public class QuitApplication : MonoBehaviour
    {
        /// <summary>
        /// Die Taste mit dem Input-Manager abfragen.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            }
        }
    }
}
