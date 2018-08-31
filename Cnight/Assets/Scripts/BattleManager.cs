using System.Collections;
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
    private PlayerController playerController;

    private GameObject enemy;
    private EnemyController enemyController;

    private UIManager uiManager;

    Queue<Skill> currentTurnComboQueue;

    private void Start()
    {
        player = PlayerManager.instance.player;
        playerAnimator = player.GetComponent<PlayerAnimator>();
        playerSkills = player.GetComponent<PlayerSkills>();
        playerController = player.GetComponent<PlayerController>();

        enemy = EnemyManager.instance.enemy;
        enemyController = enemy.GetComponent<EnemyController>();

        uiManager = UIManager.instance;
    }

    private void Update()
    {
    }

    public void AttackPressed(int comboNumber)
    {
        if(isPlayerTurn)
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

    // Attack animation finished. 
    // If no more attacks in combo, end the turn.
    // If there are more attacks in combo, tell UI to show next combo elements.
    public void CurrentAttackFinished()
    {
        if(currentTurnComboQueue.Count > 0)
        {
            if (isPlayerTurn)
            {
                uiManager.ContinuePlayerCombo();
            }
        }
        else
       {
            currentTurnComboQueue = null;
            TurnEnded();
        }
    }
}
