using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    private PlayerData playerData;

    [HideInInspector]
    public GameObject weapon;

    private WeaponBehavior wb;

    private void Start()
    {
        // Instantiate data and weapon
        playerData = GameObject.Find("MainManager").GetComponent<GameManager>().playerData;
        weapon = playerData.weapon;

        PickUpWeapon(playerData.weapon);
    }

    // Picks up the weapon (still needs to remove the other weapon)
    public void PickUpWeapon(GameObject newWeapon)
    {
        playerData.weapon = newWeapon.GetComponent<WeaponBehavior>().wData.prefab;

        weapon = Instantiate(newWeapon, transform.position + new Vector3(0, 0, -1), Quaternion.identity, transform);
        weapon.layer |= LayerMask.NameToLayer("Player");
        wb = weapon.GetComponent<WeaponBehavior>();
    }

    void Update()
    {
        // Set the rotation to the mouse relative to the player
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Shoot if shot
        if (Input.GetMouseButtonDown(0))
        {
            wb.Shoot(playerData.damageMultiplier);
        }
    }
}
