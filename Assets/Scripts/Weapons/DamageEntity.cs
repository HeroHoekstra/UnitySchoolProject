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
        if (other.tag == "Player" || other.tag == "Breakable")
        {
            other.gameObject.GetComponent<Health>().Hit(damage);

            Destroy(gameObject);
        }
    }
}
