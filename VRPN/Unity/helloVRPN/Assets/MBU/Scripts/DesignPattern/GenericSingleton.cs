//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========

namespace VRKL.MBU
{
    /// <summary>
    /// Beispiel für die Implementierung eines Singleton
    /// in C# als Generic.
    /// 
    /// Wenn nie auf diese Klasse zugegriffen wird, dann wird
    /// die Instanz nie erzeugt.
    /// Wir verwenden "Eager Creation".
    /// Die Klasse kann auch als <code>static</code> deklariert
    /// werden. Das ist nützlich im Fall, dass wir die
    /// Singleton-Klasse nicht von einer weitere Basisklasse
    /// ableiten.
    /// <remarks>
    /// Quelle für die Implementierung des Patterns:
    /// https://wiki.byte-welt.net/wiki/Singleton_Beispiele_(Design_Pattern)#Eager_Creation.
    /// Dort findet man auch Varianten mit "Lazy Creation".
    /// </remarks>
    /// </summary>
    public class GenericSingleton<T>
    where T : new()
    {
        /// <summary>
        /// Variable für die Verwendung der Instanz.
        /// <remarks>
        /// Wir verwenden die Instanz mit
        /// <code>
        /// class MySingleton : GenericSingleton<MySingleton> {}
        /// </code>
        /// Der Zugriff auf die Instanz erfolgt dann über
        /// <code>
        /// MySingleton.Instance
        /// </code>
        /// </remarks>
        /// </summary>
        public static readonly T Instance = new T();
    }
}
