using UnityEngine;

public class Quest : MonoBehaviour
{
    private Task[] tasks;
    // Use this for initialization
    void Start()
    {
        tasks = GetComponents<Task>();
    }

    // Update is called once per frame
    public void UpdateQuest(GameObject target)
    {
        foreach (Task t in tasks)
        {
            t.Progress(target);
        }
    }

}
