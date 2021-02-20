using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum States
    {
        chase,
        attack,
        idle,
        patrol
    }
    
    public float speed = 1.5f;

    private GameObject  playerObject;
    private float       step;
    private Vector2     targetPosition;
    public  Vector3     targetDirection;
    private States      state = States.idle;
    private Animator    animator;

    // Start is called before the first frame update
    void Start()
    {
        state = States.chase;

        playerObject = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = playerObject.transform.position;

        targetDirection = targetPosition - new Vector2(transform.position.x, transform.position.y);
       
        if (targetDirection.x > 0 && targetDirection.y <= 1.5 && targetDirection.y >= -1.5)
        {
            animator.SetBool("LookingRight", true);
            animator.SetBool("LookingLeft", false);
            animator.SetBool("LookingUp", false);
            animator.SetBool("LookingDown", false);
        }
        if (targetDirection.x < 0 && targetDirection.y <= 1.5 && targetDirection.y >= -1.5)
        {
            animator.SetBool("LookingLeft", true);
            animator.SetBool("LookingRight", false);
            animator.SetBool("LookingUp", false);
            animator.SetBool("LookingDown", false);
        }

        if (targetDirection.y > 0 && targetDirection.x <= 1.5 && targetDirection.x >= -1.5)
        {
            animator.SetBool("LookingUp", true);
            animator.SetBool("LookingDown", false);
            animator.SetBool("LookingLeft", false);
            animator.SetBool("LookingRight", false);
        }
        if (targetDirection.y < 0 && targetDirection.x <= 1.5 && targetDirection.x >= -1.5)
        {
            animator.SetBool("LookingDown", true);
            animator.SetBool("LookingUp", false);
            animator.SetBool("LookingLeft", false);
            animator.SetBool("LookingRight", false);
        }

        if (state == States.chase)
        {
            step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
    }
}
