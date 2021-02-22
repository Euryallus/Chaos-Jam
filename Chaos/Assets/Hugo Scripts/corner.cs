using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("RoomSwitch"))
        {
            Destroy(gameObject);
        }

        if(collision.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
