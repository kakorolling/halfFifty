using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Dynamic;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    //singleton
    public static GameManager instance = null;
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;//이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
        DontDestroyOnLoad(gameObject); //씬 전환이 되더라도 파괴되지 않게 한다.
    }
    SettingData settingData;
    GameData gameData;
    public GameObject playerObj { get; private set; }
    void Start()
    {
        if (!LoadSetting("default")) CreateDefaultSetting();
        if (!LoadGame("dummy")) CreateDummyGame();
        else StartGame();
        MapEditor.instance.Init(gameData.mapDic);
    }
    void OnApplicationQuit()
    {
        SaveSetting("default");
        SaveGame("dummy");
    }

    //setting
    void CreateDefaultSetting()
    {
        settingData = new SettingData()
        {
            chunkSize = new Vector2Int(32, 32),
            seamlessRange = 2,
            audioVolume = 100
        };
    }
    bool SaveSetting(string settingName)
    {
        if (settingData == null)
        {
            Debug.LogError("There is no setting data");
            return false;
        }
        string filePath = $"{Application.persistentDataPath}/{settingName}.setting_data";
        string json = JsonConvert.SerializeObject(settingData);
        System.IO.File.WriteAllText(filePath, json);
        Debug.LogWarning("Setting saved as" + filePath);
        return true;
    }
    bool LoadSetting(string settingName)
    {
        string filePath = $"{Application.persistentDataPath}/{settingName}.setting_data";
        if (!File.Exists(filePath))
        {
            Debug.LogError("Setting file not found in " + filePath);
            return false;
        }
        string json = System.IO.File.ReadAllText(filePath);
        settingData = JsonConvert.DeserializeObject<SettingData>(json);
        if (settingData == null)
        {
            Debug.LogError("Setting file is invalid");
            return false;
        }
        return true;
    }

    //game
    void CreateDummyGame()
    {
        Guid mapId = Guid.NewGuid();
        Position playerObjPos = new Position(mapId, 512, 512);
        gameData = new GameData();
        gameData.mapDic = new Dictionary<Guid, MapData>();
        gameData.objDic = new Dictionary<Guid, ObjData>();
        gameData.camPos = playerObjPos;

        MapData mapData = new MapData()
        {
            size = new Vector2Int(1024, 1024),
            terrainLayer = new int[1024][]
                .Select(e => new int[1024]
                    .Select(e => 1)
                    .ToArray())
                .ToArray(),
            objLayer = new List<Guid>[1024][]
                .Select(e => new List<Guid>[1024]
                    .Select(e => null as List<Guid>)
                    .ToArray())
                .ToArray(),
            colliderLayer = new bool[1024][]
                .Select(e => new bool[1024]
                    .Select(e => false)
                    .ToArray())
                .ToArray()
        };
        gameData.mapDic.Add(mapId, mapData);

        ObjData ptObjData = new ObjData();
        Guid ptObjId = Guid.NewGuid();
        ptObjData.common = new CommonData() { type = "CommonData" };
        ptObjData.common.id = ptObjId;
        ptObjData.common.position = default(Position);
        ptObjData.common.pointArr = new Vector2Int[]{
            new Vector2Int(0,0),
            new Vector2Int(1,0),
            new Vector2Int(0,1),
            new Vector2Int(1,1)
        };
        ptObjData.propertyDic = new Dictionary<string, PropertyData>(){
            {"Mover",new MoverData(){type="MoverData"}},
            {"Graphic",new GraphicData(){type="GraphicData"}}
        };
        ((MoverData)ptObjData.propertyDic["Mover"]).speed = 4f;
        ((GraphicData)ptObjData.propertyDic["Graphic"]).spriteId = "flower_factory_3";
        gameData.objDic.Add(ptObjId, ptObjData);
        StartGame();
        gameData.playerObjId = CreateObj(ptObjId, playerObjPos);
        playerObj = objDic[gameData.playerObjId];
    }
    public bool SaveGame(string gameName)
    {
        if (gameData == null)
        {
            Debug.LogError("There is no game data");
            return false;
        }
        string filePath = $"{Application.persistentDataPath}/{gameName}.game_data";
        string json = JsonConvert.SerializeObject(gameData);
        System.IO.File.WriteAllText(filePath, json);
        Debug.LogWarning("Game saved as" + filePath);
        return true;
    }
    public bool LoadGame(string gameName)
    {
        //세이브 데이터에 플레이어 오브젝트의 아이디값이 있어야함 -> 맵 아이디랑 포지션(좌표값)이 있어야함
        string filePath = $"{Application.persistentDataPath}/{gameName}.game_data";
        if (!File.Exists(filePath))
        {
            Debug.LogError("Game file not found in " + filePath);
            return false;
        }
        string json = System.IO.File.ReadAllText(filePath);
        gameData = JsonConvert.DeserializeObject<GameData>(json);
        if (gameData == null)
        {
            Debug.LogError("Game file is invalid");
            return false;
        }
        return true;
    }
    public bool StartGame()
    {
        //카메라
        Camera.main.GetComponent<CameraController>().StartTraceTarget();
        //맵
        Guid mapId = gameData.camPos.mapId;
        MapData mapData = gameData.mapDic[mapId];
        LoadMap(mapData);

        return true;
    }
    string[] GetGameNameArr() //이름 목록만 가져옴
    {
        string directoryPath = Application.persistentDataPath;
        string[] gameNameList =
        Directory.GetFiles(directoryPath, "*.game_data")
        .Select(e => Path.GetFileName(e))
        .ToArray();
        return gameNameList;
    }

    //map
    MapData mapData;
    Transform mapTf;
    public void LoadMap(MapData mapData)
    {
        if (mapData != null) StopUpdateSeamless();
        this.mapData = mapData;
        var mapObj = new GameObject();
        mapObj.name = "map";
        mapObj.AddComponent<Grid>();
        this.mapTf = mapObj.transform;

        StartUpdateSeamless();
    }
    Vector2Int chunkSize;
    int seamlessRangeIn, seamlessRangeOut;
    Dictionary<Vector2Int, GameObject> chunkDic;
    Vector2Int centerIdx;
    Transform cameraTf;
    Coroutine coUpdateSeamless;
    void StartUpdateSeamless()
    {
        if (coUpdateSeamless != null) return;
        //setting
        chunkSize = settingData.chunkSize;
        seamlessRangeIn = settingData.seamlessRange - 1;
        seamlessRangeOut = settingData.seamlessRange + 1;

        chunkDic = new Dictionary<Vector2Int, GameObject>();
        centerIdx = new Vector2Int(int.MaxValue, int.MaxValue);
        cameraTf = Camera.main.transform;
        coUpdateSeamless = StartCoroutine(UpdateSeamless());
    }
    void StopUpdateSeamless()
    {
        if (coUpdateSeamless == null) return;
        StopCoroutine(coUpdateSeamless);
        coUpdateSeamless = null;
    }
    IEnumerator UpdateSeamless()
    {
        while (true)
        {
            Vector2Int newCenterIdx = ConvertWorldPosToChunkIdx(cameraTf.position);
            if (newCenterIdx == centerIdx)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }
            //범위를 벗어난 청크 언로드
            List<Vector2Int> unloadIdxList = new List<Vector2Int>();
            foreach (var kvp in chunkDic)
            {
                Vector2Int chunkIdx = kvp.Key;
                if (chunkIdx.x < newCenterIdx.x - seamlessRangeOut
                | chunkIdx.x > newCenterIdx.x + seamlessRangeOut
                | chunkIdx.y < newCenterIdx.y - seamlessRangeOut
                | chunkIdx.y > newCenterIdx.y + seamlessRangeOut)
                    unloadIdxList.Add(chunkIdx);
            }
            foreach (Vector2Int unloadIdx in unloadIdxList)
            {
                UnloadChunk(unloadIdx);
            }

            //범위에 들어온 새로운 청크 로드
            for (int x = newCenterIdx.x - seamlessRangeIn; x <= newCenterIdx.x + seamlessRangeIn; x++)
            {
                for (int y = newCenterIdx.y - seamlessRangeIn; y <= newCenterIdx.y + seamlessRangeIn; y++)
                {
                    Debug.Log(new Vector2Int(x, y));
                    LoadChunk(new Vector2Int(x, y));
                }
            }
            centerIdx = newCenterIdx;
            yield return new WaitForSeconds(1f);
        }
    }
    void LoadChunk(Vector2Int chunkIdx)
    {
        if (chunkDic.ContainsKey(chunkIdx)) return;
        Vector2Int chunkPos = chunkSize * chunkIdx;
        GameObject chunkObj = new GameObject();
        chunkObj.name = chunkIdx.ToString();
        chunkObj.AddComponent<Tilemap>();
        chunkObj.AddComponent<TilemapRenderer>();
        chunkObj.transform.position = (Vector2)chunkPos;
        Tilemap tilemap = chunkObj.GetComponent<Tilemap>();
        chunkObj.transform.parent = mapTf;
        chunkDic.Add(chunkIdx, chunkObj);

        for (int x = 0; x < chunkSize.x; x++)
        {
            for (int y = 0; y < chunkSize.y; y++)
            {
                int terrainId = mapData.terrainLayer[chunkPos.x + x][chunkPos.y + y];
                TileBase tile = ResourceManager.instance.terrainDic[terrainId];
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                List<Guid> objIdList = mapData.objLayer[chunkPos.x + x][chunkPos.y + y];
                if (objIdList == null) continue;
                foreach (Guid objId in objIdList)
                {
                    LoadObj(objId);
                }
            }
        }
    }
    void UnloadChunk(Vector2Int chunkIdx)
    {
        if (!chunkDic.ContainsKey(chunkIdx)) return;
        Destroy(chunkDic[chunkIdx]);
        chunkDic.Remove(chunkIdx);

        Vector2Int chunkPos = chunkSize * chunkIdx;
        for (int x = 0; x < chunkSize.x; x++)
        {
            for (int y = 0; y < chunkSize.y; y++)
            {
                List<Guid> objIdList = mapData.objLayer[chunkPos.x + x][chunkPos.y + y];
                if (objIdList == null) continue;
                foreach (Guid objId in objIdList)
                {
                    UnloadObj(objId);
                }
            }
        }
    }

    public void EditTerrainLayer(Position position, int terrain)
    {

    }
    public void EditColliderLayer(Position position, bool collider)
    {

    }
    //세개 합쳐서 EditObjLayer
    public void AttachObj(Guid objId, IEnumerable<Vector2Int> coordArr)
    {
        List<Guid> overlappedIdList = new List<Guid>();
        overlappedIdList.Add(objId);
        foreach (Vector2Int coord in coordArr)
        {
            List<Guid> idList = mapData.objLayer[coord.x][coord.y];
            if (idList == null)
            {
                mapData.objLayer[coord.x][coord.y] = new List<Guid>();
                idList = mapData.objLayer[coord.x][coord.y];
            }
            overlappedIdList.AddRange(idList);
            idList.Add(objId);
        }
        overlappedIdList = overlappedIdList.Distinct().ToList();
        foreach (Guid id in overlappedIdList)
        {
            Common common = objDic[id].GetComponent<Common>();
            common.onOverlappingEnter?.Invoke();
        }
        UpdateObj(objId);
    }
    public void DetachObj(Guid objId, IEnumerable<Vector2Int> coordArr)
    {
        List<Guid> overlappedIdList = new List<Guid>();
        overlappedIdList.Add(objId);
        foreach (Vector2Int coord in coordArr)
        {
            List<Guid> idList = mapData.objLayer[coord.x][coord.y];
            if (idList == null) continue;
            overlappedIdList.AddRange(idList);
            idList.Remove(objId);
        }
        overlappedIdList = overlappedIdList.Distinct().ToList();
        foreach (Guid id in overlappedIdList)
        {
            Common common = objDic[id].GetComponent<Common>();
            common.onOverlappingExit?.Invoke();
        }
        UpdateObj(objId);
    }
    void UpdateObj(Guid objId)
    {
        //만약 로드된맵으로 들어온거면 loadObj(), 로드된맵밖으로 나간거면 unloadObj()
        Position objPos = gameData.objDic[objId].common.position;
        bool isOnLoadedMap = objPos.mapId == gameData.camPos.mapId;
        Vector2Int chunkIdx = ConvertWorldPosToChunkIdx((Vector2Int)objPos);
        bool isOnLoadedChunk = chunkDic.ContainsKey(chunkIdx);
        Debug.Log(isOnLoadedChunk);
        if (isOnLoadedMap & isOnLoadedChunk) LoadObj(objId);
        else UnloadObj(objId);
    }

    //obj
    Dictionary<Guid, GameObject> objDic = new Dictionary<Guid, GameObject>();
    public Guid CreateObj(Guid objId, Position position)
    {
        ObjData objData = gameData.objDic[objId];
        string objJson = JsonConvert.SerializeObject(objData);
        ObjData newObjData = JsonConvert.DeserializeObject<ObjData>(objJson);
        Guid newObjId = Guid.NewGuid();
        //DeserializePropertyData(newObjData);
        newObjData.common.id = newObjId;
        gameData.objDic.Add(newObjId, newObjData);
        LoadObj(newObjId);
        objDic[newObjId].GetComponent<Common>().position = position;
        return newObjId;
    }
    public void DestroyObj()
    {
    }
    void LoadObj(Guid objId)
    {
        Debug.Log($"Load: {objId}");
        if (objDic.ContainsKey(objId)) return;
        ObjData objData = gameData.objDic[objId];
        //DeserializePropertyData(objData);
        GameObject obj = new GameObject();
        obj.name = objId.ToString();
        Common common = obj.AddComponent<Common>();
        common.SetData(objData.common);
        foreach (var kvp in objData.propertyDic)
        {
            string propertyName = kvp.Key;
            dynamic propertyData = kvp.Value;
            Property property = obj.AddComponent(Type.GetType(propertyName)) as Property;
            property.SetData(propertyData);
        }
        objDic.Add(objId, obj);
        Debug.Log("A");
        if (gameData.playerObjId == objId)
        {
            Debug.Log("B");
            playerObj = obj;
        }
        Debug.Log("C");
    }
    void UnloadObj(Guid objId)
    {
        Debug.Log($"Unload: {objId}");
        if (!objDic.ContainsKey(objId)) return;
        Destroy(objDic[objId]);
        objDic.Remove(objId);
        if (gameData.playerObjId == objId) playerObj = null;
    }

    //util
    Vector2Int ConvertWorldPosToChunkIdx(Vector2 worldPos)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPos.x / chunkSize.x), Mathf.FloorToInt(worldPos.y / chunkSize.y));
    }
}

public class SettingData
{
    public Vector2Int chunkSize { get; set; }
    public int seamlessRange { get; set; }//min:1
    public int audioVolume { get; set; }
}
public class GameData
{
    public Dictionary<Guid, MapData> mapDic { get; set; }
    public Dictionary<Guid, ObjData> objDic { get; set; }
    public Guid playerObjId { get; set; }
    public Position camPos { get; set; }
}
public class MapData
{
    public Vector2Int size { get; set; }
    public int[][] terrainLayer { get; set; }
    public List<Guid>[][] objLayer { get; set; }
    public bool[][] colliderLayer { get; set; }
}
public class ObjData
{
    public CommonData common { get; set; }
    public Dictionary<string, PropertyData> propertyDic { get; set; }
}
public struct Position
{
    public Guid mapId { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public Position(Guid mapId, int x, int y)
    {
        this.mapId = mapId;
        this.x = x;
        this.y = y;
    }
    public Position(Guid mapId, Vector2Int coord)
    {
        this.mapId = mapId;
        this.x = coord.x;
        this.y = coord.y;
    }
    public static explicit operator Vector2Int(Position position)
    {
        return new Vector2Int(position.x, position.y);
    }
    public static Position operator +(Position a, Vector2Int b) => new Position(a.mapId, a.x + b.x, a.y + b.y);
    public override bool Equals(object obj)
    {
        if (!(obj is Position)) return false;
        Position position = (Position)obj;
        if (this.mapId == position.mapId
        & this.x == position.x
        & this.y == position.y) return true;
        return false;
    }
    public override int GetHashCode() => (mapId, x, y).GetHashCode();
    public static bool operator ==(Position a, Position b) => a.Equals(b);
    public static bool operator !=(Position a, Position b) => !(a == b);
}
//캐릭터이동->맵로딩->카메라이동
//캐릭터가 먼저 다음맵에 가있고, 맵이 로딩되고나서, 카메라가 뒤이어 도착
//맵의 로딩은 mapId또는 mapData를 인수로 전달
//맵의 로딩이 끝나면 gameData.objDic[playerObjId].common.position으로 카메라이동

//맵도 프로토타입있어야함
//맵과 오브젝트의 관계는 독립적? obj id는 월드전체에서 유니크해야함
//맵A 맵B가 있는데, 맵B가 사라지면 맵B에 있던 obj들은 어떻게되나? 맵안에 오브젝트가 종속되어있는 느낌->같이 삭제됨
//프로토타입 맵에는 오브젝트가 배치되어있는데, 이 옵젝들은 프로토타입옵젝인가? 하나의 세트라면 프로토타입?
//근데 맵에 똑같은종류의 옵젝이 두개 배치돼있으면 프로토타입옵젝이 두개가 있다는말인데 뭔가 안맞음
//그럼결국 프로토타입맵에 배치된 옵젝은?
//"프로토타입" 이라는개념을 그냥 일반의 그것과 합쳐버리는게 맞을까?
//ㄴ>이랬을때 문제점: 프로토타입의 역할을 해야하는 옵젝은 비활성화된채 어느곳에도 존재하지 않아야함

//그 개발자가 미리만들어두는 "프로토타입맵"에는 개발자가 미리 옵젝도 배치해둘수있잖아 ㅇㅇ
//그 옵젝들도 수치가 약간씩 다를수있고 그렇다면?
//그냥 싹다 일반옵젝 일반맵으로 만들어봄