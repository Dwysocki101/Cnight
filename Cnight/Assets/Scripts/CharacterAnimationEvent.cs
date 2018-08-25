using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvent : MonoBehaviour {
    private GameObject player;
    private BattleManager battleManager;

    private void Awake()
    {
        player = PlayerManager.instance.player;
    }

    private void Start()
    {
        battleManager = BattleManager.instance;
    }

    public void AttackFinishedEvent()
    {
        battleManager.CurrentAttackFinished();
    }
}
