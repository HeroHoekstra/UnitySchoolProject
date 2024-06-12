using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTilemap : MonoBehaviour
{
    [HideInInspector]
    public TerrainSettings settings;
    public TerrainRegions regions;

    public Tilemap walkableTilemap;
    public Tilemap unWalkableTilemap;

    [HideInInspector]
    public float[,] noiseMap;

    public void Start()
    {
        settings = GameObject.Find("MainManager").GetComponent<GameManager>().tSettings;

        noiseMap = generateNoiseMap(settings.mapWidth, settings.mapHeight, settings.noiseMapScale, getSeed(settings.seed));

        getTileMap(noiseMap);
    }

    private void getTileMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Vector3Int tilePos;

        // Generate a tilemap
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int i = 0; i < regions.terrainRegion.Length; i++)
                {
                    tilePos = new Vector3Int(x, y, 0);
                    if (noiseMap[x, y] >= regions.terrainRegion[i].startHeight && noiseMap[x, y] <= regions.terrainRegion[i].endHeight)
                    {
                        if (regions.terrainRegion[i].isWalkable)
                        {
                            walkableTilemap.SetTile(tilePos, regions.terrainRegion[i].tileBase);
                        }
                        else
                        {
                            unWalkableTilemap.SetTile(tilePos, regions.terrainRegion[i].tileBase);
                        }
                    }
                    else if (noiseMap[x, y] < 0)
                    {
                        unWalkableTilemap.SetTile(tilePos, regions.terrainRegion[0].tileBase);
                    }
                }
            }
        }
    }

    private float[,] generateNoiseMap(int width, int height, float noiseMapScale, Vector2 seed)
    {
        // Make Array
        float[,] noiseMap = new float[width, height];

        // If noise scale == 0, you get a devide by 0 error
        if (noiseMapScale <= 0) noiseMapScale = 0.0001f;

        float maxDist = (float)Mathf.Sqrt(width * width + height * height) / 2f;

        // Loop through each pixel
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float noiseMapScaleX = x / noiseMapScale + seed.x;
                float noiseMapScaleY = y / noiseMapScale + seed.y;

                // Get distance from center
                float dist = (float)Mathf.Sqrt(Mathf.Pow(x - width / 2, 2) + Mathf.Pow(y - height / 2, 2));

                noiseMap[x, y] = Mathf.PerlinNoise(noiseMapScaleX, noiseMapScaleY) - falloffIntensity(dist, maxDist, settings.falloffDistance);
            }
        }

        return noiseMap;
    }

    private Vector2 getSeed(Vector2 seed)
    {
        Vector2 newSeed;
        if (!settings.setRandomSeed && seed != null) {
            newSeed = seed;
        } 
        else
        {
            newSeed.x = Random.Range(-10000f, 10000f);
            newSeed.y = Random.Range(-10000f, 10000f);
        }

        return newSeed;
    }

    private float falloffIntensity(float distance, float maxDistance, float falloffDistance)
    {
        if (distance < falloffDistance) return 0f;
        return (distance - falloffDistance) / (maxDistance - falloffDistance);
    }
}