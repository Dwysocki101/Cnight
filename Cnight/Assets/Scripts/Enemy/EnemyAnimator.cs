using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    protected Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Called when enemy successfully blocks attack. Play the 'CounterAttack01' (hardcoded) animation.
    public void PlayCounterAnimation()
    {
        animator.Play("CounterAttack01");
    }
}
