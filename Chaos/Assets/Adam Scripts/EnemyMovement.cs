using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject targetObject;
    public float speed = 3;

    private float step;
    private Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;

        targetPosition = targetObject.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }
}
