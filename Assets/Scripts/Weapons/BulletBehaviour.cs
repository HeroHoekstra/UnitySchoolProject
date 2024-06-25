using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float despawnTime;

    [HideInInspector]
    public float damageMult;

    // Destroy bullet after time to avoid too many gameObjects
    void Start()
    {
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);

        Destroy(gameObject);
    }

    // If the appropriate thing is hit, do manage the hit and destroy this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Player")
        {
            Health health = other.gameObject.GetComponent<Health>();

            health.Hit(damageMult);

            Destroy(gameObject);
        }
    }
}
