using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public BlockDirectionEnum currentBlock = BlockDirectionEnum.None;
    GameObject blockIconLeft;
    GameObject blockIconRight;

    public BlockDirectionEnum currentCombo = BlockDirectionEnum.None;

    private UIManager uiManager;
    private BattleManager battleManager;

    // Use this for initialization
    void Start()
    {
        battleManager = BattleManager.instance;
        uiManager = UIManager.instance;

        battleManager.onTurnChange += onTurnChange;

        blockIconLeft = GameObject.FindGameObjectsWithTag("PlayerBlockIconLeft")[0];
        blockIconRight = GameObject.FindGameObjectsWithTag("PlayerBlockIconRight")[0];
    }

    // Called when user chooses a direction for continuing the combo attack
    // Using block direction enum to compare with monster block, should probably rename enum :P
    // ** Convert int to block direction**
    public void ComboDirectionalButtonClicked(int direction)
    {
        currentCombo = (BlockDirectionEnum) direction;
        battleManager.PlayerComboDirectionChosen();
    }

    void onTurnChange(bool playerTurn)
    {
        if (!playerTurn)
        {
            currentCombo = BlockDirectionEnum.None;
        }
    }
}
