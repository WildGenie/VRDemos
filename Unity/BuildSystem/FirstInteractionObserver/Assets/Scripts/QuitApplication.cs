using UnityEngine;

/// <summary>
/// Beenden der Anwendung mit der ESC-Taste
/// </summary>
public class QuitApplication : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
            // Im Editor müssen wir isPlaying auf false setzen
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
