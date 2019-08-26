using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///Anwendung mit Esc-Taste beenden
/// </summary>
public class QuitApplication : MonoBehaviour {
	
	void Update ()
    {
        if (Input.GetKeyUp(key: KeyCode.Escape))
        {
            Application.Quit();
            // Esc is ignored in Editor playback mode
            #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
           #endif
        }
    }


}
