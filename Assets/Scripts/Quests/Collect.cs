using UnityEngine;
using System.Collections;

[System.Serializable]
public class Collect : Task
{
    public override void Progress(GameObject target)
    {
        if (target.name == this.target.name)
        {
            count--;
            Debug.Log(count);
        }
    }
}