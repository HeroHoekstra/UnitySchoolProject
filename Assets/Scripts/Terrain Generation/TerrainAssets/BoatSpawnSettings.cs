using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boat Spawn Settings", menuName = "Terrain Data/Boat Spawn Settings")]
public class BoatSpawnSettings : ScriptableObject
{
    public float xOffset;
    public float yOffset;
    public float speed;
    public Vector2 playerDropOffOffset;
}
