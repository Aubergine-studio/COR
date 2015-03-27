using UnityEngine;
using System.Collections;

public enum TaskType
{
    none = 0,
    collect  = 1,
    kill = 2,
    talk = 3,
}

[System.Serializable]
public abstract class Task : MonoBehaviour
{
    public bool isDone = false;
    public GameObject target;
    public int count;

    public abstract void Progress(GameObject target);
}


    