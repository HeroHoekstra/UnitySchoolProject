using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public EnemyData enemyData;
    private PlayerData playerData;
    private HealthUI healthUI;

    private float maxHealth = -1;
    private float defense = -1;

    private ObjectType ot;

    private float health;

    private enum ObjectType
    {
        Enemy,
        Player,
        Breakable,
        Other
    }

    private void Start()
    {
        playerData = GameObject.Find("MainManager").GetComponent<GameManager>().playerData;

        if (playerData != null)
        {
            healthUI = GameObject.Find("CurrentHealth").GetComponent<HealthUI>();

            maxHealth = playerData.maxHealth;
            defense = playerData.defense;

            ot = ObjectType.Player;
        }
        else if (enemyData != null)
        {
            maxHealth = enemyData.maxHealth;
            defense = enemyData.defense;

            ot = ObjectType.Enemy;
        }
        else
        {
            ot = ObjectType.Other;
        }

        if (defense == -1 || maxHealth == -1 && ot != ObjectType.Other)
        {
            Debug.LogError("Could not find objects health data");
            return;
        }

        health = maxHealth;
    }

    public void Hit(float damage)
    {
        if (ot != ObjectType.Other)
        {
            health -= damage / defense;

            if (health < 0)
            {
                if (ot == ObjectType.Enemy || ot == ObjectType.Breakable)
                {
                    GameObject.Find("MainManager").GetComponent<GameManager>().score += enemyData.score;
                    Destroy(gameObject);
                }
                else
                {
                    // TODO: fix this
                    Debug.Log("hit player");
                    if (healthUI != null)
                    {
                        Debug.Log("Processing hit");
                        healthUI.UpdateBar(playerData.maxHealth, health);
                    }
                }
            }
        }
    }
}
