using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnBoat : MonoBehaviour
{
    public GameObject boat;
    public GameObject player;
    public BoatSpawnSettings bSettings;

    public int minSize;

    private GameObject spawnedBoat;
    private Vector2 previousBoatPos;

    private GameObject spawnedPlayer;
    private bool holdPlayer = true;

    private GenerateTilemap gtm;
    private float[,] noiseMap;
    TerrainRegions.TerrainRegion lowestRegion;
    private Vector2 boatSpawnPos;

    // Variables for finding island size
    private int currentSize = 0;
    private bool[,] visited;
    private int width;
    private int height;


    private void Start()
    {
        gtm = GetComponent<GenerateTilemap>();

        noiseMap = gtm.noiseMap;
        lowestRegion = gtm.regions.terrainRegion
            .Where(s => s.isWalkable)
            .OrderBy(s => s.startHeight)
            .FirstOrDefault();

        width = gtm.noiseMap.GetLength(0);
        height = gtm.noiseMap.GetLength(1);
        visited = new bool[width, height];

        boatSpawnPos = getBoatSpawn() + new Vector2(bSettings.xOffset, bSettings.yOffset);

        spawnedBoat = Instantiate(boat, new Vector3(-10, boatSpawnPos.y, 0), Quaternion.identity);
        previousBoatPos = spawnedBoat.transform.position;

        spawnedPlayer = Instantiate(player, boatSpawnPos, Quaternion.identity);
        GameObject.Find("Main Camera").GetComponent<CameraMovement>().trans = spawnedPlayer.transform;
    }

    private void Update()
    {
        enterIsland();
    }

    private void enterIsland()
    {
        // Move the boat and slowly stop
        float distance = Vector2.Distance(spawnedBoat.transform.position, boatSpawnPos);
        float t = Mathf.Clamp01(Mathf.Pow(Time.deltaTime * bSettings.speed / distance, 0.5f));
        spawnedBoat.transform.position = Vector2.Lerp(spawnedBoat.transform.position, boatSpawnPos, t);

        // Check if the boat is stopped and release player from boat
        if (holdPlayer && Vector2.Distance(spawnedBoat.transform.position, previousBoatPos) == 0)
        {
            holdPlayer = false;

            // Check for the tiles the boat is on and add them to the walkable tile map
            RaycastHit2D[] hits = Physics2D.RaycastAll(spawnedBoat.transform.position, Vector2.down);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject == gtm.unWalkableTilemap.gameObject)
                {
                    Vector3Int cellPos = gtm.unWalkableTilemap.WorldToCell(hit.point);
                    TileBase tile = gtm.unWalkableTilemap.GetTile(cellPos);

                    if (tile != null)
                    {
                        gtm.unWalkableTilemap.SetTile(cellPos, null);
                        gtm.walkableTilemap.SetTile(cellPos, tile);
                        Debug.Log(cellPos);
                    }
                }
            }
        }

        if (holdPlayer)
        {
            holdPlayerInBoat();
        }
        else if (!spawnedPlayer.GetComponent<Movement>().enabled)
        {
            spawnedPlayer.GetComponent<Movement>().enabled = true;
        }

        previousBoatPos = spawnedBoat.transform.position;
    }

    private void holdPlayerInBoat()
    {
        spawnedPlayer.GetComponent<Movement>().enabled = false;
        spawnedPlayer.transform.position = spawnedBoat.transform.position;
    }

    private Vector2 getBoatSpawn()
    {
        bool firstXSeen = false;
        int firstY = -1;
        int firstX = 0;
        int lastX = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Detect first row
                if (noiseMap[x, y] >= lowestRegion.startHeight && noiseMap[x, y] <= lowestRegion.endHeight)
                {
                    if (!firstXSeen)
                    {
                        // Check if this island is big enough
                        checkNeighbors(x, y);
                        if (currentSize >= minSize)
                        {
                            firstXSeen = true;
                            firstX = x;
                        }
                        else
                        {
                            currentSize = 0;
                        }
                    }
                }
                else if (firstXSeen)
                {
                    lastX = x - 1;
                    firstY = y;
                    break;
                }
            }

            if (firstY != -1) break;
        }

        int xDiff = lastX - firstX;
        int middleX = xDiff / 2 + firstX;

        return new Vector2(middleX + .5f, firstY - .5f);
    }

    private void checkNeighbors(int x, int y)
    {
        // Check if the search is out of bounds
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return;
        }
        // Check if this square has been visited or if the land is too low
        if (visited[x, y] || !(noiseMap[x, y] >= lowestRegion.startHeight))
        {
            return;
        }

        visited[x, y] = true;

        currentSize++;

        // Check neighbors
        checkNeighbors(x + 1, y);
        checkNeighbors(x, y + 1);
        checkNeighbors(x - 1, y);
        checkNeighbors(x, y - 1);
    }
}