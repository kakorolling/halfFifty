using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2Int idx; //청크에 대한 인덱스 -> 배열이 아닌 좌표값

    public int[,] terrainLayer; // 지형 코드 레이어
    public List<GameObject>[,] objLayer; //오브젝트 레이어
    public bool[,] colliderLayer; //충돌체 감지 레이어
}
