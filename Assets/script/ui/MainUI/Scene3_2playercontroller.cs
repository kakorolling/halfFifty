using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3_2playercontroller : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float applySpeed;

   #pragma warning disable CS0414 //경고 무시
    private bool isRun = false;

    [SerializeField]
    private StatusController theStatusController;
    void Start()
    {
        theStatusController = FindObjectOfType<StatusController>();
        applySpeed= walkSpeed;
    }

    
    void Update()
    {
        TryRun();
        Move();
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
        if(Input.GetKey(KeyCode.LeftShift) && theStatusController.GetCurrentSP() > 0)
        {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || theStatusController.GetCurrentSP() <= 0)
        {
            RunningCanacel();
        }
    }

    private void Move()
    {
        float _movex = Input.GetAxisRaw("Horizontal");
        float _movey = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(_movex,_movey)*Time.deltaTime*applySpeed);
    }

}
