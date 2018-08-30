﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BattleManager : MonoBehaviour
{

    #region Singleton
    public static BattleManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private bool isPlayerTurn = true;

    public delegate void OnTurnChange(bool playerTurn);
    public OnTurnChange onTurnChange;

    private GameObject player;
    private PlayerAnimator playerAnimator;
    private PlayerSkills playerSkills;

    private GameObject enemy;
    private EnemyController enemyController;

    Queue<Skill> currentTurnComboQueue;

    private void Start()
    {
        player = PlayerManager.instance.player;
        playerAnimator = player.GetComponent<PlayerAnimator>();
        playerSkills = player.GetComponent<PlayerSkills>();

        enemy = EnemyManager.instance.enemy;
        enemyController = enemy.GetComponent<EnemyController>();
    }

    private void Update()
    {
    }

    public void AttackPressed(int comboNumber)
    {
        if (isPlayerTurn)
        {
            currentTurnComboQueue = new Queue<Skill>(playerSkills.combos[comboNumber]);
            string animationName = currentTurnComboQueue.Dequeue().animationName;
            playerAnimator.StartAttack(animationName);
        }
        else
        {
            throw new NotImplementedException("TODO: button clicked on not player turn;this ui should probably be hidden");
        }
       
    }

    [ContextMenu("EndTurn")]
    public void forceTurnEnd()
    {
        TurnEnded();
    }

    public void TurnEnded()
    {
        isPlayerTurn = !isPlayerTurn;
        onTurnChange.Invoke(isPlayerTurn);
    }

    public void CurrentAttackFinished()
    {
        if (currentTurnComboQueue.Count > 0)
        {
            if (isPlayerTurn)
            {
                //playerController.ContinueCombo();
                enemyController.StartBlock();
            }
        }
        else
        {
            currentTurnComboQueue = null;
            TurnEnded();
        }
    }
}
