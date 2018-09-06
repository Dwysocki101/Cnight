using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    protected Animator animator;
    public bool isMoving;
    public float lerpTime = 0.5f;

    private Vector3 startPos;
    private Vector3 endPos;
    private float currentLerpTime = 0;

    private BattleManager battleManager;

    // Use this for initialization
    void Start () {
        battleManager = BattleManager.instance;
        battleManager.onTurnChange += onTurnChange;
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > lerpTime)
            {
                isMoving = false;
                currentLerpTime = lerpTime;
            }

            float lerpPercent = currentLerpTime / lerpTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, lerpPercent);

            if (!isMoving)
            {
                currentLerpTime = 0;
            }
        }
    }

    // Called when enemy successfully blocks attack. Play the 'CounterAttack01' (hardcoded) animation.
    public void PlayCounterAnimation()
    {
        animator.Play("CounterAttack01");
    }

    public void StartAttack(string animationName)
    {
        if (isMoving == false)
        {
            isMoving = true;
            startPos = transform.localPosition;
            endPos = transform.localPosition + Vector3.right * 2;
        }

        PlayAttackAnimation(animationName);
    }

    // Play attack animation.
    public void PlayAttackAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    void onTurnChange(bool playerTurn)
    {
        if (playerTurn)
        {
            Vector3 temp = startPos;
            startPos = endPos;
            endPos = temp;
            isMoving = true;
        }
    }
}
