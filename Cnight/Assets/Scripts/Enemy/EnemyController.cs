using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public BlockDirectionEnum currentBlock = BlockDirectionEnum.None;
    GameObject blockIconLeft;
    GameObject blockIconRight;
    BlockUIController blockLeftUIController;
    BlockUIController blockRightUIController;

    private EnemyAnimator enemyAnimator;

    // Use this for initialization
    void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();

        blockIconLeft = GameObject.FindGameObjectsWithTag("BlockIconLeft")[0];
        blockIconRight = GameObject.FindGameObjectsWithTag("BlockIconRight")[0];
        blockLeftUIController = blockIconLeft.GetComponent<BlockUIController>();
        blockRightUIController = blockIconRight.GetComponent<BlockUIController>();
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
}