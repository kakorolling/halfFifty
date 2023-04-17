using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//입력을 관리하는 클래스
public class InputManager : MonoBehaviour
{
    void Update()
    {
        Vector2 delta = Vector2.zero;

        //이동용: WASD, Left Shift
        if (Input.GetKey(KeyCode.W)) delta.y++;
        if (Input.GetKey(KeyCode.A)) delta.x--;
        if (Input.GetKey(KeyCode.S)) delta.y--;
        if (Input.GetKey(KeyCode.D)) delta.x++;
        GameManager.instance.playerCon.Move(delta, Input.GetKey(KeyCode.LeftShift));

        //퀵슬롯은 휠과 1~8까지

        //내가 화면을 클릭했을 때, 상호작용은 어디까지고 디테일하게 
        if (Input.GetMouseButtonDown(0))
        {
            //action0
        }
        if (Input.GetMouseButtonDown(1))
        {
            //action1
        }
    }
}
