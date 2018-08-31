using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public BlockDirectionEnum currentBlock = BlockDirectionEnum.None;
    GameObject blockIconLeft;
    GameObject blockIconRight;

    private UIManager uiManager;

    // Use this for initialization
    void Start()
    {
        uiManager = UIManager.instance;
        blockIconLeft = GameObject.FindGameObjectsWithTag("PlayerBlockIconLeft")[0];
        blockIconRight = GameObject.FindGameObjectsWithTag("PlayerBlockIconRight")[0];
    }
}
