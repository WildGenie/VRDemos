//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========

using System.Collections.Generic;

namespace VRKL.MBU
{
    /// <summary>
    /// Basis-Klasse für ein Subject 
    /// im Observer-Pattern.
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// wir benachrichtigen alle registrierten
        /// Observer, dass sich das beobachtete Objekt verändert hat.
        /// </summary>
        public virtual void Notify()
        {
            foreach (Observer obs in observers)
                obs.Refresh();
        }

        /// <summary>
        /// Einen weiteren Observer hinzufügen
        /// </summary>
        /// <param name="o">Observer, der hinzugefügt werden soll</param>
        public virtual void Attach(Observer o)
        {
            observers.Add(o);
        }

        /// <summary>
        /// Einen Observer entfernen
        /// </summary>
        /// <param name="o">Observer, der entfernt werden soll</param>
        public virtual void Detach(Observer o)
        {
            observers.Remove(o);
        }

        /// <summary>
        /// Default-Konstruktor
        /// </summary
        protected Subject()
        {
            observers = new List<Observer>();
        }

        /// <summary>
        /// Liste der registrierten Observer Klassen
        /// </summary>
        protected readonly List<Observer> observers;
    }
}
