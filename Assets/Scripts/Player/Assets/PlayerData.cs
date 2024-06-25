using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Player Settings/Player Data")]
public class PlayerData : ScriptableObject
{
    public float maxHealth;
    public float defense;

    public float speed;

    public float damageMultiplier;

    public GameObject weapon;
}
