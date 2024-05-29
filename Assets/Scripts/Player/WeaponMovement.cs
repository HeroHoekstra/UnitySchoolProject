using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public PlayerData playerData;

    private GameObject weapon;
    private WeaponBehavior wb = new WeaponBehavior();


    private void Start()
    {
        weapon = transform.Find("Weapon").gameObject;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0))
        {
            wb.Shoot(playerData.damageMultiplier);
        }
    }
}
