using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyData enemyData;

    private Transform player;
    private bool range;

    public void Start()
    {
        // Set detection radius
        CircleCollider2D sightDist = gameObject.AddComponent<CircleCollider2D>();
        sightDist.radius = enemyData.detectionRadius;
        sightDist.isTrigger = true;

        // Set movement speed
        transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = enemyData.speed;

        range = enemyData.behaviors == EnemyData.Behaviors.RANGE_SITTING_DUCK ||
                enemyData.behaviors == EnemyData.Behaviors.RANGE_MOVE_AFTER_SHOT;
    }

    private void Update()
    {
        if (player != null)
        {
            if (enemyData.behaviors == EnemyData.Behaviors.MELEE)
            {
                // Just go towards the player
                transform.parent.Find("Target").position = player.position;
            }
            else if (range) {
                // Go to the player but kep a distance
                Vector2 direction = player.position - transform.position;
                float distanceToPlayer = direction.magnitude;

                Vector2 targetPoint = (Vector2)player.position - direction.normalized * enemyData.keepDistance;
                transform.parent.Find("Target").position = targetPoint;
            }
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
