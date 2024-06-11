using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClearLevel : MonoBehaviour
{
    private Tilemap[] tilemaps;
    private Transform enemies;

    public void Init()
    {
        // Get Variables
        tilemaps = GameObject.Find("TerrainSpawner").GetComponentsInChildren<Tilemap>();
        enemies = GameObject.Find("Enemies").transform;
    }

    // Start is called before the first frame update
    public void Clear()
    {
        // Clear tilemaps
        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap != null)
            {
                tilemap.ClearAllTiles();
            }
        }

        // Clear enemies
        foreach (Transform child in enemies)
        {
            if (child != null)
            {
                Destroy(child);
            }
        }
    }
}
