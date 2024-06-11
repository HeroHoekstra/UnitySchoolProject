using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public WeaponData weaponData;
    public GameObject parent;

    [HideInInspector]
    public bool reloading;
    [HideInInspector]
    public bool shot;

    private int ammo;
    private float lastShotTime;

    public void Start()
    {
        ammo = weaponData.ammo;

        lastShotTime = Time.time;
    }



    public void Shoot(float damageMult)
    {
        if (
            (Time.time - lastShotTime >= weaponData.shootSpeed && ammo > 0) ||
            (Time.time - lastShotTime >= weaponData.reloadTime)
        ) {
            reloading = false;
            shot = true;

            if (ammo <= 0)
            {
                ammo = weaponData.ammo;
                reloading = true;
            }

            GameObject bullet = Instantiate(weaponData.bullet, transform.position, transform.rotation, transform.parent.parent);
            DamageEntity de = bullet.GetComponent<DamageEntity>();
            
            de.damage = weaponData.damage * damageMult;
            de.parent = parent;
            Vector3 forwardDirection = transform.right;
            bullet.GetComponent<Rigidbody2D>().velocity = forwardDirection * weaponData.bulletSpeed;

            ammo--;
            lastShotTime = Time.time;
        }
    }
}
