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

    private PlayerSkills playerSkills;
    private BattleManager battleManager;

    // Use this for initialization
    protected virtual void Start()
    {
        battleManager = BattleManager.instance;
        battleManager.onTurnChange += onTurnChange;
        animator = GetComponentInChildren<Animator>();
        playerSkills = GetComponent<PlayerSkills>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
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

    public void StartAttack(int comboNumber)
    {
        if (isMoving == false)
        {
            isMoving = true;
            startPos = transform.localPosition;
            endPos = transform.localPosition + Vector3.right * 2;
        }

        string animationName = playerSkills.combos[comboNumber][0].animationName;
        SetTriggerAttack(animationName);
    }

    public void SetTriggerAttack(string animationName)
    {
        animator.Play(animationName);
    }

    void onTurnChange(bool playerTurn)
    {
        if (!playerTurn)
        {
            Vector3 temp = startPos;
            startPos = endPos;
            endPos = temp;
            isMoving = true;
        }
    }

}
