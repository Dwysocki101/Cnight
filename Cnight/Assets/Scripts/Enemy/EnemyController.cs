using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public BlockDirectionEnum currentBlock = BlockDirectionEnum.None;
    GameObject blockIconLeft;
    GameObject blockIconRight;
    BlockUIController blockLeftUIController;
    BlockUIController blockRightUIController;

    public BlockDirectionEnum currentComboDirection = BlockDirectionEnum.None;
    GameObject comboIconLeft;
    GameObject comboIconRight;

    private BattleManager battleManager;
    private EnemyAnimator enemyAnimator;
    private EnemySkills enemySkills;
    
    // Use this for initialization
    void Start()
    {
        battleManager = BattleManager.instance;
        battleManager.onTurnChange += onTurnChange;
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemySkills = GetComponent<EnemySkills>();

        blockIconLeft = GameObject.FindGameObjectsWithTag("BlockIconLeft")[0];
        blockIconRight = GameObject.FindGameObjectsWithTag("BlockIconRight")[0];
        blockLeftUIController = blockIconLeft.GetComponent<BlockUIController>();
        blockRightUIController = blockIconRight.GetComponent<BlockUIController>();

        comboIconLeft = GameObject.FindGameObjectsWithTag("ComboIconLeft")[0];
        comboIconLeft.GetComponent<Image>().enabled = true;
        comboIconLeft.SetActive(false);
        comboIconRight = GameObject.FindGameObjectsWithTag("ComboIconRight")[0];
        comboIconRight.GetComponent<Image>().enabled = true;
        comboIconRight.SetActive(false);
    }

    public void StartBlock()
    {
        currentBlock = Random.Range(0, 100) > 50 ? BlockDirectionEnum.Left : BlockDirectionEnum.Right;

        if(currentBlock == BlockDirectionEnum.Left)
        {
            blockLeftUIController.SetActive(true);
        }
        else
        {
            blockRightUIController.SetActive(true);
        }
    }

    public void EndBlock()
    {
        currentBlock = BlockDirectionEnum.None;
        blockLeftUIController.SetActive(false);
        blockRightUIController.SetActive(false);
    }

    // Player combo was blocked, end block ui and play counter attack animation
    public void StartCounterAttack()
    {
        enemyAnimator.PlayCounterAnimation();
    }

    // Called when enemy turn starts. Wait 1s before starting enemy attack.
    IEnumerator ExecuteWhenTurnStart()
    {
        yield return new WaitForSeconds(1f);

        battleManager.StartEnemyCombo(GetComboList());
    }

    // Get the combo list to use for this turn.
    private List<Skill> GetComboList()
    {
        List<List<Skill>> combos = enemySkills.combos;
        int combosLength = combos.Count;
        int key = Random.Range(0, 100);
        List<Skill> comboList = combos[0];;

        if (combosLength == 2)
        {
            if (key < 60)
            {
                comboList = combos[0];
            }
            else
            {
                comboList = combos[1];
            }
        }
        else if (combosLength == 3)
        {
            if (key < 50)
            {
                comboList = combos[0];
            }
            else if (key < 80)
            {
                comboList = combos[1];
            }
            else
            {
                comboList = combos[2];
            }
        }

        return comboList;
    }

    public void ContinueCombo()
    {
        currentComboDirection = Random.Range(0, 100) > 50 ? BlockDirectionEnum.Left : BlockDirectionEnum.Right;

        if (currentComboDirection == BlockDirectionEnum.Left)
        {
            comboIconLeft.SetActive(true);
        }
        else
        {
            comboIconRight.SetActive(true);
        }
    }

    public void EndComboChoice()
    {
        currentComboDirection = BlockDirectionEnum.None;
        comboIconLeft.SetActive(false);
        comboIconRight.SetActive(false);
    }

    // Turn changed. If it is now enemy turn, go to turn start coroutine
    void onTurnChange(bool playerTurn)
    {
        currentBlock = BlockDirectionEnum.None;
        currentComboDirection = BlockDirectionEnum.None;

        if (!playerTurn)
        {
            StartCoroutine(ExecuteWhenTurnStart());
        }
    }
}