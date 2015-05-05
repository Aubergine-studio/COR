using UnityEngine;

public class Kill : Task
{
    private void Update()
    {
        if (count == 0) isDone = true;
    }

    public override void Progress(GameObject target)
    {
        if (target.name == this.target.name)
        {
            count--;
            Debug.Log(count);
        }
    }
}