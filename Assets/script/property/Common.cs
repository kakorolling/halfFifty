using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Common : Property
{
    struct Data
    {
        public int id;
        public Vector2Int center;
        public Vector2Int[] vertices;
    }
    Data data;
    public int id
    {
        get { return data.id; }
    }
    public Vector2Int center
    {
        get { return data.center; }
        set
        {
            if (value == data.center) return;
            OnCenterShifted();
            var prevCoords = data.vertices.Select((vertex) => vertex * data.center);
            var nextCoords = data.vertices.Select((vertex) => vertex * value);
            var overlappingCoords = prevCoords.Intersect(nextCoords);
            var removingCoords = prevCoords.Except(overlappingCoords);
            var addingCoords = nextCoords.Except(overlappingCoords);

            //가도되는지 물어보는 이벤트
            //if (!GameManager.instance.IsObjAddable(addingCoords, this))
            {
                //이동불가
                return;
            }
            //GameManager.instance.RemoveObj(removingCoords, this);
            //GameManager.instance.AddObj(addingCoords, this);

            data.center = value;
            transform.position = new Vector3(value.x, value.y, 0);
        }
    }
    public Vector2Int[] vertices
    {
        get { return data.vertices; }
        set
        {
            if (value == data.vertices) return;
            OnVerticesChanged();
            data.vertices = value;
        }
    }

    public override string GetData() { return JsonUtility.ToJson(data); }
    public override void SetData(string json) { data = JsonUtility.FromJson<Data>(json); }

    public Action onDestroyed;
    public Action onCenterShifted;
    public Action onVerticesChanged;
    public Action onOverlappingEnter;
    public Action onOverlappingStay;
    public Action onOverlappingExit;
    void OnDestroyed()
    {
        if (onDestroyed != null) onDestroyed.Invoke();
    }
    void OnCenterShifted()
    {
        if (onCenterShifted != null) onCenterShifted.Invoke();
    }
    void OnVerticesChanged()
    {
        if (onVerticesChanged != null) onVerticesChanged.Invoke();
    }
    void OnOverlappingEnter()
    {
        if (onOverlappingEnter != null) onOverlappingEnter.Invoke();
    }
    void OnOverlappingStay()
    {
        if (onOverlappingStay != null) onOverlappingStay.Invoke();
    }
    void OnOverlappingExit()
    {
        if (onOverlappingExit != null) onOverlappingExit.Invoke();
    }
}



