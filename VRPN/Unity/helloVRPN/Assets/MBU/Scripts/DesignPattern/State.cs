//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========

namespace VRKL.MBU
{
    /// <summary>
    /// Abstrakte Basisklasse für eine State Machine.
    /// 
    /// Der Zustandsautomat ist so implementiert, dass
    /// die einzelnen Zustände, die von dieser Basisklasse
    /// abgeleitet werden selbst entscheiden,
    /// in welchen zukünftigen Zustand sie wechseln.
    /// Klassen, die den Zustandsautomaten verwenden 
    /// rufen die Funktion <code>OnStateUpdate</code>
    /// auf. Dort wird in einer abgeleiteten State-Klasse
    /// entschieden, ob der Zustand verlassen wird
    /// und welcher Zustand jetzt angenommen wird.
    /// 
    /// Von dieser Basisklasse abgeleitete Klassen
    /// implementieren das Singleton Pattern!
    /// </summary>
    public abstract class State
    {
        protected State() { }

        /// <summary>
        /// Wird während des Eintretens in einen State aufgerufen
        /// </summary>
        public abstract void OnStateEntered();

        /// <summary>
        /// Wird aufgerufen während der State aktiv ist
        /// </summary>
        public abstract void OnStateUpdate();

        /// <summary>
        /// Wird während des Verlassens eines States aufgerufen
        /// </summary>
        public abstract void OnStateQuit();

        /// <summary>
        /// Sollen die Zustandsklassen eine Ausgabe auf der Konsole durchführen?
        /// </summary>
        public bool DebugOutput
        {
            get { return _output; }
            set { _output = value; }
        }
        protected bool _output { get; set; } = false;
    }
}
