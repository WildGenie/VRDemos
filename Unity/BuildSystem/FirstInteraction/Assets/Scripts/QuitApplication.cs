using UnityEngine;

/// <summary>
/// Beenden auf dem Desktop mit ESC
/// </summary>
public class QuitApplication : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
            // Im Editor verwenden wir isPlaying=false
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
