using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//입력을 관리하는 클래스
public class InputManager : MonoBehaviour
{
    PlayerController playerCon;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("up!");
            playerCon.Move(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("left!");
            playerCon.Move(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("down!");
            playerCon.Move(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("right!");
            playerCon.Move(Vector2.right);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerCon.Run();
        }
    }
}
