using UnityEngine;
using Pathfinding;

public class UpdateMapSize : MonoBehaviour
{
    private AstarPath astar;
    private TerrainSettings settings;

    private void Start()
    {
        astar = FindObjectOfType<AstarPath>();
        settings = GameObject.Find("TerrainSpawner").GetComponent<GenerateTilemap>().settings;

        GridGraph grid = astar.data.gridGraph;

        grid.SetDimensions(settings.mapWidth, settings.mapHeight, 1);
        transform.position = new Vector2(0, 0);
        grid.center = new Vector3(settings.mapWidth / 2, settings.mapHeight / 2, 0);

        astar.Scan();
    }
}