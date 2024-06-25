using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    // Warning: this script is really ugly IMO
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
        // If enemy data is not null, this script belongs to an enemy
        if (enemyData != null)
        {
            // Gets this enemy's data and object type for easier type accessibility 
            maxHealth = enemyData.maxHealth;
            defense = enemyData.defense;
            ot = ObjectType.Enemy;
            enemyBehaviour = GetComponent<EnemyBehaviour>();
        }
        // Else it belongs to the player
        else
        {
            // Get player data and UI data for health bar
            playerData = GameObject.Find("MainManager").GetComponent<GameManager>().playerData;
            healthUI = GameObject.Find("Health").GetComponent<HealthUI>();
            gameManager = GameObject.Find("MainManager").GetComponent<GameManager>();

            maxHealth = playerData.maxHealth;
            defense = playerData.defense;
            ot = ObjectType.Player;
        }

        // By default, defense and health are -1 (which should never occur in a data script), so if they are not changed no health data was found
        if (defense == -1 || maxHealth == -1)
        {
            Debug.LogError("Could not find objects health data");
            return;
        }

        health = maxHealth;
    }

    // Handles being hit
    public void Hit(float damage)
    {
        // Remove from health
        health -= damage / defense;

        if (ot == ObjectType.Player)
        {
            // Update health bar
            healthUI.UpdateBar(maxHealth, health);
        } 
        else
        {
            // Make enemy detect player
            if (enemyBehaviour.player == null)
            {
                enemyBehaviour.player = GameObject.Find("Player(Clone)").transform;
            }
        }

        // Death
        if (health <= 0)
        {
            if (ot == ObjectType.Enemy)
            {
                // Drop weapon
                if (Mathf.Round(Random.Range(0, 100)) < enemyData.dropChance)
                {
                    GameObject weapon = Instantiate(GetComponent<EnemyBehaviour>().enemyData.weapon, transform.position, transform.rotation);
                    weapon.GetComponent<BoxCollider2D>().enabled = true;
                    weapon.GetComponent<WeaponBehavior>().pickUp = true;
                }

                // Add score and die
                GameObject.Find("MainManager").GetComponent<GameManager>().score += enemyData.score;
                Destroy(gameObject);
            } 
            else if (ot == ObjectType.Player)
            {
                // Load death screen
                SceneManager.LoadScene("Death");
            }
        }
    }
}
