using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvent : MonoBehaviour {
    private GameObject player;
    private BattleManager battleManager;

    private void Start()
    {
        player = PlayerManager.instance.player;
        battleManager = BattleManager.instance;
    }

    // Called when attack animation ends. Inform battle manager animation ended.
    public void AttackFinishedEvent()
    {
        battleManager.CurrentAttackFinished(false);
    }

    // Called when counter attack animation ends. Inform battle manager animation ended.
    public void CounterAttackEndEvent()
    {
        battleManager.CurrentAttackFinished(true);
    }
}
