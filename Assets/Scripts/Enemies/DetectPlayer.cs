using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    // This is a really basic player detection script
    // The only reason this is a sepperate script is because of how the enemy works

    public GameObject enemy;
    private EnemyBehaviour eb;

    private void Start()
    {
        eb = enemy.GetComponent<EnemyBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            eb.player = other.transform;
        }
    }
}
