using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Terrain Settings")]
    public TerrainSettings tSettings;
    public float terrainSizeMultiplier;

    [Header("Enemy Settings")]
    public EnemySpawnSettings esSetting;
    public float difficultyIncrease;
    public int spawnIncrease;
    public float spawnMultiplierIncrease;

    [Header("Other")]
    public PlayerData playerData;
    public KeepData keepData;
    public int oldScore;
    public int score = 0;

    [Header("Defaults")]
    public TerrainSettings dTSettings;
    public EnemySpawnSettings dEsSettings;
    public PlayerData dPlayerData;

    private int level = 0;

    private void Awake()
    {
        // Make this permanent and remove previous session data
        Instance = this;
        DontDestroyOnLoad(gameObject);

        ResetData();
    }

    public void NextLevelData()
    {
        level++;

        // Alter terrain settings
        tSettings.mapHeight = Mathf.FloorToInt((tSettings.mapHeight + level) * terrainSizeMultiplier);
        tSettings.mapWidth = Mathf.FloorToInt((tSettings.mapWidth + level) * terrainSizeMultiplier);
        tSettings.falloffDistance -= level * spawnMultiplierIncrease;

        // Alter enemy settings
        esSetting.difficullty += difficultyIncrease;
        esSetting.spawnAmount += spawnIncrease;
        esSetting.dynamicEnemyMulitplier += difficultyIncrease;
    }

    public void ResetData()
    {
        oldScore = score;
        keepData.UpdateHighScore(score);

        tSettings = Instantiate(dTSettings);
        esSetting = Instantiate(dEsSettings);
        playerData = Instantiate(dPlayerData);

        score = 0;
        level = 0;
    }
}
