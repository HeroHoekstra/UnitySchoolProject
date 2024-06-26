using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapon Settings/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public GameObject prefab;

    public int ammo;
    public float damage;

    public GameObject bullet;
    public float bulletSpeed;

    public float shootSpeed;
    public float reloadTime;

    public float playerBuff;
}
