using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Settings", menuName = "Terrain Data/Terrain Settings")]
public class TerrainSettings : ScriptableObject
{
    // Get variables
    public int mapWidth;
    public int mapHeight;
    public float noiseMapScale;
    public float falloffDistance;

    // Seed
    public bool setRandomSeed;
    public Vector2 seed;
}

