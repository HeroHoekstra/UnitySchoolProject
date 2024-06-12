using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEntity : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public GameObject parent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if 
        (
            // Check tag
            other.tag != parent.tag && (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Breakable") &&
            // Check gameObject
            other.gameObject != parent && other.gameObject != gameObject
            
        )
        {
            other.gameObject.GetComponent<Health>().Hit(damage);

            Destroy(gameObject);
        }
    }
}
