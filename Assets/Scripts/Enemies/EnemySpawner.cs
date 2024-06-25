using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnEnemies : MonoBehaviour
{
    private EnemySpawnSettings ess;
    public Transform enemyParent;

    private Tilemap land;

    private void Start()
    {
        // Gets data
        ess = GameObject.Find("MainManager").GetComponent<GameManager>().esSetting;
        land = transform.Find("Walkable").gameObject.GetComponent<Tilemap>();

        // Check for land
        if (land != null)
        {
            // If dynamic enemy amount is checked, get the appropriate amount
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
                Vector3Int randomCell = GetRandomTilePosition(land);
                Vector3 randomTilePos;
                if (randomCell != null)
                {
                    randomTilePos = land.CellToWorld(randomCell);
                } else
                {
                    return;
                }

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

                // Spawn the right enemies
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
            Debug.LogError("Can't find the land area did you change the name of the \"Walkable\" tilemap?");
        }
    }

    Vector3Int GetRandomTilePosition(Tilemap tilemap)
    {
        // Get the bounds of the tilemap
        BoundsInt bounds = tilemap.cellBounds;
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        // Iterate through the bounds and collect all positions with a tile
        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                for (int z = bounds.zMin; z <= bounds.zMax; z++)
                {
                    Vector3Int localPlace = new Vector3Int(x, y, z);
                    if (tilemap.HasTile(localPlace))
                    {
                        tilePositions.Add(localPlace);
                    }
                }
            }
        }

        // Check if there are any tiles in the tilemap
        if (tilePositions.Count > 0)
        {
            // Pick a random tile position
            int randomIndex = Random.Range(0, tilePositions.Count);
            return tilePositions[randomIndex];
        }

        // Return zero if no tiles are found
        return Vector3Int.zero;
    }
}
