using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public BlockDirectionEnum currentBlock = BlockDirectionEnum.None;
    GameObject blockIconLeft;
    GameObject blockIconRight;

    public BlockDirectionEnum currentCombo = BlockDirectionEnum.None;

    private PlayerAnimator playerAnimator;
    private UnitStats playerStats;

    private UIManager uiManager;
    private BattleManager battleManager;

    // Use this for initialization
    void Start()
    {
        battleManager = BattleManager.instance;
        uiManager = UIManager.instance;
        playerAnimator = GetComponent<PlayerAnimator>();
        playerStats = GetComponent<UnitStats>();

        battleManager.onTurnChange += onTurnChange;

        blockIconLeft = GameObject.FindGameObjectsWithTag("PlayerBlockIconLeft")[0];
        blockIconRight = GameObject.FindGameObjectsWithTag("PlayerBlockIconRight")[0];
    }

    // Called when user chooses a direction for continuing the combo attack or blocking
    // Using block direction enum to compare with monster block, should probably rename enum :P
    // ** Convert int to block direction**
    public void ComboDirectionalButtonClicked(int direction)
    {
        uiManager.DisableDirectionalCanvas();

        if (battleManager.isPlayerTurn)
        {
            currentCombo = (BlockDirectionEnum) direction;
            battleManager.PlayerComboDirectionChosen();
        }
        else
        {
            currentBlock = (BlockDirectionEnum) direction;
            battleManager.PlayerBlockDirectionChosen();
        }
    }

    // Enemy combo was blocked, play counter attack animation
    public void StartCounterAttack()
    {
        playerAnimator.PlayCounterAnimation();
    }

    public void TakeDamage(int damage)
    {
        playerStats.TakeDamage(damage);
    }

    void onTurnChange(bool playerTurn)
    {
        currentCombo = BlockDirectionEnum.None;
        currentBlock = BlockDirectionEnum.None;
    }
}
