using UnityEngine;
using Pathfinding;
using System.Collections;

public class UpdateMapSize : MonoBehaviour
{
    private AstarPath astar;
    private TerrainSettings settings;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        // Wait for .2s (ik that this is bad practise but I can't be fucked)
        yield return new WaitForSeconds(0.2f);

        // Get map size
        astar = FindObjectOfType<AstarPath>();
        settings = GameObject.Find("TerrainSpawner").GetComponent<GenerateTilemap>().settings;

        // Set A* graph size
        GridGraph grid = astar.data.gridGraph;
        grid.SetDimensions(settings.mapWidth, settings.mapHeight, 1);
        transform.position = new Vector2(0, 0);
        grid.center = new Vector3(settings.mapWidth / 2, settings.mapHeight / 2, 0);

        astar.Scan();
    }
}