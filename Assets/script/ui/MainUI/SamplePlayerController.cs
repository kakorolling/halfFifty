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

    //QuickSlotController의 Usingitem을 사용하기 위한 참조
    private QuickSlotController QuickSlotController;

    public GameObject PlayerSample;
   
    
    enum FarmerBasicStates
    {
        idle = 0,
        up = 1,
        down = 2,
        left = 3,
        right = 4
    }

    enum FarmerWoodAxeStates
    {
        idle = 5,
        up = 6,
        down = 7,
        left = 8,
        right = 9
    }


    private void Start()
    {
        theStatusController = FindObjectOfType<StatusController>();
        applySpeed = walkSpeed;

        //rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        QuickSlotController = FindObjectOfType<QuickSlotController>();
        
        
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
        Item Usingitem = QuickSlotController.Usingitem;
        Debug.Log(Usingitem.GetImageName());
        Debug.Log(PlayerSample.GetComponent<SpriteRenderer>().sprite);
        
        // 기본 농부
        if(Usingitem.GetName() == "")
        {
            if (moveX > 0)
            {
                animator.SetInteger(animationState, (int)FarmerBasicStates.right);
            }
            else if (moveX < 0)
            {
                animator.SetInteger(animationState, (int)FarmerBasicStates.left);
            }
            else if (moveY > 0)
            {
                animator.SetInteger(animationState, (int)FarmerBasicStates.up);
            }
            else if (moveY < 0)
            {
                animator.SetInteger(animationState, (int)FarmerBasicStates.down);
            }
            /*
            else
            {
                animator.SetInteger(animationState, (int)FarmerBasicStates.idle);
            }
            */
        }

        
        else if(Usingitem.GetImageName() == "WoodAxe")
        {
            animator.enabled = false;
            PlayerSample.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprite/PlayerImage/Farmer_WoodAxe/Farmer_WoodAxe_Basic");
            animator.SetInteger(animationState, (int)FarmerWoodAxeStates.idle);
            animator.enabled = true;
            
            
            if (moveX > 0)
            {
                animator.SetInteger(animationState, (int)FarmerWoodAxeStates.right);
            }
            else if (moveX < 0)
            {
                animator.SetInteger(animationState, (int)FarmerWoodAxeStates.left);
            }
            else if (moveY > 0)
            {
                animator.SetInteger(animationState, (int)FarmerWoodAxeStates.up);
            }
            else if (moveY < 0)
            {
                animator.SetInteger(animationState, (int)FarmerWoodAxeStates.down);
            }
           
            
            
        }
        
    }   
            
    
}
