using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerController playerCon;
    public InteractionController interactionCon;

    public static bool isPause = false; // pause 기본 false 상태

    public readonly Vector2Int chunkSize = new Vector2Int(16, 16); //타일청크의 사이즈는 (16*16)
    public Dictionary<Vector2Int, Chunk> loadedChunkDic; //key: Vector2Int, value: Chunk 불러오기

    void Awake()
    {
        //싱글톤 패턴
        if (instance != null) Destroy(gameObject);
        instance = this;//이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
        DontDestroyOnLoad(gameObject); //씬 전환이 되더라도 파괴되지 않게 한다.
    }

    public void LoadChunk(string chunkData) //청크를 로드하는 거
    {
        Vector2Int idx;
        Chunk chunk = null;
        //청크의 인덱스값으로 청크를 불러옴 loadedMap.Add(idx, chunk); 
    }

    // public (int terrain, List<GameObject> objList, bool collider) GetTileData(Vector2Int worldTileIdx) //좌표값을 받아서 그 좌표의 타일 정보 3가지를 반환
    // {
    //     Vector2Int chunkIdx = new Vector2Int(worldTileIdx.x / chunkSize.x, worldTileIdx.y / chunkSize.y); //청크 좌표값 구하기
    //     Chunk chunk = loadedMap[chunkIdx];
    //     Vector2Int chunkTileIdx = new Vector2Int(worldTileIdx.x % chunkSize.x, worldTileIdx.y % chunkSize.y); //한 청크 기준 타일 좌표값 구하기
    //     int terrain = chunk.terrainLayer[chunkTileIdx.x, chunkTileIdx.y]; //한 청크안의 좌표의 지형 값을 불러오기
    //     List<GameObject> objList = chunk.objLayer[chunkTileIdx.x, chunkTileIdx.y]; //한 청크안의 좌표의 오브젝트 리스트를 불러오기
    //     bool collider = chunk.colliderLayer[chunkTileIdx.x, chunkTileIdx.y]; //한 청크안의 좌표의 충돌 여부 불러오기
    //     return (terrain, objList, collider);
    // }

    public GameObject LoadObj(string json) //모든 오브젝트에는 데이터가 있어야함. 
    {
        GameObject obj = new GameObject();
        ObjData data = JsonUtility.FromJson<ObjData>(json);
        for (int i = 0; i < data.propertyNameArr.Length; i++)
        {
            Property property = obj.AddComponent(Type.GetType(data.propertyNameArr[i])) as Property;
            property.SetData(data.propertyDataArr[i]);
        }
        return obj;
    }

    public string SaveObj(GameObject obj)
    {

        ObjData data = new ObjData();

        Property[] propertyArr = obj.GetComponents<Property>();
        data.propertyNameArr = new string[propertyArr.Length];
        data.propertyDataArr = new string[propertyArr.Length];

        for (int i = 0; i < propertyArr.Length; i++)
        {
            data.propertyNameArr[i] = propertyArr[i].GetType().Name;
            data.propertyDataArr[i] = propertyArr[i].GetData();
        }
        string json = JsonUtility.ToJson(data);
        return json;
    }


    public bool MoveObj(GameObject obj, Vector2Int position) // 실제 격자공간에 오브젝트를 넣음, 그에 맞게 GameScene의 위치도 바꿈
    {
        return new bool();
    }

}

public class ObjData
{
    public string[] propertyNameArr; //프로퍼티들의 이름들 나열
    public string[] propertyDataArr; //프로퍼티들의 데이터 나열
}