/// <summary>
/// Controller 
/// </summary>
public class Controller
{
    public Controller(Model m)
    {
        Mod = m;
    }

    public Model Mod { get; set; }

    /// <summary>
    /// Highlight aktivieren
    /// </summary>
    public void ToggleHighlight()
    {
        Mod.status = !Mod.status;
    }
}