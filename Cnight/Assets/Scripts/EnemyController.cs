using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("StartBlock")]
    public void StartBlock()
    {
        BlockDirection blockDirection = Random.Range(0, 100) > 50 ? BlockDirection.Left : BlockDirection.Right;
        if (blockDirection == BlockDirection.Left)
        {
            GameObject shieldIconLeft = GameObject.FindGameObjectsWithTag("NecroShieldIconLeft")[0];
            NecroShieldIcon icon = shieldIconLeft.GetComponent<NecroShieldIcon>();
            icon.SetActive(true);
        }
        else
        {
            GameObject shieldIconLeft = GameObject.FindGameObjectsWithTag("NecroShieldIconRight")[0];
            NecroShieldIcon icon = shieldIconLeft.GetComponent<NecroShieldIcon>();
            icon.SetActive(true);
        }
    }
}

public enum BlockDirection
{
    Left,
    Right,
    Up,
    Down
}