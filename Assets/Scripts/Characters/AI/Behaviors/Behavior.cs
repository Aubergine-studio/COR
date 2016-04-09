using UnityEngine;
/// <summary>
/// Abstarkcyjna klasa zachowania.
/// </summary>
public abstract class Behavior : MonoBehaviour
{
    protected EnemyAI Brain;

    public void Initialize(EnemyAI brain)
    {
        Brain = brain;
    }

    /// <summary>
    /// Definicja zachowania.
    /// </summary>
    public abstract void Behave();
}
