using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyData enemyData;

    private Transform player;
    private GameObject target;
    private bool range;

    private GameObject weapon;
    private WeaponBehavior weaponBehavior;

    private EnemyState currentState = EnemyState.Idle;
    private Vector2 destination;
    private enum EnemyState
    {
        Idle,
        Shooting,
        Moving,
    }

    public void Start()
    {
        // Give them their weapon
        weapon = Instantiate(enemyData.weapon, transform.position, Quaternion.identity, transform);
        weaponBehavior = weapon.GetComponent<WeaponBehavior>();
        weaponBehavior.parent = gameObject;

        // Set detection radius
        CircleCollider2D sightDist = gameObject.AddComponent<CircleCollider2D>();
        sightDist.radius = enemyData.detectionRadius;
        sightDist.isTrigger = true;

        // Set movement speed
        GetComponent<AIPath>().maxSpeed = enemyData.speed;

        range = enemyData.behaviors == EnemyData.Behaviors.RANGE_SITTING_DUCK ||
                enemyData.behaviors == EnemyData.Behaviors.RANGE_MOVE_AFTER_SHOT ||
                enemyData.behaviors == EnemyData.Behaviors.RANGE_MOVE_AFTER_RELOAD;

        target = transform.parent.Find("Target").gameObject;
    }

    private void Update()
    {
        if (player != null)
        {
            if (currentState == EnemyState.Idle)
            {
                if (enemyData.behaviors == EnemyData.Behaviors.MELEE)
                {
                    MoveTargetToPosition(player.position);
                }
                else if (range)
                {
                    Shoot();

                    Vector2 direction = player.position - transform.position;
                    destination = (Vector2)player.position - direction.normalized * enemyData.keepDistance;

                    if ((enemyData.behaviors == EnemyData.Behaviors.RANGE_MOVE_AFTER_SHOT && weaponBehavior.shot) ||
                        (enemyData.behaviors == EnemyData.Behaviors.RANGE_MOVE_AFTER_RELOAD && weaponBehavior.reloading))
                    {
                        weaponBehavior.shot = false;
                        weaponBehavior.reloading = false;
                        Vector2 randomOffset = new Vector2(Random.Range(-enemyData.moveRange, enemyData.moveRange), Random.Range(-enemyData.moveRange, enemyData.moveRange));
                        destination += randomOffset;
                        currentState = EnemyState.Moving;
                    }
                    
                    MoveTargetToPosition(destination);
                }
            }
            else if (currentState == EnemyState.Moving)
            {
                MoveTargetToPosition(destination);
                if (Vector2.Distance(target.transform.position, destination) <= enemyData.shootDistance)
                {
                    currentState = EnemyState.Idle;
                }
            }
        }
    }

    private void MoveTargetToPosition(Vector2 position)
    {
        target.transform.position = position;
    }

    private void Shoot()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < enemyData.shootDistance)
        {
            // Point weapon
            Vector3 targetDirection = player.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            weaponBehavior.Shoot(enemyData.damageMultiplier);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.transform;
        }
    }
}
