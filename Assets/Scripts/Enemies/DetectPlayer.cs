using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    GameObject go;
    EnemyBehaviour eb;

    private void Start()
    {
        go = transform.Find("Enemy").gameObject;
        eb = go.GetComponent<EnemyBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            eb.player = other.transform;
        }
    }
}
