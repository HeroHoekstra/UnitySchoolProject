using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [HideInInspector]
    public float damageMult;

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
