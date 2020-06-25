/// <summary>
/// Model 
/// </summary>
public class Model : Subject
{
    public Model()
    {
        Status = false;
    }
    public Model(bool s)
    {
        Status = s;

    }
    public void Toggle()
    {
        Status = !Status;
        Notify();
    }

    public bool Status
    {
        get { return _status; }
        set { _status = value; }
    }

    private bool _status { get; set; }
}
