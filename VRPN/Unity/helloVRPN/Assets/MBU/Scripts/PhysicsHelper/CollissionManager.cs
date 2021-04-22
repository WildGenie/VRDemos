using UnityEngine;

/// <summary>
/// Klasse, die auf der Konsole ausgibt, dass ein Kollisions-Event
/// mit dem GameObject stattgefunden hat, zu dem diese Klasse
/// hinzugefügt ist.
/// 
/// Voraussetzung: Diese GameObject hat *nicht* die Eigenschaft "isTrigger"!
/// </summary>
namespace VRKL.MBU
{
    public class CollissionManager : MonoBehaviour
    {
        /// <summary>
        /// Kollision hat begonnen
        /// </summary>
        /// <param name="collision">Daten der Kollision wie Name des
        /// kollidierenden Objekts und weitere Informationen</param>
        void OnCollisionEnter(Collision Coll)
        {
            Debug.Log(">>> OnCollisionEnter");
            Debug.Log("Eine Kollision mit  " + Coll.collider.gameObject.name + " hat begonnen!");
            Debug.Log("<<< OnCollisionEnter");
        }

        /// <summary>
        /// Kollision ist beendet
        /// </summary>
        /// <param name="collision">Daten der Kollision wie Name des
        /// kollidierenden Objekts und weitere Informationen</param>
        void OnCollisionExit(Collision Coll)
        {
            Debug.Log(">>> OnCollisionExit");
            Debug.Log("Ende der Kollision mit " + Coll.collider.gameObject.name + " ist beendet!");
            Debug.Log(">>> OnCollisionExit");
        }
    }
}
