using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public EnemyData enemyData;
    public PlayerData playerData;

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
        if (playerData != null)
        {
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
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Oof! auwie! u die :(");
                }
            }
        }
    }
}
