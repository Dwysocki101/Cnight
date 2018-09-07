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
    public bool isPlayerTurn = true;

    private GameObject player;
    private PlayerAnimator playerAnimator;
    private PlayerSkills playerSkills;
    private PlayerController playerController;

    private GameObject enemy;
    private EnemyController enemyController;
    private EnemyAnimator enemyAnimator;
    private EnemySkills enemySkills;

    private UIManager uiManager;

    private Queue<Skill> currentTurnComboQueue;
    private Skill currentSkill;

    public delegate void OnTurnChange(bool playerTurn);
    public OnTurnChange onTurnChange;

    private void Start()
    {
        player = PlayerManager.instance.player;
        playerAnimator = player.GetComponent<PlayerAnimator>();
        playerSkills = player.GetComponent<PlayerSkills>();
        playerController = player.GetComponent<PlayerController>();

        enemy = EnemyManager.instance.enemy;
        enemyController = enemy.GetComponent<EnemyController>();
        enemyAnimator = enemy.GetComponent<EnemyAnimator>();
        enemySkills = enemy.GetComponent<EnemySkills>();

        uiManager = UIManager.instance;
    }

    public void AttackPressed(int comboNumber)
    {
        if(isPlayerTurn)
        {
            currentTurnComboQueue = new Queue<Skill>(playerSkills.combos[comboNumber]);
            currentSkill = currentTurnComboQueue.Dequeue();
            playerAnimator.StartAttack(currentSkill.animationName);
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
            currentSkill = currentTurnComboQueue.Dequeue();
            playerAnimator.PlayAttackAnimation(currentSkill.animationName);
        }
    }

    // Attack animation finished. 
    // If no more attacks in combo, end the turn.
    // If there are more attacks in combo, tell UI to show next combo elements.
    public void CurrentAttackFinished(bool attackCountered)
    {
        // Last attack was finished, reset player direction chosen for next attack
        playerDirectionChosen = false;

        if(!attackCountered)
        {
            // attack hit, inflict damage on opponent
            DamageOpponent();

            if (currentTurnComboQueue.Count > 0)
            {
                if (isPlayerTurn)
                {
                    uiManager.ContinuePlayerCombo();
                }
                else
                {
                    uiManager.ContinueEnemyCombo();
                }
            }
            else
            {
                currentTurnComboQueue = null;
                TurnEnded();
            }
        }
        else
        {
            if (isPlayerTurn)
            {
                playerController.TakeDamage(enemySkills.counterAttack.damage);
            }
            else
            {
                enemyController.TakeDamage(playerSkills.counterAttack.damage);
            }

            currentTurnComboQueue = null;
            TurnEnded();
        }
    }

    // Given the combo list to use, star thte nemey combo. Play the first attack animation.
    public void StartEnemyCombo(List<Skill> comboList)
    {
        currentTurnComboQueue = new Queue<Skill>(comboList);
        currentSkill = currentTurnComboQueue.Dequeue();
        enemyAnimator.StartAttack(currentSkill.animationName);
    }

    // Called during enemy combo attack when user does not choose a block direction
    // Continue enemy combo next attack.
    // 
    public void PlayerBlockDirectionNotChosen()
    {
        playerDirectionChosen = false;
        // Remove block and directional canvas ui and play next animation
        uiManager.EndEnemyCombo();
        currentSkill = currentTurnComboQueue.Dequeue();
        enemyAnimator.PlayAttackAnimation(currentSkill.animationName);
    }

    // Called during enemy combo attack when user chooses a directional button to block.
    // If combo direction is same as player block direction, player will counter attack and end enemy turn.
    // If combo direction is NOT the same as player block direction, play next attack animation and continue combo.
    public void PlayerBlockDirectionChosen()
    {
        playerDirectionChosen = true;

        if (playerController.currentBlock == enemyController.currentComboDirection)
        {
            playerController.StartCounterAttack();
        }
        else
        {
            // Remove block and directional canvas ui and play next animation
            uiManager.EndEnemyCombo();
            currentSkill = currentTurnComboQueue.Dequeue();
            enemyAnimator.PlayAttackAnimation(currentSkill.animationName);
        }
    }

    void DamageOpponent()
    {
        int damage = currentSkill.damage;

        if (isPlayerTurn)
        {
            enemyController.TakeDamage(damage);
        }
        else
        {
            playerController.TakeDamage(damage);
        }
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
