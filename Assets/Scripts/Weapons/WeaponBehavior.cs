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
    [HideInInspector]
    public bool pickUp = false;

    private GameObject player;

    private int ammo;
    private float lastShotTime;

    // Handle audio
    public List<AudioClip> audio;
    private AudioSource audioPlayer;

    public void Start()
    {
        ammo = wData.ammo;
        lastShotTime = Time.time;

        // Make sure the trigger collider is set, so it can be picked up
        if (pickUp) 
        {
            GetComponent<BoxCollider2D>().enabled = true; 
        }

        audioPlayer = GetComponent<AudioSource>();
    }

    public void Shoot(float damangeMult)
    {
        // Check for reload time and time between shots
        if (
            (Time.time - lastShotTime >= wData.shootSpeed && ammo > 0) ||
            (Time.time - lastShotTime >= wData.reloadTime)
        )
        {
            // Check if it should reload
            reloading = false;
            shot = true;

            if (ammo <= 0)
            {
                ammo = wData.ammo;
                reloading = true;
            }

            // Instantiate bullet
            GameObject bullet = Instantiate(wData.bullet, transform.position, transform.rotation);
            Vector3 forwardDirection = transform.right;

            Rigidbody2D bRb = bullet.GetComponent<Rigidbody2D>();

            // Set layer mask of bullet and set it to include / exlude the right layer masks
            // If this is not done the bullt will hit the thing it's shot from, instantly despawn and hit the parent
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

            // Play random audio clip
            audioPlayer.Stop();
            AudioClip clip = audio[Random.Range(0, audio.Count - 1)];
            audioPlayer.clip = clip;
            audioPlayer.Play();

            ammo--;
            lastShotTime = Time.time;
        }
    }

    private void Update()
    {
        // Check for pickup (it is like this because on trigger stay was weird)
        if (player != null && Input.GetButtonDown("Pick Up"))
        {
            player.GetComponent<WeaponMovement>().PickUpWeapon(gameObject);
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && pickUp)
        {
            player = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }
}
