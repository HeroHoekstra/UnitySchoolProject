using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private ClearLevel cl;

    private bool firstEnter = true;

    private void Start()
    {
        cl = GetComponent<ClearLevel>();

        cl.Init();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player" && !firstEnter)
        {
            cl.Clear();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            firstEnter = false;
        }
    }
}
