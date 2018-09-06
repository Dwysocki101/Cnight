using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ActionsMenuController : MonoBehaviour {
    private BattleManager battleManager;
    private CanvasGroup canvasGroup;

	// Use this for initialization
	void Start () {
        battleManager = BattleManager.instance;
        battleManager.onTurnChange += onTurnChange;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void onTurnChange(bool playerTurn)
    {
        if (playerTurn)
        {
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.interactable = false;
        }
    }
}
