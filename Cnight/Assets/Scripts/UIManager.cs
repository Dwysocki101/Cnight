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
    public Button attackButton3;
    public Canvas playerDirectionCanvas;
    public CanvasGroup directionCanvasGroup;

    GameObject enemy;
    public Canvas enemyComboCanvas;

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
        directionCanvasGroup = playerDirectionCanvas.GetComponent<CanvasGroup>();

        enemy = EnemyManager.instance.enemy;
        enemyController = EnemyManager.instance.enemy.GetComponent<EnemyController>();

        battleManager.onTurnChange += onTurnChange;

        Text attackButton1Text = attackButton1.GetComponentInChildren<Text>();
        attackButton1Text.text = player.GetComponent<PlayerSkills>().combos[0][0].name;
        Text attackButton2Text = attackButton2.GetComponentInChildren<Text>();
        attackButton2Text.text = player.GetComponent<PlayerSkills>().combos[1][0].name;
        Text attackButton3Text = attackButton3.GetComponentInChildren<Text>();
        attackButton3Text.text = player.GetComponent<PlayerSkills>().combos[2][0].name;
    }

    private void Update()
    {
        if (directionChoiceActive && !battleManager.playerDirectionChosen)
        {
            currentChoiceCooldown -= Time.deltaTime;

            if (currentChoiceCooldown <= 0)
            {
                if (battleManager.isPlayerTurn)
                {
                    // if cooldown expired without a choice, end current turn
                    battleManager.TurnEnded();
                }
                else
                {
                    // enemy turn and player did not choose a block direction
                    battleManager.PlayerBlockDirectionNotChosen();
                }
            }
        }

        if (playerDirectionCanvas.isActiveAndEnabled)
        {
            UpdateDirectionalCanvasPosition();
        }

        if (enemyComboCanvas.isActiveAndEnabled)
        {
            UpdateEnemyComboCanvasPosition();
        }
    }

    public void ContinuePlayerCombo()
    {
        directionCanvasGroup.interactable = true;
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
            UpdateDirectionalCanvasPosition();
        }
    }

    public void EndPlayerCombo()
    {
        directionChoiceActive = false;
        ShowPlayerDirectionCanvas(false);
        enemyController.EndBlock();
    }

    // Enemy combo is continuing. 
    // Show player directional canvas to choose a block direction.
    // Make enemy continue combo to show an attack direction.
    public void ContinueEnemyCombo()
    {
        directionCanvasGroup.interactable = true;
        directionChoiceActive = true;
        currentChoiceCooldown = maxChoiceCooldown;
        ShowPlayerDirectionCanvas(true);
        enemyController.ContinueCombo();
    }

    public void EndEnemyCombo()
    {
        directionChoiceActive = false;
        ShowPlayerDirectionCanvas(false);
        enemyController.EndComboChoice();
    }

    // Move the directional arrows canvas over the current player position.
    private void UpdateDirectionalCanvasPosition()
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        playerDirectionCanvas.transform.position = playerPosition;
    }

    public void DisableDirectionalCanvas()
    {
        directionCanvasGroup.interactable = false;
    }

    private void UpdateEnemyComboCanvasPosition()
    {
        Vector3 enemyPosition = Camera.main.WorldToScreenPoint(enemy.transform.position);
        enemyComboCanvas.transform.position = enemyPosition;
    }

    // Called when turn ended.
    // If player turn ended and player had a combo attack, end player's combo
    void onTurnChange(bool playerTurn)
    {
        if (directionChoiceActive)
        {
            if (!playerTurn)
            {
                EndPlayerCombo();
            }
            else
            {
                EndEnemyCombo();
            }
        }
    }
}
