using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{
    protected Animator animator;
    public bool isAttacking;// if player is in middle of a combo
    public float lerpTime;

    private Vector3 startPos;
    private Vector3 endPos;
    private float currentLerpTime = 0;

    // Use this for initialization
    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isAttacking)
        {
            currentLerpTime += Time.deltaTime;
            currentLerpTime = Mathf.Clamp(currentLerpTime, currentLerpTime, lerpTime);
            float lerpPercent = currentLerpTime / lerpTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, lerpPercent);
        }
    }

    public void SetTriggerAttack(string attackName)
    {
        animator.SetTrigger(attackName);

        if (isAttacking == false)
        {
            isAttacking = true;
            startPos = transform.localPosition;
            endPos = transform.localPosition + Vector3.right * 3;
        }
    }
}
