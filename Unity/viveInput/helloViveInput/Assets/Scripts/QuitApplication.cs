using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Erstes Beispiel einer C# Klasse in einer Unity-Anwendung
/// </summary>
public class QuitApplication : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Vive Input verwendet ESC, um die Simulation zu stoppen.
        // Deshalb verwenden wir hier die Taste Q wie "Quit".
        // Eventuell ist es möglich, Q nur auszuwerten, wenn bereits
        // ESC betätigt ist. Frage also: können wir den Status des
        // Simulators abfragen?
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Application.Quit();
            // Esc is ignored in Editor playback mode
            #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
           #endif
        }
    }


}
