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

    // Use this for initialization
    void Start()
    {
        blockIconLeft = GameObject.FindGameObjectsWithTag("BlockIconLeft")[0];
        blockIconRight = GameObject.FindGameObjectsWithTag("BlockIconRight")[0];
        blockLeftUIController = blockIconLeft.GetComponent<BlockUIController>();
        blockRightUIController = blockIconRight.GetComponent<BlockUIController>();
    }

    public void StartBlock()
    {
        BlockDirectionEnum blockDirection = Random.Range(0, 100) > 50 ? BlockDirectionEnum.Left : BlockDirectionEnum.Right;

        if(blockDirection == BlockDirectionEnum.Left)
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
        blockLeftUIController.SetActive(false);
        blockRightUIController.SetActive(false);
    }
}