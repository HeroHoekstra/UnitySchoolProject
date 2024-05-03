using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Settings/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float detectionRadius;
    public float maxHealth;
    public float speed;
    
    public float keepDistance;
    public float damageMultiplier;
    public GameObject weapon;

    public Behaviors behaviors;

    public enum Behaviors
    {
        NONE,
        MELEE,
        RANGE_SITTING_DUCK,
        RANGE_MOVE_AFTER_SHOT
    }
}
