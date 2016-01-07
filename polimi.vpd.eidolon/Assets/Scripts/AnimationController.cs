using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    public void StartColdAnimation()
    {
        animator.SetInteger("AnimControl", 1);
    }

    public void StartShowerAnimation()
    {
        animator.SetInteger("AnimControl", 2);
    }

    public void StartBeerAnimation()
    {
        animator.SetInteger("AnimControl", 3);
    }

    public void StopAnimation()
    {
        animator.SetInteger("AnimControl", 0);
    }
}
