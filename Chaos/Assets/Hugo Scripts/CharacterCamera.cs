using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField]
    CameraMovement camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("RoomSwitch"))
        {
            camera.focus = collision.transform.parent.gameObject;
        }
    }
}
