using UnityEngine;

public abstract class AnimatorController : MonoBehaviour
{
    protected Animator Animator;
    protected Inputs Inputs;
    protected Character Chracter;

    // Use this for initialization
    protected void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        Inputs = gameObject.GetComponent<Inputs>();
        Chracter = gameObject.GetComponent<Character>();
    }

    protected abstract void ControlAnimator();

    // Update is called once per frame
    protected void LateUpdate()
    {
        ControlAnimator();
    }
}
