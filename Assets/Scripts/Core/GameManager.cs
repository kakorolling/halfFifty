using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;
using System.Text.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static readonly Vector2Int chunkSize = new Vector2Int(32, 32); //타일청크의 사이즈는 (32*32)
    public static readonly Vector2Int worldSize = chunkSize * 32;
    public GameObject gridObj;
    public GameObject chunkPf;
    public TileBase testTerrain;

    GameData gameData; //게임의 모든 정보(일종의 DB 같은거)
    public static bool isPaused; // pause 기본 false 상태
    public Dictionary<Vector2Int, Chunk> chunkDic; //key: chunk의 idx, value: 로드된 Chunk
    public Dictionary<int, GameObject> objDic;
    public Dictionary<int, RuleTile> terrainDic;

    void Awake()
    {
        //싱글톤 패턴
        if (instance != null) Destroy(gameObject);
        instance = this;//이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
        DontDestroyOnLoad(gameObject); //씬 전환이 되더라도 파괴되지 않게 한다.

        isPaused = false;
        chunkDic = new Dictionary<Vector2Int, Chunk>();
        objDic = new Dictionary<int, GameObject>();
        terrainDic = new Dictionary<int, RuleTile>();
        RuleTile[] tiles = Resources.LoadAll<RuleTile>("Tiles");
        foreach (RuleTile tile in tiles)
        {
            terrainDic.Add(Int32.Parse(tile.name), tile);
        }
        //  CreateDummyGameData();
        LoadGame("temp");
    }

    void OnApplicationQuit()
    {
        SaveGame("temp");
    }

    private void CreateDummyGameData()
    {
        gameData = new GameData();
        gameData.terrainLayer = new int[1024][].Select(e => new int[1024]).ToArray();
        gameData.objLayer = new List<int>[1024][].Select(e => new List<int>[1024]).ToArray();
        gameData.colliderLayer = new bool[1024][].Select(e => new bool[1024]).ToArray();
        gameData.objDic = new Dictionary<int, string>();
        for (int x = 0; x < 1024; x++)
        {
            for (int y = 0; y < 1024; y++)
            {
                gameData.terrainLayer[x][y] = 1;
                gameData.objLayer[x][y] = new List<int>();
                gameData.colliderLayer[x][y] = false;
            }
        }
    }

    private void UpdateChunk(Chunk chunk)
    {
        Vector2Int offset = chunk.idx * chunkSize;
        for (int x = 0; x < chunkSize.x; x++)
        {
            for (int y = 0; y < chunkSize.y; y++)
            {
                chunk.terrainLayer[x, y] = gameData.terrainLayer[x + offset.x][y + offset.y];
                chunk.objLayer[x, y] = gameData.objLayer[x + offset.x][y + offset.y];
                chunk.colliderLayer[x, y] = gameData.colliderLayer[x + offset.x][y + offset.y];
            }
        }
        chunk.UpdateTileMap();
    }

    public void LoadChunk(Vector2Int idx) //청크를 로드하는 거 -> GameData에서 동기화
    {
        Vector2 chunkPos = idx * chunkSize;
        Chunk chunk = Instantiate(chunkPf, chunkPos, Quaternion.identity).GetComponent<Chunk>();
        chunk.idx = idx;
        chunkDic.Add(idx, chunk);
        UpdateChunk(chunk);
        chunk.transform.parent = gridObj.transform;
    }

    public void UnloadChunk(Vector2Int idx)
    {
        Chunk chunk = chunkDic[idx];
        chunkDic.Remove(idx);
        Destroy(chunk.gameObject);
    }

    private void UpdateAllChunk()
    {
        List<Chunk> chunkList = chunkDic.Values.ToList();
        for (int i = 0; i < chunkList.Count; i++)
        {
            UpdateChunk(chunkList[i]);
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
        string json = JsonSerializer.Serialize(data);
        return json;
    }

    public void LoadObj(string json) //모든 오브젝트에는 데이터가 있어야함. 
    {
        GameObject obj = new GameObject();
        ObjData data = JsonSerializer.Deserialize<ObjData>(json);
        for (int i = 0; i < data.propertyNameArr.Length; i++)
        {
            Property property = obj.AddComponent(Type.GetType(data.propertyNameArr[i])) as Property;
            property.SetData(data.propertyDataArr[i]);
        }
        Common common = obj.GetComponent<Common>();
        objDic.Add(common.id, obj);
    }

    public void SaveGame(string saveFileName)
    {
        string path = $"{Application.persistentDataPath}/{saveFileName}.save";
        System.IO.File.WriteAllText(path, JsonSerializer.Serialize(gameData));
        Debug.LogWarning("Saved as" + path);
    }
    public void LoadGame(string saveFileName)
    {
        string path = $"{Application.persistentDataPath}/{saveFileName}.save";
        if (!File.Exists(path)) Debug.LogError("Save file not found in " + path);
        string json = System.IO.File.ReadAllText(path);
        gameData = JsonSerializer.Deserialize<GameData>(json);
    }
    public List<string> GetSaveFileNameList() //이름 목록만 가져옴
    {
        List<string> saveFileNameList =
        Directory.GetFiles(Application.persistentDataPath, "*.save")
        .Select(e => Path.GetFileName(e))
        .ToList();
        return saveFileNameList;
    }

    public void SetTerrain(Vector2Int coord, int terrain) //지형값을 바꿔야함->바꿀좌표, 뭐로바꿀지
    {
        gameData.terrainLayer[coord.x][coord.y] = terrain; //이거의 int값
        Vector2Int chunkIdx = new Vector2Int(Mathf.FloorToInt(coord.x / chunkSize.x), Mathf.FloorToInt(coord.y / chunkSize.y));
        UpdateChunk(chunkDic[chunkIdx]);
    }

    public void AddObj(Vector2Int[] coord, Common common)
    {
        //gameData.objLayer[coord.x][coord.y].Add(common.id);
    }
    public void RemoveObj(Vector2Int[] coord, Common common)
    {
        //gameData.objLayer[coord.x][coord.y].Remove(common.id);
    }
}

public class GameData
{
    //map data
    public int[][] terrainLayer { get; set; } // 지형 코드 레이어
    public List<int>[][] objLayer { get; set; } //오브젝트 레이어
    public bool[][] colliderLayer { get; set; } //충돌체 감지 레이어
    //object data
    public Dictionary<int, string> objDic { get; set; }
}
public class ObjData
{
    public string[] propertyNameArr; //프로퍼티들의 이름들 나열
    public string[] propertyDataArr; //프로퍼티들의 데이터 나열
}