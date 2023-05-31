using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Dynamic;
using Newtonsoft.Json;

public class Common : Property
{
    CommonData data;
    public override void SetData(PropertyData propertyData) { data = (CommonData)propertyData; }

    public Guid id
    {
        get => data.id;
    }
    public Position position
    {
        get => data.position;
        set//TODO: mapId의 이동 추가
        {
            if (value == position) return;
            onPositionChanged?.Invoke();
            IEnumerable<Vector2Int> prevCoords;
            if (position == default(Position)) prevCoords = new Vector2Int[0];
            else prevCoords = pointArr.Select((point) => point * (Vector2Int)position);
            var nextCoords = pointArr.Select((point) => point * (Vector2Int)value);
            var overlappingCoords = prevCoords.Intersect(nextCoords);
            var removingCoords = prevCoords.Except(overlappingCoords);
            var addingCoords = nextCoords.Except(overlappingCoords);
            //if (!GameManager.instance.CheckObjPlacable(id, addingCoords)) return;
            data.position = value;
            GameManager.instance.DetachObj(id, removingCoords);
            GameManager.instance.AttachObj(id, addingCoords);
            transform.position = new Vector3(value.x, value.y, 0);
            Debug.Log($"{position.x}, {position.y}");
        }
    }
    public Vector2Int[] pointArr
    {
        get => data.pointArr;
        set
        {
            if (value == pointArr) return;
            onPointArrChanged?.Invoke();
            data.pointArr = value;
        }
    }

    public Action onCreated;
    public Action onDestroyed;
    public Action onPositionChanged;
    public Action onPointArrChanged;
    public Action onOverlappingEnter;
    public Action onOverlappingExit;
    //public Action onOverlappingStay;//틱시스템이 있을때만 가능
}
public class CommonData : PropertyData
{
    public Guid id { get; set; }
    public Position position { get; set; }
    public Vector2Int[] pointArr { get; set; }
}