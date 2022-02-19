using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;
    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer (int damage, Vector3 direction) {
        currentHealth -= damage;
        thePlayer.Knockback(direction);
        healthBar.SetHealth(currentHealth);
    }

    public void HealPlayer (int healAmount) {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}