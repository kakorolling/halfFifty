using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    //singleton
    public static GameManager instance = null;
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject); //씬이 전환되더라도 파괴되지 않음
    }

    public Setting setting { get; private set; }
    public Game game { get; private set; }
    public GameObject playerGo;//Todo: 다른곳으로 이동해서 더 단순하게 처리
    public bool isPaused;

    void Start()
    {
        if (!LoadSetting("default")) LoadDefaultSetting();
        if (!LoadGame("dummy")) LoadDummyGame();
        //MapEditor.instance.Init(game.mapDic);
    }
    void OnApplicationQuit()
    {
        SaveSetting("default");
        SaveGame("dummy");
    }

    //setting
    bool SaveSetting(string settingName)
    {
        if (setting == null)
        {
            Debug.LogError("There is no setting");
            return false;
        }
        string filePath = $"{Application.persistentDataPath}/{settingName}.setting";
        string json = JsonConvert.SerializeObject(setting);
        System.IO.File.WriteAllText(filePath, json);
        Debug.LogWarning("Setting saved as" + filePath);
        return true;
    }
    bool LoadSetting(string settingName)
    {
        string filePath = $"{Application.persistentDataPath}/{settingName}.setting";
        if (!File.Exists(filePath))
        {
            Debug.LogError("Setting file not found in " + filePath);
            return false;
        }
        string json = System.IO.File.ReadAllText(filePath);

        setting = JsonConvert.DeserializeObject<Setting>(json);
        if (setting == null)
        {
            Debug.LogError("Setting file is invalid");
            return false;
        }
        return true;
    }
    void LoadDefaultSetting()
    {
        setting = new Setting()
        {
            chunkSize = new Vector2Int(32, 32),
            seamlessRange = 2,
            audioVolume = 100
        };
    }

    //game
    public bool SaveGame(string gameName)
    {
        if (game == null)
        {
            Debug.LogError("There is no game");
            return false;
        }
        string filePath = $"{Application.persistentDataPath}/{gameName}.game";
        string json = JsonConvert.SerializeObject(game);
        System.IO.File.WriteAllText(filePath, json);
        Debug.LogWarning("Game saved as" + filePath);
        return true;
    }
    public bool LoadGame(string gameName)
    {
        //세이브 데이터에 플레이어 오브젝트의 아이디값이 있어야함 -> 맵 아이디랑 포지션(좌표값)이 있어야함
        string filePath = $"{Application.persistentDataPath}/{gameName}.game";
        if (!File.Exists(filePath))
        {
            Debug.LogError("Game file not found in " + filePath);
            return false;
        }
        string json = System.IO.File.ReadAllText(filePath);
        game = JsonConvert.DeserializeObject<Game>(json);
        if (game == null)
        {
            Debug.LogError("Game file is invalid");
            return false;
        }

        Camera.main.GetComponent<CameraController>().StartTraceTarget();
        Guid mapId = game.camPos.mapId;
        LoadMap(mapId);
        return true;
    }
    void LoadDummyGame()
    {
        Guid mapId = Guid.NewGuid();
        Position playerObjPos = new Position(mapId, 512, 512);

        game = new Game()
        {
            mapDic = new Dictionary<Guid, Map>(),
            objDic = new Dictionary<Guid, Obj>(),
            camPos = playerObjPos,
        };

        Map map = new Map()
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
            colliderLayer = new int[1024][]
                .Select(e => new int[1024]
                    .Select(e => 0)
                    .ToArray())
                .ToArray()
        };
        game.mapDic.Add(mapId, map);

        Obj originObj = Obj.Create(
            Common.Create(pointArr: new Vector2Int[]{
                    new Vector2Int(0,0),
                    new Vector2Int(1,0),
                    new Vector2Int(0,1),
                    new Vector2Int(1,1)
                }),
            Mover.Create(speed: 4f),
            Graphic.Create(spriteId: "flower_factory_3")
        );
        Guid originObjId = Guid.NewGuid();
        game.objDic.Add(originObjId, originObj);
        game.playerObjId = CreateObj(originObjId, playerObjPos).id;

        Camera.main.GetComponent<CameraController>().StartTraceTarget();
        LoadMap(mapId);
    }
    string[] GetSettingNameArr()
    {
        string[] settingNameArr =
        Directory.GetFiles(Application.persistentDataPath, "*.setting")
        .Select(e => Path.GetFileName(e))
        .ToArray();
        return settingNameArr;
    }
    string[] GetGameNameArr()
    {
        string[] gameNameArr =
        Directory.GetFiles(Application.persistentDataPath, "*.game")
        .Select(e => Path.GetFileName(e))
        .ToArray();
        return gameNameArr;
    }

    //map
    Guid mapId;
    Map map;
    Transform mapTf;
    public void LoadMap(Guid mapId)
    {
        Map map = game.mapDic[mapId];
        if (map != null) StopUpdateSeamless();
        this.map = map;
        var mapObj = new GameObject();
        //mapObj.name = "map";
        mapObj.AddComponent<Grid>();
        this.mapTf = mapObj.transform;

        StartUpdateSeamless();
    }
    int seamlessRangeIn, seamlessRangeOut;
    Dictionary<Vector2Int, GameObject> chunkDic;
    Vector2Int centerIdx;
    Transform cameraTf;
    Coroutine coUpdateSeamless;
    void StartUpdateSeamless()
    {
        if (coUpdateSeamless != null) return;
        seamlessRangeIn = setting.seamlessRange - 1;
        seamlessRangeOut = setting.seamlessRange + 1;

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
                || chunkIdx.x > newCenterIdx.x + seamlessRangeOut
                || chunkIdx.y < newCenterIdx.y - seamlessRangeOut
                || chunkIdx.y > newCenterIdx.y + seamlessRangeOut)
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
                    LoadChunk(new Vector2Int(x, y));
                }
            }
            centerIdx = newCenterIdx;
            yield return new WaitForSeconds(1f);
        }
    }
    void LoadChunk(Vector2Int chunkIdx)
    {
        GameObject chunkObj;
        if (chunkDic.ContainsKey(chunkIdx)) return;
        chunkObj = new GameObject();
        chunkObj.transform.parent = mapTf;
        chunkObj.transform.position = (Vector2)setting.chunkSize * chunkIdx;
        chunkObj.AddComponent<Tilemap>();
        chunkObj.AddComponent<TilemapRenderer>();
        chunkDic.Add(chunkIdx, chunkObj);
        UpdateChunk(chunkIdx);
    }
    void UnloadChunk(Vector2Int chunkIdx)
    {
        if (!chunkDic.ContainsKey(chunkIdx)) return;

        //chunkGo
        Destroy(chunkDic[chunkIdx]);
        //objGo
        Vector2Int chunkPos = setting.chunkSize * chunkIdx;
        for (int x = 0; x < setting.chunkSize.x; x++)
        {
            for (int y = 0; y < setting.chunkSize.y; y++)
            {
                List<Guid> objIdList = map.objLayer[chunkPos.x + x][chunkPos.y + y];
                if (objIdList == null) continue;
                foreach (Guid objId in objIdList)
                {
                    GetObj(objId).UnloadGo();
                }
            }
        }

        chunkDic.Remove(chunkIdx);
    }
    public void UpdateChunk(Vector2Int chunkIdx)
    {
        GameObject chunkObj;
        if (!chunkDic.TryGetValue(chunkIdx, out chunkObj)) return;
        Tilemap tilemap = chunkObj.GetComponent<Tilemap>();
        Vector2Int chunkPos = setting.chunkSize * chunkIdx;
        for (int x = 0; x < setting.chunkSize.x; x++)
        {
            for (int y = 0; y < setting.chunkSize.y; y++)
            {
                //terrain
                int terrainId = map.terrainLayer[chunkPos.x + x][chunkPos.y + y];
                TileBase tile = ResourceManager.instance.terrainDic[terrainId];
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                //obj
                List<Guid> objIdList = map.objLayer[chunkPos.x + x][chunkPos.y + y];
                if (objIdList != null)
                {
                    foreach (Guid objId in objIdList)
                    {
                        GetObj(objId).LoadGo();
                    }
                }
            }
        }
    }
    public bool CheckIsCoordLoaded(Position position)
    {
        Vector2Int chunkIdx = ConvertWorldPosToChunkIdx((Vector2Int)position);
        return (position.mapId == game.camPos.mapId) && (chunkDic != null) && (chunkDic.ContainsKey(chunkIdx));
    }

    //obj
    //Dictionary<Guid, GameObject> objDic = new Dictionary<Guid, GameObject>();
    public Obj CreateObj(Guid originObjId, Position position)
    {
        Obj obj = GetObj(originObjId);
        string objJson = JsonConvert.SerializeObject(obj);
        Debug.Log(objJson);
        Obj newObj = JsonConvert.DeserializeObject<Obj>(objJson);
        newObj.id = Guid.NewGuid();
        newObj.SetPropertyObjId();
        foreach (var property in newObj.GetPropertyArr())
        {
            property.obj = newObj;
        }
        newObj.GetProperty<Common>().position = position;

        game.objDic.Add(newObj.id, newObj);
        return newObj;
    }
    public void DestroyObj() { }

    //util
    public Vector2Int ConvertWorldPosToChunkIdx(Vector2Int worldPos)
    => new Vector2Int(
        Mathf.FloorToInt(worldPos.x / setting.chunkSize.x),
        Mathf.FloorToInt(worldPos.y / setting.chunkSize.y)
    );
    public Vector2Int ConvertWorldPosToChunkIdx(Vector2 worldPos)
    => new Vector2Int(
        Mathf.FloorToInt(worldPos.x / setting.chunkSize.x),
        Mathf.FloorToInt(worldPos.y / setting.chunkSize.y)
    );
    public Obj GetObj(Guid objId) => game.objDic[objId];
}

public class Setting
{
    public Vector2Int chunkSize { get; set; }
    public int seamlessRange { get; set; }//min:1
    public int audioVolume { get; set; }
}
public class Game
{
    public Dictionary<Guid, Map> mapDic { get; set; }
    public Dictionary<Guid, Obj> objDic { get; set; }
    public Guid playerObjId { get; set; }
    public Position camPos { get; set; }
    [JsonIgnore] public Obj playerObj { get => objDic[playerObjId]; }
}
public class Map
{
    public Vector2Int size { get; set; }
    public int[][] terrainLayer { get; set; }
    public List<Guid>[][] objLayer { get; set; }
    public int[][] colliderLayer { get; set; }
}
[JsonObject(MemberSerialization.OptIn)]
public class Obj
{
    [JsonProperty] Guid _id { get; set; }
    [JsonProperty] Dictionary<string, Property> propertyDic { get; set; }
    public static Obj Create(params Property[] propertyArr)
    {
        Obj obj = new Obj();
        obj._id = default(Guid);
        obj.propertyDic = new Dictionary<string, Property>();
        foreach (var property in propertyArr)
        {
            obj.AddProperty(property);
        }
        return obj;
    }
    public Guid id
    {
        get => _id;
        set
        {
            if (_id != default(Guid)) return;
            _id = value;
        }
    }
    GameObject _go;
    public GameObject go { get => _go; }
    public bool AddProperty(Property property)
    {
        //Todo: 프로퍼티들에 프로퍼티가 추가된다는 이벤트를 발생시킴, 그에따라 추가 가능여부를 결정
        property.obj = this;
        if (property.obj != this) return false;
        propertyDic.Add(property.GetType().ToString(), property);
        return true;
    }
    public bool RemoveProperty(Property property)
    {
        //Todo: 프로퍼티들에 프로퍼티가 제거된다는 이벤트를 발생시킴, 그에따라 제거 가능여부를 결정
        if (!propertyDic.Remove(property.GetType().ToString())) return false;
        return true;
    }
    public T GetProperty<T>() where T : Property
    {
        string propertyName = typeof(T).ToString();
        Property property = null;
        propertyDic.TryGetValue(propertyName, out property);
        return (T)property;
    }
    public Property[] GetPropertyArr() => propertyDic.Values.ToArray();
    public void SetPropertyObjId()
    {
        foreach (var property in propertyDic.Values)
        {
            property.obj = this;
        }
    }
    public void LoadGo()
    {
        Debug.Log("Load go: " + id);
        if (_go != null) return;
        _go = new GameObject();
        foreach (var property in propertyDic.Values)
        {
            property.OnLoadGo();
        }
        if (id == GameManager.instance.game.playerObjId) GameManager.instance.playerGo = _go;
    }
    public void UnloadGo()
    {
        Debug.Log("Unload go: " + id);
        if (_go == null) return;
        UnityEngine.Object.Destroy(_go);
        _go = null;
    }
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
        && this.x == position.x
        && this.y == position.y) return true;
        return false;
    }
    public override int GetHashCode() => (mapId, x, y).GetHashCode();
    public static bool operator ==(Position a, Position b) => a.Equals(b);
    public static bool operator !=(Position a, Position b) => !(a == b);
}

public static class MapExtension
{
    public static T Get<T>(this T[][] map, Vector2Int coord) => map[coord.x][coord.y];
    public static void Set<T>(this T[][] map, Vector2Int coord, T value)
    {
        map[coord.x][coord.y] = value;
    }
}