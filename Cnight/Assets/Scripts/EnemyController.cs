using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public BlockDirection currentBlock = BlockDirection.None;
    GameObject blockIconLeft;
    GameObject blockIconRight;

    // Use this for initialization
    void Start()
    {
        blockIconLeft = GameObject.FindGameObjectsWithTag("BlockIconLeft")[0];
        blockIconRight = GameObject.FindGameObjectsWithTag("BlockIconRight")[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("StartBlock")]
    public void StartBlock()
    {
        BlockDirection blockDirection = Random.Range(0, 100) > 50 ? BlockDirection.Left : BlockDirection.Right;

        if(blockDirection == BlockDirection.Left)
        {
            BlockUIController blockUIController = blockIconLeft.GetComponent<BlockUIController>();
            blockUIController.SetActive(true);
        }
        else
        {
            BlockUIController blockUIController = blockIconRight.GetComponent<BlockUIController>();
            blockUIController.SetActive(true);
        }
    }
}

public enum BlockDirection
{
    None,
    Left,
    Right,
    Up,
    Down
}