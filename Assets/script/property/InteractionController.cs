using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    // Vector2Int[] offsets = new Vector2Int[]{
    //     new Vector2Int(-1,1),
    //     new Vector2Int(0,1),
    //     new Vector2Int(1,1),
    //     new Vector2Int(-1,0),
    //     new Vector2Int(1,0),
    //     new Vector2Int(-1,-1),
    //     new Vector2Int(0,-1),
    //     new Vector2Int(1,-1),
    // };

    // void Update()
    // {
    //     Vector2Int playerCoord = GameManager.instance.playerCon.Coord;
    //     Vector2Int[] interactableCoords = GetInteractableCoords(playerCoord);
    //     //마우스를 대면 커서가 보이도록
    //     Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     int selectedCoordIdx = -1; //0~7까지 있는 Idx 값에서 선택되지 않은 값
    //     for (int i = 0; i < interactableCoords.Length; i++)
    //     {
    //         if (point.x >= interactableCoords[i].x
    //         && point.x < interactableCoords[i].x + 1
    //         && point.y >= interactableCoords[i].y
    //         && point.y < interactableCoords[i].y + 1)
    //         {
    //             selectedCoordIdx = i;
    //             break;
    //         }
    //     }
    //     if (selectedCoordIdx != -1)
    //     {
    //         Debug.Log(interactableCoords[selectedCoordIdx]);
    //     }
    // }

    // Vector2Int[] GetInteractableCoords(Vector2Int position)
    // {
    //     Vector2Int[] interactableCoords = new Vector2Int[offsets.Length];
    //     for (int i = 0; i < offsets.Length; i++)
    //     {
    //         interactableCoords[i] = position + offsets[i];
    //     }
    //     return interactableCoords;
    // }
}
