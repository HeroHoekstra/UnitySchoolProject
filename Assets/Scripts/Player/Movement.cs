using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerData playerData;
    public float moveSpeed;

    private void Start()
    {
        playerData = GameObject.Find("MainManager").GetComponent<GameManager>().playerData;

        moveSpeed = playerData.speed;
    }

    private void FixedUpdate()
    {
        float movementX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float movementY = Input.GetAxisRaw("Vertical") * moveSpeed;

        transform.position = new Vector2(transform.position.x, transform.position.y) + new Vector2(movementX, movementY);
    }
}
