using UnityEngine;

/// <summary>
/// Sztuczna inteligencji.
/// Na bazie zachowani podpiętych do postaci w odpowiednio manipuluje klasą wejść.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    private Character _mySlave;
    public Character Slave
    {
        get { return _mySlave; }
    }
    private Inputs _inputs;
    public Inputs inputs
    {
        get { return _inputs; }
    }
    public Transform ObstacleDetector;
    public float DetectorRadius = 0.3f;
    public float Sight = 10;

    private Behavior[] _behaviors;

    void Start()
    {
        _inputs = GetComponentInParent<Inputs>();
        inputs.HorizontalInputLeft = 0f;
        _mySlave = GetComponentInParent<Enemy>();

        _behaviors = GetComponents<Behavior>();
        foreach (Behavior b in _behaviors)
        {
            b.Initialize(this);
        }
    }

    void Update()
    {
        foreach (Behavior b in _behaviors)
        {
            b.Behave();
        }
    }
}
