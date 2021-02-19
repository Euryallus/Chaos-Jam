using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum States
    {
        active,
        idle,
        patrol
    }
    
    public float speed = 1.5f;

    private GameObject  playerObject;
    private float       step;
    private Vector2     targetPosition;
    private Vector3     targetDirection;
    private States      state = States.idle;

    // Start is called before the first frame update
    void Start()
    {
        state = States.active;

        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.active)
        {
            step = speed * Time.deltaTime;

            targetPosition = playerObject.transform.position;

            targetDirection = targetPosition - new Vector2(transform.position.x, transform.position.y);

            GetComponent<Animator>().SetFloat("TurnSide", targetDirection.x); 
            
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
    }
}
