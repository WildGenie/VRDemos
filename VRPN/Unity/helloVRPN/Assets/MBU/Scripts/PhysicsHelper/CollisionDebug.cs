//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Namespace für allgemeine Unity-Assets
/// </summary>
namespace VRKL.MBU
{
    /// <summary>
    /// Visualisierung von Kraftvektoren, die bei einer Kollision entstehen.
    /// 
    /// Diese Klasse gibt rote Vektoren aus falls eine Kollision mit dem
    /// angehängten Collider auftreten. Deshalb setzt diese Klasse auch eine
    /// Collider-Komponente voraus, was mit RequireComponent sicher gestellt wird.
    /// Die Länge des Strahls visualisiert den Betrag der auftretenden Kraft,
    /// die Richtung ist die inverse Richtung der Richtung, in der die Kollision
    /// auftrat.
    /// 
    /// Alle Punkte an denen eine Kollision auftritt werden auf der Konsole ausgegeben.
    /// Diese Klasse gibt nur etwas aus, wenn *kein* Trigger in der Kollision
    /// ausgelöst wird, da in diesem Fall keine "richtige" Kollision auftritt.
    /// <remarks> 
    /// Autor: Patrick Schwarz, Wintersemester 2014/15
    /// Projekt Studiengang Informatik
    /// 
    /// Änderungen: Manfred Brill, Sommersemester 2020
    /// </remarks>
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CollisionDebug : MonoBehaviour
    {
        /// <summary>
        /// Parameter für DrawRay, der angibt, wie lange 
        /// der Strahl ausgegeben werden soll,
        /// in Sekunden.
        /// 
        /// Ein Wert von 0.0f bedeutet, dass der Strahl
        /// einen Frame lang dargestellt wird.
        /// </summary>
        [Tooltip("Wie lange wird der Strahl ausgegeben (in Sekunden). Ein Wert von 0.0 bedeutet ein Frame lang.")]
        [Range(0.0f, 10.0f)]
        public float duration = 10.0f;
        /// <summary>
        /// Überschreiben von OnCollisionEnter
        /// 
        /// Wir geben eine Meldung auf der Konsole aus für jedes Objekt,
        /// mit dem eine Kollision auftritt und visualisieren den entstehenden
        /// Kraftvektor mit Hilfe eines Strahls.
        /// </summary>
        /// <remarks>
        /// Diese Funktion wird aufgerufen, wenn eine Kollision
        /// ohne Trigger auftritt. 
        /// </remarks>/// 
        /// <param name="collision">Daten der aufgetretenen Kollision</param>
        public void OnCollisionEnter(Collision collision)
        {
            // Log zusammensetzen - Grundbestandteil
            // Der Rest wird in der foreach-Schleife dazu gegeben.
            string log = name + " ist kollidiert mit " + collision.gameObject.name + ", v = " + collision.relativeVelocity + "\n";

            Debug.Log(">>> OnCollisionEnter");
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.red, this.duration, false);
                log += "\t\t" + contact.thisCollider.name + " hit " + contact.otherCollider.name + "\n";
            }
            Debug.Log(log);
            Debug.Log("<<< OnCollisionEnter");
        }
    }
}