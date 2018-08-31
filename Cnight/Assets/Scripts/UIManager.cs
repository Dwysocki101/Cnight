using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    GameObject player;
    public Button attackButton1;
    public Button attackButton2;
    public Canvas playerDirectionCanvas;

    // directional choice cooldown at max time, default to 2 seconds
    public float maxChoiceCooldown = 2f;
    // directional choice cooldown remaining
    public float currentChoiceCooldown = 0f;
    private bool directionChoiceActive = false;

    private EnemyController enemyController;

    private BattleManager battleManager;

    private void Start()
    {
        battleManager = BattleManager.instance;
        player = PlayerManager.instance.player;
        enemyController = EnemyManager.instance.enemy.GetComponent<EnemyController>();

        Text attackButton1Text = attackButton1.GetComponentInChildren<Text>();
        attackButton1Text.text = player.GetComponent<PlayerSkills>().combos[0][0].name;
        Text attackButton2Text = attackButton2.GetComponentInChildren<Text>();
        attackButton2Text.text = player.GetComponent<PlayerSkills>().combos[1][0].name;
    }

    private void Update()
    {
        if (directionChoiceActive)
        {
            currentChoiceCooldown -= Time.deltaTime;

            if (currentChoiceCooldown <= 0)
            {
                EndPlayerCombo();
            }
        }

        if (playerDirectionCanvas.isActiveAndEnabled)
        {
            UpdateCanvasPosition();
        }
    }

    public void ContinuePlayerCombo()
    {
        directionChoiceActive = true;
        currentChoiceCooldown = maxChoiceCooldown;
        ShowPlayerDirectionCanvas(true);
        enemyController.StartBlock();
    }

    // Show/Hide player directional canvas for choosing attack/block direction.
    public void ShowPlayerDirectionCanvas(bool showCanvas)
    {
        playerDirectionCanvas.gameObject.SetActive(showCanvas);

        if (showCanvas)
        {
            UpdateCanvasPosition();
        }
    }

    public void EndPlayerCombo()
    {
        directionChoiceActive = false;
        ShowPlayerDirectionCanvas(false);
        enemyController.EndBlock();
        battleManager.TurnEnded();
    }

    // Move the directional arrows canvas over the current player position.
    private void UpdateCanvasPosition()
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        playerDirectionCanvas.transform.position = playerPosition;
    }
}
