using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    //Chunk는 Tilemap의 일종의 관리자(Controller)
    //같은 오브젝트에 존재하는 컴포넌트인 Tilemap의 SetTile을 통해 Tile값 변경
    public Vector2Int idx; //청크에 대한 인덱스 -> 배열이 아닌 좌표값
    public int[,] terrainLayer; // 지형 코드 레이어
    public List<int>[,] objLayer; //오브젝트 레이어
    public bool[,] colliderLayer; //충돌체 감지 레이어
    Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    public void UpdateTileMap() //타일맵 업데이트
    {
        Vector2Int chunkSize = GameManager.instance.chunkSize;
        for (int x = 0; x < chunkSize.x; x++)
        {
            for (int y = 0; y < chunkSize.y; y++)
            {
                int terrain = terrainLayer[x, y];
                TileBase tile = GameManager.instance.terrainDic[terrain];
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}
