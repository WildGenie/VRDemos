using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EBeenden der Anwendung
/// </summary>
public class QuitApplication : MonoBehaviour {

	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
            // Esc wird im Editor playback ignoriert:
            #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
           #endif
        }
    }


}
