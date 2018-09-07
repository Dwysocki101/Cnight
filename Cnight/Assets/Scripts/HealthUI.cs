using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    public Image healthBar;

	// Use this for initialization
	void Start () {
        GetComponent<UnitStats>().onHealthChange += OnHealthChange;
    }

    void OnHealthChange(int currentHealth, int maxHealth)
    {
        float healthPercent = currentHealth / (float) maxHealth;
        healthBar.fillAmount = healthPercent;
    }
}
