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

    public void StartBlock()
    {
        BlockDirection blockDirection = Random.Range(0, 1) > 0.5 ? BlockDirection.Left : BlockDirection.Right;

    }
}

public enum BlockDirection
{
    Left,
    Right,
    Up,
    Down
}