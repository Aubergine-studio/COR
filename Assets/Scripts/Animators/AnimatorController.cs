using UnityEngine;
using System.Collections;

public abstract class AnimatorController : MonoBehaviour
{
	protected Animator animator;
	protected Inputs inputs;

	// Use this for initialization
	protected void Start ()
	{
		animator = gameObject.GetComponent<Animator> ();
		inputs = gameObject.GetComponent<Inputs> ();
	}

	protected abstract void ControlAnimator ();

	// Update is called once per frame
	protected void Update ()
	{
		ControlAnimator ();
	}
}
