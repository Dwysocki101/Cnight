using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    #region Singleton
    public static BattleManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private bool isPlayerTurn = true;

    public delegate void OnTurnChange(bool playerTurn);
    public OnTurnChange onTurnChange;

    private void Update()
    {
        if (Input.GetKeyDown("5"))
        {
            isPlayerTurn = !isPlayerTurn;
            onTurnChange.Invoke(isPlayerTurn);
        }
    }
}
