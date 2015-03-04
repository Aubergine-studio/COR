using UnityEngine;
using System.Collections;

public abstract class Behavior : MonoBehaviour
{
    protected EnemyAI brain;

    public void Initialize(EnemyAI _brain)
    {
        this.brain = _brain;
    }

    public abstract void Behave();
}
