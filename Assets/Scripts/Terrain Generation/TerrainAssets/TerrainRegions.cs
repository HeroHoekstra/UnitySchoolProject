using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Terrain Regions", menuName = "Terrain Data/Terrain Regions")]
public class TerrainRegions : ScriptableObject
{
    public TerrainRegion[] terrainRegion;

    [System.Serializable]
    public struct TerrainRegion
    {
        public bool isWalkable;
        public TileBase tileBase;

        [Range(0f, 1f)]
        public float startHeight;
        [Range (0f, 1f)]
        public float endHeight;
    }
}
