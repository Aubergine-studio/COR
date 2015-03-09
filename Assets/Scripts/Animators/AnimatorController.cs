using UnityEngine;
using System.Collections;

public abstract class AnimatorController : MonoBehaviour
{
    protected Animator animator;
    protected Inputs inputs;
    protected Character chracter;

    // Use this for initialization
    protected void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        inputs = gameObject.GetComponent<Inputs>();
        chracter = gameObject.GetComponent<Character>();
    }

    protected abstract void ControlAnimator();

    // Update is called once per frame
    protected void LateUpdate()
    {
        ControlAnimator();
    }
}
