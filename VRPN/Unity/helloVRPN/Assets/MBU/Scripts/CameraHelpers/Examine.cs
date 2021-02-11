//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

namespace VRKL.MBU
{
    /// <summary>
    /// Realisierung der Examine-Metapher 
    /// 
    /// Die C#-Klasse verwendet die Renderer-Komponente
    /// des untersuchten GameObjects. Dies wird durch
    /// <code>RequireComponent</code> sicher gestellt.
    /// <remarks>
    /// Mehr zur Examine-Metapher findet man in
    /// Michael Bender, Manfred Brill: "Computergrafik",
    /// Hanser Verlag, 2005.
    /// </remarks>
    /// </summary>
    [AddComponentMenu("MBU/Camera/Examine")]
    [RequireComponent(typeof(Camera))]
    public class Examine : MonoBehaviour
    {
        /// <summary>
        /// Geschwindigkeit für die Bewegung der Kamera
        /// </summary>
        [Tooltip("Radius: Abstand vom Ursprung")]
        [Range(1.0f, 10.0f)]
        public float radius = 10.0f;

        [Tooltip("Elevation: Winkel in Gradmaß")]
        [Range(0.0f, 180.0f)]
        public float elevation = 45.0f;
        [Tooltip("Azimuth: Winkel in Gradmaß")]
        [Range(-180.0f, 180.0f)]
        public float azimuth = 0.0f;

        /// <summary>
        /// Geschwindigkeit für die Bewegung der Kamera
        /// </summary>
        [Tooltip("Schrittweite für die Veränderung der Winkel")]
        [Range(1.0f, 30.0f)]
        public float angleDelta = 10.0f;

        /// <summary>
        /// Multiplikator für die Mausbewegung
        /// </summary>
        [Tooltip("Multiplikator für die Mausbewegung")]
        [Range(1.0f, 100.0f)]
        public float mouseSensitivity = 10.0f;

        /// <summary>
        /// Ursprung als Target für die Funktion Lookat
        /// für die Orientierung der Kamera.
        /// </summary>
        private Vector3 targetForLookAt = new Vector3(0.0f, 0.0f, 0.0f);
        /// <summary>
        /// Axis für das Input-System von Unity
        /// </summary>
        private const string HorizontalMovementAxis = "Horizontal";
        /// <summary>
        /// Axis für das Input-System von Unity
        /// </summary>
        private const string VerticalMovementAxis = "Vertical";
        /// <summary>
        /// Axis für das Input-System von Unity
        /// </summary>
        private const string HorizontalRotationAxis = "Mouse X";
        /// <summary>
        /// Axis für das Input-System von Unity
        /// </summary>
        private const string VerticalRotationAxis = "Mouse Y";

        /// <summary>
        /// Setup
        /// 
        /// Kamera mit Hilfe der Kugelkoordinaten setzen.
        /// </summary>
        public void Start()
        {
            Vector3 pos;

            pos.x = radius * Mathf.Sin(azimuth) * Mathf.Cos(elevation);
            pos.y = radius * Mathf.Sin(azimuth) * Mathf.Sin(elevation);
            pos.z = radius * Mathf.Cos(azimuth);

            transform.position = pos;
            transform.LookAt(targetForLookAt);
        }

    }
}
