using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerSkills))]
public class PlayerAnimator : MonoBehaviour
{
    protected Animator animator;
    public bool isMoving;// if player is in middle of a combo
    public float lerpTime;

    private Vector3 startPos;
    private Vector3 endPos;
    private float currentLerpTime = 0;

    private BattleManager battleManager;
    private UIManager uiManager;

    // Use this for initialization
    protected virtual void Start()
   {
        battleManager = BattleManager.instance;
        battleManager.onTurnChange += onTurnChange;
        uiManager = UIManager.instance;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isMoving)
        {
            currentLerpTime += Time.deltaTime;

            if(currentLerpTime > lerpTime)
            {
                isMoving = false;
                currentLerpTime = lerpTime;
            }

            float lerpPercent = currentLerpTime / lerpTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, lerpPercent);

            if(!isMoving)
            {
                currentLerpTime = 0;
            }
        }
    }

    public void StartAttack(string animationName)
    {
        if(isMoving == false)
        {
            isMoving = true;
            startPos = transform.localPosition;
            endPos = transform.localPosition + Vector3.left * 2;
        }

        PlayAttackAnimation(animationName);
    }

    // Play attack animation and disable directional canvas.
    public void PlayAttackAnimation(string animationName)
    {
        uiManager.ShowPlayerDirectionCanvas(false);
        animator.Play(animationName);
    }

    void onTurnChange(bool playerTurn)
    {
        if(!playerTurn)
        {
            Vector3 temp = startPos;
            startPos = endPos;
            endPos = temp;
            isMoving = true;
        }
    }
}
