using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour {

    public int maxHealth = -1;
    public int currentHealth;

    public delegate void OnHealthChange(int current, int max);
    public OnHealthChange onHealthChange;

    private void Start()
    {
        if (maxHealth < 0)
        {
            maxHealth = 100;
        }

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = System.Math.Max(currentHealth, 0);
        onHealthChange.Invoke(currentHealth, maxHealth);
    }
}
