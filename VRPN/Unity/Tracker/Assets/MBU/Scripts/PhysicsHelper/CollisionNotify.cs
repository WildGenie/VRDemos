//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Namespace für allgemeine Unity-Assets
/// </summary>
namespace VRKL.MBU
{
    /// <summary>
    /// Gibt alle Kollisionen des zugehörigen Colliders auf der Konsole aus.
    /// 
    /// Es werden sowohl Kollisionen mit und ohne Trigger ausgegeben.
    /// Die Klasse setzt eine Collider- und eine RigidBody-Komponente voraus, was mit
    /// RequireComponents sicher gestellt wird.
    /// <remarks>
    /// Autor: Patrick Schwarz, Wintersemester 2014/15
    /// Projekt Studiengang Informatik
    /// 
    /// Änderungen: Manfred Brill, Sommersemester 2020
    /// </remarks>
    /// </summary>
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class CollisionNotify : MonoBehaviour
    {
        /// <summary>
        /// Überschreiben von OnCollisionEnter
        /// 
        /// Diese Funktion wird aufgerufen, wenn eine Kollision
        /// ohne Trigger auftritt.
        /// Wir geben eine Meldung auf der Konsole aus.
        /// </summary>
        /// <remarks>
        /// Diese Funktion wird nur aufgerufen, wenn
        /// eine non-trigger Kollission aufgetreten ist!
        /// </remarks>/// 
        /// <param name="collision">Daten der aufgetretenen Kollision</param>
        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log(">>> OnCollisionEnter");
            Debug.Log("Kollision!\n Objekt ist kollidiert mit " + collision.gameObject.name);
            Debug.Log("<<< OnCollisionEnter");
        }

        /// <summary>
        /// Überschreiben von OnTriggerEnter
        /// 
        /// Diese Funktion wird aufgerufen, wenn eine Kollision
        /// mit Trigger auftritt.
        /// </summary>
        /// <param name="other">Daten der aufgetretenen Kollision</param>
        public void OnTriggerEnter(Collider other)
        {
            Debug.Log(">>> OnTriggerEnter");
            Debug.Log("Kollision mit Trigger!\n Eine Kollision mit " + other.gameObject.name + " ist aufgetreten!");
            Debug.Log("<<< OnTriggerEnter");
        }
    }
}
