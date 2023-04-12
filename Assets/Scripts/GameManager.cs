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

    GameData gameData = new GameData(); //게임의 모든 정보(일종의 DB 같은거)
    public static bool isPause = false; // pause 기본 false 상태
    public readonly Vector2Int chunkSize = new Vector2Int(32, 32); //타일청크의 사이즈는 (32*32)
    public Dictionary<Vector2Int, Chunk> loadedChunkDic; //key: Vector2Int, value: Chunk 불러오기
    public Dictionary<int, GameObject> loadedObjDic;

    void Awake()
    {
        //싱글톤 패턴
        if (instance != null) Destroy(gameObject);
        instance = this;//이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
        DontDestroyOnLoad(gameObject); //씬 전환이 되더라도 파괴되지 않게 한다.
    }


    public void LoadChunk(Vector2Int idx) //청크를 로드하는 거 -> 
    {
        Chunk chunk = new Chunk();
        chunk.idx = idx;
        Vector2Int offset = idx * chunkSize;
        for (int x = 0; x < chunkSize.x; x++)
        {
            for (int y = 0; y < chunkSize.y; y++)
            {
                chunk.terrainLayer[x, y] = gameData.terrainLayer[x + offset.x, y + offset.y];
                chunk.objLayer[x, y] = gameData.objLayer[x + offset.x, y + offset.y].Select(id =>
                {
                    if (loadedObjDic[id] == null)
                    {
                        GameObject loadedObj = LoadObj(gameData.objDic[id]);
                        loadedObjDic.Add(id, loadedObj);
                    }
                    return loadedObjDic[id];
                })
                .ToList();
                chunk.colliderLayer[x, y] = gameData.colliderLayer[x + offset.x, y + offset.y];
            }
        }
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

    public bool MoveObj(GameObject obj, Vector2Int position) // 실제 격자공간에 오브젝트를 넣음, 그에 맞게 GameScene의 위치도 바꿈
    {
        return new bool();
    }

}

public class GameData
{
    //map data
    public int[,] terrainLayer = new int[1024, 1024]; // 지형 코드 레이어
    public List<int>[,] objLayer = new List<int>[1024, 1024]; //오브젝트 레이어
    public bool[,] colliderLayer = new bool[1024, 1024]; //충돌체 감지 레이어

    //object data
    public Dictionary<int, string> objDic = new Dictionary<int, string>();
}

public class ObjData
{
    public string[] propertyNameArr; //프로퍼티들의 이름들 나열
    public string[] propertyDataArr; //프로퍼티들의 데이터 나열
}