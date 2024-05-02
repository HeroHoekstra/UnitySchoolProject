using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Spawn Settings", menuName = "Enemy Settings/Enemy Spawn Settings")]
public class EnemySpawnSettings : ScriptableObject
{
    [Header("Spawning Enemies")]
    public int spawnAmount;
    public float difficullty;
    public float enemyDifficultyMargin;

    [Header("Dynamic Enemy Amount")]
    public bool useDynamicSpawnAmount;
    public float dynamicEnemyMulitplier;

    [Header("Enemies")]
    public List<EnemyTypes> enemyTypes;

    [System.Serializable]
    public struct EnemyTypes
    {
        public GameObject enemy;
        public float difficulty;
        public float rarity;
    }
}
