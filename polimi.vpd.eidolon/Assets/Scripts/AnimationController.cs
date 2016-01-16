using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    public void StartWalk()
    {
        animator.SetInteger("Anim", 1);
    }

    public void PickUpDress ()
    {
        animator.SetInteger("Anim", 2);
    }

    public void PickUpBeer()
    {
        animator.SetInteger("Anim", 3);
    }

    public void StandStill()
    {
        animator.SetInteger("Anim", 4);
    }

    public void StopAnimation()
    {
        animator.SetInteger("Anim", 0);
    }
}
