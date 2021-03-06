﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public BlockDirectionEnum currentBlock = BlockDirectionEnum.None;
    GameObject blockIconLeft;
    GameObject blockIconRight;

    public BlockDirectionEnum currentComboDirection = BlockDirectionEnum.None;
    GameObject comboIconLeft;
    GameObject comboIconRight;

    private BattleManager battleManager;
    private EnemyAnimator enemyAnimator;
    private EnemySkills enemySkills;
    private UnitStats enemyStats;
    
    // Use this for initialization
    void Start()
    {
        battleManager = BattleManager.instance;
        battleManager.onTurnChange += onTurnChange;
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemySkills = GetComponent<EnemySkills>();
        enemyStats = GetComponent<UnitStats>();

        blockIconLeft = GameObject.FindGameObjectsWithTag("BlockIconLeft")[0];
        blockIconLeft.GetComponent<Image>().enabled = true;
        blockIconLeft.SetActive(false);
        blockIconRight = GameObject.FindGameObjectsWithTag("BlockIconRight")[0];
        blockIconRight.GetComponent<Image>().enabled = true;
        blockIconRight.SetActive(false);

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
            blockIconLeft.SetActive(true);
        }
        else
        {
            blockIconRight.SetActive(true);
        }
    }

    public void EndBlock()
    {
        currentBlock = BlockDirectionEnum.None;
        blockIconLeft.SetActive(false);
        blockIconRight.SetActive(false);
    }

    // Player combo was blocked, end block ui and play counter attack animation
    public void StartCounterAttack()
    {
        enemyAnimator.PlayCounterAnimation(enemySkills.counterAttack.animationName);
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

    public void TakeDamage(int damage)
    {
        enemyStats.TakeDamage(damage);
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