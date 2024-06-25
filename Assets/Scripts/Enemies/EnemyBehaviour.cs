using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyData enemyData;
    public SpriteRenderer sprite;

    [HideInInspector]
    public Transform player;
    private GameObject target;
    private bool range;

    private GameObject weapon;
    private WeaponBehavior weaponBehavior;
    private Rigidbody2D rb;

    private EnemyState currentState = EnemyState.Idle;
    private Vector2 destination;
    private enum EnemyState
    {
        Idle,
        Shooting,
        Moving,
    }

    private Vector2 lastPos;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Give them their weapon
        weapon = Instantiate(enemyData.weapon, transform.position, Quaternion.identity, transform);
        weaponBehavior = weapon.GetComponent<WeaponBehavior>();

        // Set detection radius
        CircleCollider2D sightDist = transform.parent.gameObject.AddComponent<CircleCollider2D>();
        sightDist.radius = enemyData.detectionRadius;
        sightDist.isTrigger = true;

        // Set movement speed
        GetComponent<AIPath>().maxSpeed = enemyData.speed;

        // There were plans for more ranged movement types, but there was not enough time
        range = enemyData.behaviors == EnemyData.Behaviors.RANGE_SITTING_DUCK ||
                enemyData.behaviors == EnemyData.Behaviors.RANGE_MOVE_AFTER_SHOT ||
                enemyData.behaviors == EnemyData.Behaviors.RANGE_MOVE_AFTER_RELOAD;

        target = transform.parent.Find("Target").gameObject;

        lastPos = transform.position;
    }

    private void Update()
    {
        // Flip the sprite (without rigidbody because that doesn't work for some reason)
        if (lastPos.x < transform.position.x)
        {
            sprite.flipX = true;
        } 
        else
        {
            sprite.flipX = false;
        }
        lastPos = transform.position;

        // If the player is found
        if (player != null)
        {
            if (currentState == EnemyState.Idle)
            {
                // If the movement type is meelee just move to the player
                if (enemyData.behaviors == EnemyData.Behaviors.MELEE)
                {
                    target.transform.position = player.position;
                }
                else if (range)
                {
                    // The enemy is ranged, so shoot and keep a certain distance from the player
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

                    target.transform.position = destination;
                }
            }
            else if (currentState == EnemyState.Moving)
            {
                // Move to the destination
                target.transform.position = destination;
                if (Vector2.Distance(target.transform.position, destination) <= enemyData.shootDistance)
                {
                    currentState = EnemyState.Idle;
                }
            }
        }
    }

    // Do the enemy shooting
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
}
