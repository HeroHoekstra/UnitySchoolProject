using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private GameManager gameManager; 
    private PlayerData playerData;
    private HealthUI healthUI;

    public EnemyData enemyData;
    private EnemyBehaviour enemyBehaviour;

    private float maxHealth = -1;
    private float defense = -1;

    private ObjectType ot;

    private float health;

    private enum ObjectType
    {
        Enemy,
        Player
    }

    private void Start()
    {
        if (enemyData != null)
        {
            maxHealth = enemyData.maxHealth;
            defense = enemyData.defense;

            enemyBehaviour = GetComponent<EnemyBehaviour>();

            ot = ObjectType.Enemy;
        } 
        else
        {
            playerData = GameObject.Find("MainManager").GetComponent<GameManager>().playerData;
            healthUI = GameObject.Find("Health").GetComponent<HealthUI>();
            gameManager = GameObject.Find("MainManager").GetComponent<GameManager>();

            maxHealth = playerData.maxHealth;
            defense = playerData.defense;

            ot = ObjectType.Player;
        }

        if (defense == -1 || maxHealth == -1)
        {
            Debug.LogError("Could not find objects health data");
            return;
        }

        health = maxHealth;
    }

    public void Hit(float damage)
    {
        health -= damage / defense;

        if (ot == ObjectType.Player)
        {
            healthUI.UpdateBar(maxHealth, health);
        } 
        else
        {
            if (enemyBehaviour.player == null)
            {
                enemyBehaviour.player = GameObject.Find("Player(Clone)").transform;
            }
        }

        if (health < 0)
        {
            if (ot == ObjectType.Enemy)
            {
                GameObject.Find("MainManager").GetComponent<GameManager>().score += enemyData.score;
                Destroy(gameObject);
            } 
            else if (ot == ObjectType.Player)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
