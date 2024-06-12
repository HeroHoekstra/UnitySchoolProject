using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnEnemies : MonoBehaviour
{
    private EnemySpawnSettings ess;
    public Transform enemyParent;

    private Transform land;

    private void Start()
    {
        ess = GameObject.Find("MainManager").GetComponent<GameManager>().esSetting;

        land = transform.Find("Walkable");

        if (land != null)
        {
            int enemyAmount;
            if (ess.useDynamicSpawnAmount)
            {
                enemyAmount = (int)Mathf.Ceil(ess.difficullty * ess.dynamicEnemyMulitplier);
            } else
            {
                enemyAmount = ess.spawnAmount;
            }

            for (int i = 0; i < enemyAmount; i++)
            {
                // Get random tile position
                Vector3 randomTilePos = land.GetChild(Random.Range(0, land.childCount)).position + new Vector3(0, 0, -0.3f);

                // Get random enemy
                List<EnemySpawnSettings.EnemyTypes> eligibleEnemies = new List<EnemySpawnSettings.EnemyTypes>();
                foreach (var enemy in ess.enemyTypes)
                {
                    if (Mathf.Abs(enemy.difficulty - ess.difficullty) <= ess.enemyDifficultyMargin)
                    {
                        eligibleEnemies.Add(enemy);
                    }
                }

                if (eligibleEnemies.Count == 0)
                {
                    Debug.Log("Could not find eligible enemies to spawn, defaulting to all enemy types");
                    eligibleEnemies = ess.enemyTypes;
                }

                float totalRarity = 0;
                foreach (var enemy in eligibleEnemies)
                {
                    totalRarity += enemy.difficulty;
                }

                float randValue = Random.Range(0, totalRarity);

                float cumulativeRarity = 0;
                foreach (var enemy in eligibleEnemies)
                {
                    cumulativeRarity += enemy.rarity;
                    if (randValue < cumulativeRarity)
                    {
                        Instantiate(enemy.enemy, randomTilePos, Quaternion.identity, enemyParent);
                        break;
                    }
                }
            }
        } 
        else
        {
            Debug.LogError("Can't find the land area (bowomp) did you change the name of the \"Walkable\" tilemap?");
        }
    }
}
