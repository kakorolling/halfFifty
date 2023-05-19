using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeamlessManager : MonoBehaviour
{
    private Transform focus;
    private Vector2Int latestChunk;
    private List<Vector2Int> latestChunkList;
    void Start()
    {
        focus = Camera.main.transform;
        latestChunk = new Vector2Int(99999, 99999);
        latestChunkList = new List<Vector2Int>();
    }
    void Update()
    {
        Vector2Int currentChunk = new Vector2Int(
            Mathf.FloorToInt(focus.position.x / GameManager.chunkSize.x),
            Mathf.FloorToInt(focus.position.y / GameManager.chunkSize.y)
        );
        if (currentChunk == latestChunk) return;

        //범위를 벗어난 청크 언로드
        List<Vector2Int> boundaryUnload = new List<Vector2Int>();
        for (int x = currentChunk.x - 3; x <= currentChunk.x + 3; x++)
        {
            for (int y = currentChunk.y - 3; y <= currentChunk.y + 3; y++)
            {
                Vector2Int idx = new Vector2Int(x, y);
                boundaryUnload.Add(idx);
            }
        }
        List<Vector2Int> chunkUnload = latestChunkList.Except(boundaryUnload).ToList();
        latestChunkList = latestChunkList.Except(chunkUnload).ToList();
        foreach (Vector2Int idx in chunkUnload)
        {
            GameManager.instance.UnloadChunk(idx);
        }

        //범위에 들어온 새로운 청크 로드
        List<Vector2Int> boundaryLoad = new List<Vector2Int>();
        for (int x = currentChunk.x - 1; x <= currentChunk.x + 1; x++)
        {
            for (int y = currentChunk.y - 1; y <= currentChunk.y + 1; y++)
            {
                Vector2Int idx = new Vector2Int(x, y);
                boundaryLoad.Add(idx);
            }
        }
        List<Vector2Int> chunkLoad = boundaryLoad.Except(latestChunkList).ToList();
        latestChunkList = latestChunkList.Union(chunkLoad).ToList();
        foreach (Vector2Int idx in chunkLoad)
        {
            GameManager.instance.LoadChunk(idx);
        }

        latestChunk = currentChunk;
    }

}