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

            if(collision.transform.parent.gameObject.GetComponent<miniMapCover>())
            {
                collision.transform.parent.gameObject.GetComponent<miniMapCover>().uncover();
            }

            camera.focus = collision.transform.parent.gameObject;
        }
    }
}
