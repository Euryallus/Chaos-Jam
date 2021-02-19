using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject focus;

    private void Update()
    {
        Vector3 focusPos = focus.transform.position;
        focusPos.z = transform.position.z;

        if((focusPos - transform.position).magnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, focusPos, 5f * Time.deltaTime);
        }
        else
        {
            transform.position = focusPos;
        }
    }
}
