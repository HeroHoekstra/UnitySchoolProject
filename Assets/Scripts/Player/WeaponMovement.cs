using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    private PlayerData playerData;
    public GameObject weapon;

    private WeaponBehavior wb;

    private void Start()
    {
        playerData = GameObject.Find("MainManager").GetComponent<GameManager>().playerData;

        weapon = Instantiate(weapon, transform.position + new Vector3(0, 0, -1), Quaternion.identity, transform);
        wb = weapon.GetComponent<WeaponBehavior>();
        wb.parent = gameObject;
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
