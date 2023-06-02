using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Common : Property
{
    static string _type = typeof(Common).ToString();
    [JsonProperty] public override string type { get => _type; set { } }
    [JsonProperty] Position _position { get; set; }
    [JsonProperty] Vector2Int[] _pointArr { get; set; }
    public static Common Create(Vector2Int[] pointArr)
    {
        Common common = new Common();
        common._pointArr = pointArr;
        return common;
    }
    public Position position
    {
        get => _position;
        set//TODO: mapId의 이동 추가
        {
            if (value == position) return;
            if (value.mapId == position.mapId)//맵내이동
            {
                changingPosition?.Invoke();
                IEnumerable<Vector2Int> prevCoords;
                if (position == default(Position)) prevCoords = new Vector2Int[0];
                else prevCoords = pointArr.Select((point) => point * (Vector2Int)position);
                var nextCoords = pointArr.Select((point) => point * (Vector2Int)value);

                var stayingCoords = prevCoords.Intersect(nextCoords);
                var exitingCoords = prevCoords.Except(stayingCoords);
                var enteringCoords = nextCoords.Except(stayingCoords);

                Map map = GameManager.instance.game.mapDic[position.mapId];
                IEnumerable<Guid> stayingIdList;
                IEnumerable<Guid> exitingIdList;
                IEnumerable<Guid> enteringIdList;

                List<Guid> overlapExistedIdList;
                overlapExistedIdList = new List<Guid>();
                foreach (var coord in exitingCoords)
                {
                    List<Guid> idList = map.objLayer.Get(coord);
                    if (idList == null) continue;
                    overlapExistedIdList.AddRange(idList);
                }
                exitingIdList = overlapExistedIdList.Distinct();
                overlapExistedIdList = new List<Guid>();
                foreach (var coord in enteringCoords)
                {
                    List<Guid> idList = map.objLayer.Get(coord);
                    if (idList == null) continue;
                    overlapExistedIdList.AddRange(idList);
                }
                enteringIdList = overlapExistedIdList.Distinct();

                stayingIdList = exitingIdList.Intersect(enteringIdList);
                exitingIdList = exitingIdList.Except(stayingIdList);
                enteringIdList = enteringIdList.Except(stayingIdList);

                //bool isCanceled = false;//Todo: 취소가능한 이벤트로 만들기
                foreach (var id in exitingIdList)
                {
                    GameManager.instance.GetObj(id).GetProperty<Common>().exitingOverlap?.Invoke();
                }
                // foreach (var id in intersectingIdList)//이동할때마다 발동해줄거면 사용
                // {
                //     GameManager.instance.GetObj(id).GetProperty<Common>().stayingOverlap.Invoke();
                // }
                foreach (var id in enteringIdList)
                {
                    GameManager.instance.GetObj(id).GetProperty<Common>().enteringOverlap?.Invoke();
                }
                //if (isCanceled) { return;}

                foreach (var coord in exitingCoords)
                {
                    map.objLayer.Get(coord).Remove(obj.id);
                    if (map.objLayer.Get(coord) == null) map.objLayer.Set(coord, null);
                }
                foreach (var coord in enteringCoords)
                {
                    var coordIdList = map.objLayer.Get(coord);
                    if (coordIdList == null)
                    {
                        coordIdList = new List<Guid>();
                        map.objLayer.Set(coord, coordIdList);
                    }
                    map.objLayer.Get(coord).Add(obj.id);
                }
            }
            else//맵간이동
            {
                changingPosition?.Invoke();
                IEnumerable<Vector2Int> exitingCoords = null;
                IEnumerable<Vector2Int> enteringCoords = null;
                Map exitingMap = null;
                Map enteringMap = null;
                IEnumerable<Guid> exitingIdList = null;
                IEnumerable<Guid> enteringIdList = null;
                List<Guid> overlapExistedIdList;
                bool isExitingRequired = position.mapId != default(Guid);
                bool isEnteringRequired = value.mapId != default(Guid);
                if (isExitingRequired)
                {
                    exitingCoords = pointArr.Select((point) => point * (Vector2Int)position);
                    exitingMap = GameManager.instance.game.mapDic[position.mapId];
                    overlapExistedIdList = new List<Guid>();
                    foreach (var coord in exitingCoords)
                    {
                        List<Guid> idList = exitingMap.objLayer.Get(coord);
                        if (idList == null) continue;
                        overlapExistedIdList.AddRange(idList);
                    }
                    exitingIdList = overlapExistedIdList.Distinct();
                }
                if (isEnteringRequired)
                {
                    enteringCoords = pointArr.Select((point) => point * (Vector2Int)value);
                    enteringMap = GameManager.instance.game.mapDic[value.mapId];
                    overlapExistedIdList = new List<Guid>();
                    foreach (var coord in enteringCoords)
                    {
                        List<Guid> idList = enteringMap.objLayer.Get(coord);
                        if (idList == null) continue;
                        overlapExistedIdList.AddRange(idList);
                    }
                    enteringIdList = overlapExistedIdList.Distinct();
                }

                //bool isCanceled = false;//Todo: 취소가능한 이벤트로 만들기
                if (isExitingRequired)
                {
                    foreach (var id in exitingIdList)
                    {
                        GameManager.instance.GetObj(id).GetProperty<Common>().exitingOverlap?.Invoke();
                    }
                }
                if (isEnteringRequired)
                {
                    foreach (var id in enteringIdList)
                    {
                        GameManager.instance.GetObj(id).GetProperty<Common>().enteringOverlap?.Invoke();
                    }
                }
                //if (isCanceled) { return;}

                if (isExitingRequired)
                {
                    foreach (var coord in exitingCoords)
                    {
                        exitingMap.objLayer.Get(coord).Remove(obj.id);
                        if (exitingMap.objLayer.Get(coord) == null) exitingMap.objLayer.Set(coord, null);
                    }
                }
                if (isEnteringRequired)
                {
                    foreach (var coord in enteringCoords)
                    {
                        var coordIdList = enteringMap.objLayer.Get(coord);
                        if (coordIdList == null)
                        {
                            coordIdList = new List<Guid>();
                            enteringMap.objLayer.Set(coord, coordIdList);
                        }
                        coordIdList.Add(obj.id);
                    }
                }
            }
            _position = value;

            if (GameManager.instance.CheckIsCoordLoaded(position)) obj.LoadGo();
            else obj.UnloadGo();

            if (go != null)
            {
                go.transform.position = new Vector3(value.x, value.y, 0);
            }
        }
    }
    public Vector2Int[] pointArr
    {
        get => _pointArr;
        set
        {
            if (value == pointArr) return;
            changingPointArr?.Invoke();
            _pointArr = value;
        }
    }

    public override void OnLoadGo()
    {
        go.transform.position = new Vector3(position.x, position.y, 0);
    }

    public event Action created;
    public event Action destroying;
    public event Action changingPosition;
    public event Action changingPointArr;
    public Action enteringOverlap;
    public Action exitingOverlap;
    // public EventHandler stayingOverlap;//틱시스템이 있거나, 이동할때마다 발동하는거면 추가가능
}