using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEntity : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Breakable") &&
            other.transform.parent != transform.parent
        )
        {
            Debug.Log(other.transform.parent == transform.parent);
            other.gameObject.GetComponent<Health>().Hit(damage);

            Destroy(gameObject);
        }
    }
}
