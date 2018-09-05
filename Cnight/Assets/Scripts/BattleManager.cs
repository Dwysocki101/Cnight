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

    public bool playerDirectionChosen = false;

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

    // Called during player combo attack when user chooses a directional button to continue the combo
    // If combo direction is same as monster block direction, monster will counter attack and end player turn
    // If combo direction is NOT the same as monster block direction, play next attack animation and continue combo
    public void PlayerComboDirectionChosen()
    {
        playerDirectionChosen = true;

        if (playerController.currentCombo == enemyController.currentBlock)
        {
            enemyController.StartCounterAttack();
        }
        else
        {
            // Remove block and directional canvas ui and play next animation
            uiManager.EndPlayerCombo();
            string animationName = currentTurnComboQueue.Dequeue().animationName;
            playerAnimator.PlayAttackAnimation(animationName);
        }
    }

    // Attack animation finished. 
    // If no more attacks in combo, end the turn.
    // If there are more attacks in combo, tell UI to show next combo elements.
    public void CurrentAttackFinished(bool attackCountered)
    {
        if(!attackCountered && currentTurnComboQueue.Count > 0)
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

    [ContextMenu("EndTurn")]
    public void forceTurnEnd()
    {
        TurnEnded();
    }

    // Reset playerDirectionChosen
    // Change turn and invoke onTurnChange event
    public void TurnEnded()
    {
        playerDirectionChosen = false;
        isPlayerTurn = !isPlayerTurn;
        onTurnChange.Invoke(isPlayerTurn);
    }
}
