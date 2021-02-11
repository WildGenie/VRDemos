using UnityEngine;

/// <summary>
/// Klasse mit Funktionen für die Reaktion auf den CollisionTrigger.
/// 
/// Wir implementieren Funktionen wie OnTriggerEnter und OnTriggerExit.
/// Diese Funktionen erhalten die Information über das Objekt, mit
/// dem die Kollision stattgefunden hat.
/// </summary>
namespace VRKL.MBU
{
    public class TriggerManager : MonoBehaviour
    {
        /// <summary>
        /// Das GameObject ist mit einem weiteren Objekt in der Szene kollidiert.
        /// Die Kollision hat gerade begonnen.
        /// </summary>
        /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
        void OnTriggerEnter(Collider OtherObject)
        {
            Debug.Log(">>> OnTriggerEnter");
            Debug.Log("Das GameObject " + gameObject.name + " ist kollidiert!");
            Debug.Log("Es gab eine Kollision mit " + OtherObject.name + " ");
            Debug.Log("<<< OnTriggerEnter");
        }

        /// <summary>
        /// Das GameObject ist mit einem weiteren Objekt in der Szene kollidiert.
        /// Die Kollision ist beendet.
        /// </summary>
        /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
        void OnTriggerExit(Collider OtherObject)
        {
            Debug.Log(">>> OnTriggerExit");
            Debug.Log("Die Kollision des GameObjects " + gameObject.name + " ist beendet!");
            Debug.Log("Kollision erfolgte mit " + OtherObject.name + " und ist beendet!");
            Debug.Log(">>> OnTriggerExit");
        }
    }
}
