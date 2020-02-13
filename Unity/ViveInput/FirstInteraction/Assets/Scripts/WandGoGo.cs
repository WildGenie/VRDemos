using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Gogo für einen Wand
/// 
/// IWelche Hand hier verwendet wird hängt davon ab,
/// welchem der Controller dieses Script hinzugefügt wird.
/// 
/// Wir suchen den Kopf mit Hilfe des Tags "MainCamera".
/// </summary>
public class WandGoGo : MonoBehaviour
{
    /// <summary>
    /// Wir verwenden ein Objekt, um die unskalierte Position 
    /// des Controllers zu visualisieren.
    /// 
    /// In der Literatur wird dafür ein Würfel verwendet.
    /// </summary>
    public GameObject replacer;

    /// <summary>
    /// Als Ursprung der Gogo-Skalierung verwenden wir
    /// den Kopf, den wir im VIU Rig mit Hilfe des Tags
    /// MainCamera
    /// suchen (in Awake).
    /// </summary>
    private GameObject headNode;

    // Positionsveränderung aktivieren, sonst
    // macht uns der Simulator alles kaputt
    private bool startGogo;

    /// <summary>
    /// Wann startet die gogo-Technik?
    /// 
    /// In der Literatur ist dies der Parameter D.
    /// </summary>
    public float DD = 0.9f;

    /// <summary>
    /// Der maximale Abstand zwischen Kopf und virtueller Hand in der Szene
    /// 
    /// Wir weit möchten wir die virtuelle Hand vom Körper bewegen?
    /// In der Literatur ist die die Zahl f_max.
    /// </summary>
    public float fMax = 2.0f;

    /// <summary>
    /// Wie groß ist die maximale Distanz des Users vom Kopf?
    /// 
    /// In der Literatur ist dieser Parameter r_max.
    /// Die Zahlen die man hier einsetzen kann hängen vom User ab.
    /// Es erscheint sinnvoll hier Werte aus der DIN 33 402 
    /// als Grenzen zu verwenden. 
    /// Für das 95%-Quantil kann man einen maximalen
    /// Wert von ung. 65 cm angeben, der hier verwendet wird.
    /// </summary>
    public float rMax = 1.2f;

    /// <summary>
    /// Möchten wir Log-Ausgaben?
    /// </summary>
    public bool logOutput = false;

    /// <summary>
    /// Parameter für die gogo-Funktion.
    /// 
    /// In der Literatur ist dies der Parameter alpha.
    /// </summary>
    private float alphaScale;

    /// <summary>
    /// Wir bereiten die Go-Go Technik und die Szene vor.
    /// </summary>
    private void Awake()
    {
        // Kamera als Ursprung unseres Koordinatensystems finden
        headNode = GameObject.FindGameObjectWithTag("MainCamera");

        // Den Renderer des Ersatzobjekts deaktivieren
        Renderer rend = replacer.GetComponent<Renderer>();
        rend.enabled = false;

        if (logOutput)
        {
            Debug.Log(">>>>> Awake");
            if (headNode != null)
            {
                Debug.Log("Kamera gefunden!");
            }
            else
            {
                Debug.Log("Kamera nicht gefunden");
            }

            Debug.Log("Schwellwert D:");
            Debug.Log(DD);
            Debug.Log("Maximale Entfernung Kopf zu Hand:");
            Debug.Log(rMax);
            Debug.Log("Maximale Reichweite virtuelle Hand:");
            Debug.Log(fMax);
        }

        // alphaScale aus den Parametern berechnen.
        ComputeAlphaScale();

        // Gogo erst bei Bedarf anschalten für den Simulator
        startGogo = false;  
    }

    private void Update()
    {
        // Gogo aktivieren
        if (Input.GetKeyDown(KeyCode.F12))
        {
            startGogo = !startGogo;
            /*Debug.Log("Gogo gestartet");
            Debug.Log("Position des Objekts:");
            Debug.Log(transform.position.ToString());*/
            if (startGogo)
            {
                replacer.transform.position = transform.position;
                replacer.transform.rotation = transform.rotation;
                replacer.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                replacer.GetComponent<Renderer>().enabled = false;
            }
        }

        // Hier berechnen wir ständig r, den Abstand zwischen Kopf und Hand,
        // falls Go-Go aktiviert ist und berechnen damit die
        // neue Position des Objekts.
        if (startGogo)
            UpdateGogo();
    }

    /// <summary>
    /// Anwendung der Go-Go-Technik auf das angehängte Objekt
    /// 
    /// In der Literatur wird ein "ego-zentrisches" Koordinatensystem
    /// beschrieben. Die Abbildung im Paper zeigt dabei einen Ursprung
    /// dieses Koordinatensystems in der Nähe des Brustbeins.
    /// Die Handposition wird in mit diesem Ursprung und Kugelkoordinaten beschrieben.
    /// 
    /// Für die Vive tracken wir den Kopf und die Hand, so dass wir
    /// hier als Ursprung des Koordinatensystems den Kopf verwenden.
    /// Im Gegensatz zur Literatur verwenden wir kein Kugelkoordinatensystem,
    /// sondern erzeugen aus Kopf und Hand einen Strahl. Die Variable r
    /// aus der Literatur wird so zum Parameterwert für den Strahl 
    /// (dabei verwenden wir, dass der Strahl einen normierten Richtungsvektor
    /// hat). Wir berechnen den skalierten Parameterwert und setzen das
    /// gesteuerte Objekt entsprechend auf dem Strahl.
    /// </summary>
    private void UpdateGogo()
    {
        // Kopf- und Handposition abfragen, Richtungsvektor daraus bauen
        Vector3 headPosition = headNode.transform.position;
        // Wir müssen die unskalierte Position abfragen!
        // Wir probieren das mal mit parent.
        // Vermutlich müssen wir dann auch die localPosition
        // verändern. Das würde vermutlich bedeutetn,
        // dass wir, falls sich der Skalierungsparameter nicht
        // verändert, nichts draufaddieren, und
        // falls die Skalierung greift, dieser Anteil auf die
        // localPosition gehen. Eben relativ zu parent.
        Vector3 handPosition = transform.parent.position;

        replacer.transform.position = handPosition;

        Vector3 direction = handPosition - headPosition;
        // Der Parameterwert für die aktuelle Position des Controllers
        float lambda = direction.magnitude;
        if (logOutput)
        {
            Debug.Log(">>> UpdateGogo");
            Debug.Log("Kopfposition:");
            Debug.Log(headNode.transform.position.ToString());
            Debug.Log("Handposition:");
            Debug.Log(handPosition.ToString());
            Debug.Log("Richtungsvektor für den Ray");
            Debug.Log(direction.ToString());
            Debug.Log("Abstand zwischen Kopf und Controller:");
            Debug.Log(lambda);
        }

        // Strahl erzeugen, auf dem das Objekt liegt
        // Der Konstruktor normiert den Richtungsvektor!
        Ray ray = new Ray(headPosition, direction);

        lambda = gogoScale(lambda);

        if (logOutput)
        {
            //Debug.Log("Skalierter Parameterwert:");
            //Debug.Log(lambda);
            if (lambda >= fMax)
                Debug.Log("Maximale Skalierung ist erreicht!");
        }
        // Wir verwenden den Parameterwert und den Strahl
        // für die neue Position des Objekts
        transform.localPosition = ray.GetPoint(lambda);

        if (logOutput)
        {
            Debug.Log("Neue Position des Controllers");
            Debug.Log(transform.position.ToString());
            Debug.Log(" <<<<< UpdateGogo");
        }
    }



    /// <summary>
    /// Übertragung der Skalierungsfunktion aus der Literatur
    /// 
    /// Wir verwenden hier keine Kugelkoordinaten, sondern
    /// verwenden die folgende Funktion:
    /// 
    /// Ist die Variable r kleiner als die Konstante DD,
    /// dann bleibt das Objekt da wo es ist.
    /// 
    /// Im anderen Fall geben wir den Term alpha*(r-D)^2
    /// aus der Literatur zurück. 
    /// </summary>
    /// <param name="r">Aktueller Abstand zwischen headNode und handNode</param>
    /// <returns>Skalierter Parameterwert</returns>
    private float gogoScale(float r)
    {
        // Für Werte von r größer als rMax verwenden
        // den maximalen Wert, fMax.
        float diff;
        if (logOutput)
        {
            Debug.Log(">>> gogoScale");
            if (r > rMax)
            {
                Debug.Log("r ist außerhalb des maximalen Bereichs");
                Debug.Log("r wird auf rMax gesetzt");
            }
        }
        r = Mathf.Clamp(r, 0.0f, rMax);
        if (logOutput)
        {
            Debug.Log("Der Wert für r vor dem Skalieren:");
            Debug.Log(r);
        }
        diff = r - DD;
        if (logOutput)
        {
            Debug.Log("Der zurückgebene Wert für r > D");
            Debug.Log(r + alphaScale * diff * diff);
        }
        /*if (logOutput)
        {
            Debug.Log("Wert der Differenz r-D:");
            Debug.Log(diff);
        }*/
        if (diff <= 0.0f)
            return r;
        else
            return r + alphaScale * diff * diff;
    }

    /// <summary>
    /// Den Skalierungsfaktor Alpha in der Gogo-Funktion berechnen.
    /// 
    /// Wir können im Inspektor die Werte für rMax und fMax angeben,
    /// hieraus können wir den Wert für Alpha berechnen.
    /// </summary>
    private void ComputeAlphaScale()
    {
        alphaScale = (fMax - rMax) / ((rMax - DD) * (rMax - DD));
        if (logOutput)
        {
            Debug.Log(">>> ComputeAlphaScale");
            Debug.Log("Wert für alpha:");
            Debug.Log(alphaScale);
        }
    }
}
