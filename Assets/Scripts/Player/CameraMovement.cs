using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform trans;
    public float camMoveSpeed = 0.125f;

    void LateUpdate()
    {
        // Lerp to player
        if (trans != null)
        {
            Vector3 desiredPos = new Vector3(trans.position.x, trans.position.y, transform.position.z);
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, camMoveSpeed);

            transform.position = smoothedPos;
        }
    }
}
