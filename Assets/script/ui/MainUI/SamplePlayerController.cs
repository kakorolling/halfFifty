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

    // 충돌 감지
    private Rigidbody2D rb;

    private Animator animator;
    string animationState = "AnimationState";

    //QuickSlotController의 Usingitem을 사용하기 위한 참조
    private QuickSlotController QuickSlotController;

    public GameObject PlayerSample;

    private Logging LoggingManager;
    bool isLogging;

    private Fishing FishingManager;
    bool isFishing;

    private float animationDuration;
   
    Item Usingitem;

    string wearingClothes;
    
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

    enum FarmerFishingRodStates
    {
        idle = 10,
        up = 11,
        down = 12,
        left = 13,
        right = 14
    }

    enum FarmerLoggingStates
    {
        left = 15,
        right = 16
    }

    // animationStates 15 ~ 16 is in Logging.cs
    // animationStates 17 ~ 20 is in Fishing.cs

    enum ShuBasicStates
    {
        idle = 21,
        up = 22,
        down = 23,
        left = 24,
        right = 25
    }

    enum ShuWoodAxeStates
    {
        idle = 26,
        up = 27,
        down = 28,
        left = 29,
        right = 30
    }

    enum ShuFishingRodStates
    {
        idle = 31,
        up = 32,
        down = 33,
        left = 34,
        right = 35
    }
    


    private void Start()
    {
        theStatusController = FindObjectOfType<StatusController>();
        applySpeed = walkSpeed;

        //rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        QuickSlotController = FindObjectOfType<QuickSlotController>();
        LoggingManager = FindObjectOfType<Logging>();
        FishingManager = FindObjectOfType<Fishing>();
        
        
        
    }

    private void Update()
    {
        TryRun();
        MoveAndChangeAnimation();

        this.Usingitem = QuickSlotController.Usingitem;
        
        // if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        // {
        //     animator.speed = applySpeed / 2.0f;
        // }
        // else
        // {
        //     animator.speed = 1f;
        // }

        if(Usingitem.GetName() == "" || Usingitem.GetName() == "나무 도끼")
        {
            isLogging = LoggingManager.isLogging;
            if(Input.GetKeyDown(KeyCode.F) && !isLogging)
            {   
                LoggingManager.TryStartLogging();
                
            }
        }

        if(Usingitem.GetName() == "나무 낚싯대")
        {
            isFishing = FishingManager.isFishing;
            if(Input.GetKeyDown(KeyCode.F) && !isFishing)
            {   
                FishingManager.TryStartFishing();
                
            }
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

    private void MoveAndChangeAnimation()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        transform.Translate(movement * applySpeed * Time.deltaTime);

        this.Usingitem = QuickSlotController.Usingitem;
        this.wearingClothes = QuickSlotController.wearingClothes;
       
        // 기본 농부
        if((Usingitem.GetName() == "") && (wearingClothes == "농부 의상"))
        {
            animator.enabled = false;
            PlayerSample.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/PlayerImage/Farmer_Basic/Farmer_Basic_Basic");
            animator.SetInteger(animationState, (int)FarmerBasicStates.idle);
            animator.enabled = true;

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

        
        else if((Usingitem.GetImageName() == "WoodAxe") && (wearingClothes == "농부 의상"))
        {
            animator.enabled = false;
            PlayerSample.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/PlayerImage/Farmer_WoodAxe/Farmer_WoodAxe_Basic");
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

        else if((Usingitem.GetImageName() == "FishingRod") && (wearingClothes == "농부 의상"))
        {
            animator.enabled = false;
            PlayerSample.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/PlayerImage/Farmer_FishingRod/Farmer_FishingRod_Basic");
            animator.SetInteger(animationState, (int)FarmerFishingRodStates.idle);
            animator.enabled = true;

            if (moveX > 0)
            {
                animator.SetInteger(animationState, (int)FarmerFishingRodStates.right);
            }
            else if (moveX < 0)
            {
                animator.SetInteger(animationState, (int)FarmerFishingRodStates.left);
            }
            else if (moveY > 0)
            {
                animator.SetInteger(animationState, (int)FarmerFishingRodStates.up);
            }
            else if (moveY < 0)
            {
                animator.SetInteger(animationState, (int)FarmerFishingRodStates.down);
            }
           
        }

        else if((Usingitem.GetImageName() == "") && (wearingClothes == "슈 의상"))
        {
            animator.enabled = false;
            PlayerSample.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/PlayerImage/Shu_Basic/Shu_Basic_Basic");
            animator.SetInteger(animationState, (int)ShuBasicStates.idle);
            animator.enabled = true;

            if (moveX > 0)
            {
                animator.SetInteger(animationState, (int)ShuBasicStates.right);
            }
            else if (moveX < 0)
            {
                animator.SetInteger(animationState, (int)ShuBasicStates.left);
            }
            else if (moveY > 0)
            {
                animator.SetInteger(animationState, (int)ShuBasicStates.up);
            }
            else if (moveY < 0)
            {
                animator.SetInteger(animationState, (int)ShuBasicStates.down);
            }

        } 
        
        else if((Usingitem.GetImageName() == "WoodAxe") && (wearingClothes == "슈 의상"))
        {
            Debug.Log("good");
            animator.enabled = false;
            PlayerSample.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/PlayerImage/Shu_WoodAxe/Shu_WoodAxe_Basic");
            animator.SetInteger(animationState, (int)ShuWoodAxeStates.idle);
            animator.enabled = true;

            if (moveX > 0)
            {
                animator.SetInteger(animationState, (int)ShuWoodAxeStates.right);
            }
            else if (moveX < 0)
            {
                animator.SetInteger(animationState, (int)ShuWoodAxeStates.left);
            }
            else if (moveY > 0)
            {
                animator.SetInteger(animationState, (int)ShuWoodAxeStates.up);
            }
            else if (moveY < 0)
            {
                animator.SetInteger(animationState, (int)ShuWoodAxeStates.down);
            }

        }

        else if((Usingitem.GetImageName() == "FishingRod") && (wearingClothes == "슈 의상"))
        {
            animator.enabled = false;
            PlayerSample.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/PlayerImage/Shu_FishingRod/Shu_FishingRod_Basic");
            animator.SetInteger(animationState, (int)ShuFishingRodStates.idle);
            animator.enabled = true;

            if (moveX > 0)
            {
                animator.SetInteger(animationState, (int)ShuFishingRodStates.right);
            }
            else if (moveX < 0)
            {
                animator.SetInteger(animationState, (int)ShuFishingRodStates.left);
            }
            else if (moveY > 0)
            {
                animator.SetInteger(animationState, (int)ShuFishingRodStates.up);
            }
            else if (moveY < 0)
            {
                animator.SetInteger(animationState, (int)ShuFishingRodStates.down);
            }

        } 
    }
            
    
}
