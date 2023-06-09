using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Text.Json;

public class MapEditor : MonoBehaviour
{
    public static MapEditor instance = null;
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;//이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
        DontDestroyOnLoad(gameObject); //씬 전환이 되더라도 파괴되지 않게 한다.
    }
    Dictionary<Guid, Map> mapDic;
    public Transform terrainsPnlTf;
    public GameObject terrainPf;

    public void Init(Dictionary<Guid, Map> mapDic)
    {
        this.mapDic = mapDic;
        LoadMapNameDic();
        foreach (var kvp in ResourceManager.instance.terrainDic)
        {
            int terrain = kvp.Key;
            RuleTile terrainTile = kvp.Value;
            Transform terrainTf = Instantiate(terrainPf).transform;
            terrainTf.SetParent(terrainsPnlTf);
            terrainTf.GetComponent<Image>().sprite = terrainTile.m_DefaultSprite;
            terrainTf.GetComponent<Button>().onClick.AddListener(() => SelectTerrain(terrain));
        }
        btnImgDic = new Dictionary<string, Image>();
        foreach (Button button in editModesTf.GetComponentsInChildren<Button>())
        {
            btnImgDic.Add(button.name, button.GetComponent<Image>());
        }
    }
    Guid mapId;
    Dictionary<Guid, string> mapNameDic;
    void LoadMapNameDic()
    {
        string filePath = $"{Application.persistentDataPath}/map_editor/map_name_dic";
        if (!File.Exists(filePath)) return;
        string json = System.IO.File.ReadAllText(filePath);
        mapNameDic = JsonSerializer.Deserialize<Dictionary<Guid, string>>(json);
    }
    void SaveMapNameDic()
    {
        if (mapNameDic == null) return;
        string filePath = $"{Application.persistentDataPath}/map_editor/map_name_dic";
        string json = JsonSerializer.Serialize(mapNameDic);
        System.IO.File.WriteAllText(filePath, json);
    }
    void EditMapName(Guid mapId, string mapName)
    {
        if (mapNameDic.ContainsKey(mapId))
        {
            if (mapDic.ContainsKey(mapId)) mapNameDic.Add(mapId, mapName);
        }
        else
        {
            if (mapName == "") mapNameDic.Remove(mapId);
            else mapNameDic[mapId] = mapName;
        }
    }
    void SelectMap(Guid mapId)
    {

    }


    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) phase = 0;
            return;
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int mouseCoord = new Vector2Int(Mathf.FloorToInt(mousePosition.x), Mathf.FloorToInt(mousePosition.y));

        if (editMode == "point")
        {
            if (Input.GetMouseButton(0))
            {
                DrawPoint(mouseCoord);
            }
        }
        else if (editMode == "line")
        {
            if (phase == 0 && Input.GetMouseButtonDown(0))
            {
                latestCoord = mouseCoord;
                phase = 1;
            }
            if (phase == 1 && Input.GetMouseButtonUp(0))
            {
                DrawLine(latestCoord, mouseCoord);
                phase = 0;
            }
        }
        else if (editMode == "rectangle")
        {
            if (phase == 0 && Input.GetMouseButtonDown(0))
            {
                latestCoord = mouseCoord;
                phase = 1;
            }
            if (phase == 1 && Input.GetMouseButtonUp(0))
            {
                DrawRectangle(latestCoord, mouseCoord);
                phase = 0;
            }
        }
    }

    private int selectedTerrain = 0;
    string editMode = "point";
    Vector2Int latestCoord;
    int phase = 0;
    Dictionary<string, Image> btnImgDic;
    public Transform editModesTf;
    public void SelectTerrain(int terrain)
    {
        selectedTerrain = terrain;
    }
    public void SetEditMode(string editMode)
    {
        btnImgDic[this.editMode].color = Color.white;
        btnImgDic[editMode].color = Color.grey;
        this.editMode = editMode;
    }

    private void DrawPoint(Vector2Int point)
    {
        GameManager.instance.game.mapDic[mapId].terrainLayer.Set(point, selectedTerrain);
        Vector2Int chunkIdx = GameManager.instance.ConvertWorldPosToChunkIdx(point);
        GameManager.instance.UpdateChunk(chunkIdx);
    }
    private void DrawLine(Vector2Int startPoint, Vector2Int endPoint)
    {
        HashSet<Vector2Int> chunkIdxSet = new HashSet<Vector2Int>();

        var line = new Bresenham(startPoint, endPoint);
        foreach (Vector2Int point in line)
        {
            GameManager.instance.game.mapDic[mapId].terrainLayer.Set(point, selectedTerrain);
            chunkIdxSet.Add(GameManager.instance.ConvertWorldPosToChunkIdx(point));
        }

        foreach (var chunkIdx in chunkIdxSet)
        {
            GameManager.instance.UpdateChunk(chunkIdx);
        }
    }
    private void DrawRectangle(Vector2Int startPoint, Vector2Int endPoint)
    {
        HashSet<Vector2Int> chunkIdxSet = new HashSet<Vector2Int>();

        Vector2Int pointA = new Vector2Int(Mathf.Min(startPoint.x, endPoint.x), Mathf.Min(startPoint.y, endPoint.y));
        Vector2Int pointB = new Vector2Int(Mathf.Max(startPoint.x, endPoint.x), Mathf.Max(startPoint.y, endPoint.y));
        for (int x = pointA.x; x <= pointB.x; x++)
        {
            for (int y = pointA.y; y <= pointB.y; y++)
            {
                GameManager.instance.game.mapDic[mapId].terrainLayer.Set(new Vector2Int(x, y), selectedTerrain);
                chunkIdxSet.Add(GameManager.instance.ConvertWorldPosToChunkIdx(new Vector2Int(x, y)));
            }
        }

        foreach (var chunkIdx in chunkIdxSet)
        {
            GameManager.instance.UpdateChunk(chunkIdx);
        }
    }

}

class Bresenham : IEnumerable
{
    private readonly List<Vector2Int> points;

    public int Count { get; private set; }

    public Vector2Int this[int index]
    {
        get => points[index];
    }

    public Bresenham(Vector2Int p1, Vector2Int p2)
    {
        int w = Math.Abs(p2.x - p1.x);
        int h = Math.Abs(p2.y - p1.y);
        points = new List<Vector2Int>(w + h);

        SetPoints(p1, p2);
        Count = points.Count;
    }

    private void SetPoints(in Vector2Int p1, in Vector2Int p2)
    {
        int W = p2.x - p1.x; // width
        int H = p2.y - p1.y; // height;
        int absW = Math.Abs(W);
        int absH = Math.Abs(H);

        int xSign = Math.Sign(W);
        int ySign = Math.Sign(H);

        // 기울기 절댓값
        float absM = (W == 0) ? float.MaxValue : (float)absH / absW;

        int k;  // 판별값
        int kA; // p가 0 이상일 때 p에 더할 값
        int kB; // p가 0 미만일 때 p에 더할 값

        int x = p1.x;
        int y = p1.y;

        // 1. 기울기 절댓값이 1 미만인 경우 => x 기준
        if (absM < 1f)
        {
            k = 2 * absH - absW; // p의 초깃값
            kA = 2 * absH;
            kB = 2 * (absH - absW);

            for (; W >= 0 ? x <= p2.x : x >= p2.x; x += xSign)
            {
                points.Add(new Vector2Int(x, y));

                if (k < 0)
                {
                    k += kA;
                }
                else
                {
                    k += kB;
                    y += ySign;
                }
            }
        }
        // 기울기 절댓값이 1 이상인 경우 => y 기준
        else
        {
            k = 2 * absW - absH; // p의 초깃값
            kA = 2 * absW;
            kB = 2 * (absW - absH);

            for (; H >= 0 ? y <= p2.y : y >= p2.y; y += ySign)
            {
                points.Add(new Vector2Int(x, y));

                if (k < 0)
                {
                    k += kA;
                }
                else
                {
                    k += kB;
                    x += xSign;
                }
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return points.GetEnumerator();
    }
}