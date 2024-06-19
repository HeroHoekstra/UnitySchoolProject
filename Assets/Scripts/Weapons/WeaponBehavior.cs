using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public WeaponData wData;

    [HideInInspector]
    public bool shot;
    [HideInInspector]
    public bool reloading;

    public GameObject parent;

    private int ammo;
    private float lastShotTime;

    public void Start()
    {
        ammo = wData.ammo;
        lastShotTime = Time.time;
    }

    public void Shoot(float damangeMult)
    {
        if (
            (Time.time - lastShotTime >= wData.shootSpeed && ammo > 0) ||
            (Time.time - lastShotTime >= wData.reloadTime)
        )
        {
            reloading = false;
            shot = true;

            if (ammo <= 0)
            {
                ammo = wData.ammo;
                reloading = true;
            }

            GameObject bullet = Instantiate(wData.bullet, transform.position, Quaternion.identity);
            Vector3 forwardDirection = transform.right;

            Rigidbody2D bRb = bullet.GetComponent<Rigidbody2D>();
            LayerMask thisLayer = 1 << gameObject.layer;
            LayerMask thatLayer = LayerMask.GetMask("Ignore Raycast");
            LayerMask otherLayer = LayerMask.GetMask("Player") == 1 << gameObject.layer ? LayerMask.GetMask("Enemy") : LayerMask.GetMask("Player");
            bRb.velocity = forwardDirection * wData.bulletSpeed;
            bRb.excludeLayers |= thisLayer;
            bRb.excludeLayers |= thatLayer;
            bRb.includeLayers |= otherLayer;

            CircleCollider2D coll = bullet.GetComponent<CircleCollider2D>();
            coll.excludeLayers |= thisLayer;
            coll.excludeLayers |= thatLayer;
            coll.includeLayers |= otherLayer;

            bullet.GetComponent<BulletBehaviour>().damageMult = damangeMult;

            ammo--;
            lastShotTime = Time.time;
        }
    }
}
