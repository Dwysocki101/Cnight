using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{

    public AnimationClip replaceableAttackAnim;
    protected AnimationClip[] currentAttackAnimSet;
    const float locomotionAnimatorSmoothTime = .1f;

    protected Animator animator;
    public bool wasAttacking;// we need this so we can take lock the direction we are facing during attacks, mecanim sometimes moves past the target which would flip the character around wildly
    public bool rightButtonDown = false;    //we use this to "skip out" of consecutive right mouse down...

    // Use this for initialization
    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetMouseButton(1))// are we using the right button?
        {
            Debug.Log("right click");
            if (rightButtonDown != true)// was it previously down? if so we are already using "USE" bailout (we don't want to repeat attacks 800 times a second...just once per press please
            {
                animator.SetTrigger("Attack01");//tell mecanim to do the attack animation(trigger)
                rightButtonDown = true;//right button was not down before, mark it as down so we don't attack 800 frames a second 
                wasAttacking = true;//some mecanims will actually move us past the target, so we want to keep looking in one direction instead of spinning wildly around the target
            }
        }

        if (Input.GetMouseButtonUp(1))//ok, we can clear the right mouse button and use it for the next attack
        {
            if (rightButtonDown == true)
            {
                rightButtonDown = false;
            }
        }
    }

    public void SetTriggerAttack(string attackName)
    {
        animator.SetTrigger(attackName);
        wasAttacking = true;
    }
}
