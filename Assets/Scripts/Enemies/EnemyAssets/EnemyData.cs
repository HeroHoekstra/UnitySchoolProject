using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Settings/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float detectionRadius;
    public float maxHealth;
    public float defense;

    [Header("Weapon data")]
    public float shootDistance;
    public float damageMultiplier;
    [Range(0, 100)]
    public int dropChance;
    public GameObject weapon;

    [Header("Movement")]
    public float speed;
    public float keepDistance;
    public float moveRange;
    public Behaviors behaviors;

    [Header("Other")]
    public int score;

    public enum Behaviors
    {
        NONE,
        MELEE,
        RANGE_SITTING_DUCK,
        RANGE_MOVE_AFTER_RELOAD,
        RANGE_MOVE_AFTER_SHOT
    }
}
