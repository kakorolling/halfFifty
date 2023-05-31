using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        Vector2Int delta = Vector2Int.zero;
        if (Input.GetKey(KeyCode.W)) delta.y++;
        if (Input.GetKey(KeyCode.A)) delta.x--;
        if (Input.GetKey(KeyCode.S)) delta.y--;
        if (Input.GetKey(KeyCode.D)) delta.x++;
        if (delta == Vector2Int.zero) (GameManager.instance.playerObj)?.GetComponent<Mover>()?.StopMove();
        else (GameManager.instance.playerObj)?.GetComponent<Mover>()?.StartMove(delta);

        // //퀵슬롯은 휠과 1~8까지

        // //내가 화면을 클릭했을 때, 상호작용은 어디까지고 디테일하게 
        // if (Input.GetMouseButtonDown(0))
        // {
        //     //action0
        // }
        // if (Input.GetMouseButtonDown(1))
        // {
        //     //action1
        // }
    }
}
