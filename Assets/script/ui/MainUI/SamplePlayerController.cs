using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float applySpeed;

    [SerializeField]
    private StatusController theStatusController;

    private bool isRun = false;
    private Animator animator;
    string animationState = "AnimationState";

    enum States
    {
        up = 1,
        down = 2,
        left = 3,
        right = 4
    }

    private void Start()
    {
        theStatusController = FindObjectOfType<StatusController>();
        applySpeed = walkSpeed;

        //rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TryRun();
        Move();

    
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            animator.speed = applySpeed / 2.0f;
        }
        else
        {
            animator.speed = 0f;
        }
    
    }

    private void Running()
    {
        isRun = true;
        theStatusController.DecreaseStamina(10);
        applySpeed = runSpeed;
    }

    private void RunningCanacel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && theStatusController.GetCurrentSP() > 0)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || theStatusController.GetCurrentSP() <= 0)
        {
            RunningCanacel();
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        transform.Translate(movement * applySpeed * Time.deltaTime);

        if (moveX > 0)
        {
            animator.SetInteger(animationState, (int)States.right);
        }
        else if (moveX < 0)
        {
            animator.SetInteger(animationState, (int)States.left);
        }
        else if (moveY > 0)
        {
            animator.SetInteger(animationState, (int)States.up);
        }
        else if (moveY > 0)
        {
            animator.SetInteger(animationState, (int)States.down);
        }
    }
}
