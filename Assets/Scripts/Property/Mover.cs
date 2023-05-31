using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Dynamic;
using Newtonsoft.Json;

public class Mover : Property
{
    static readonly float sqrt2 = Mathf.Sqrt(2);

    MoverData data;
    public override void SetData(PropertyData propertyData) { data = (MoverData)propertyData; }

    public float speed//meter per sec
    {
        get => data.speed;
    }

    Vector2Int direction;
    bool isMoving;
    Coroutine coMove;
    IEnumerator Move()
    {
        while (isMoving)
        {
            onMoved?.Invoke();
            common.position += direction;
            yield return new WaitForSeconds(1 / (2 * speed));
        }
        coMove = null;

    }
    public void StartMove(Vector2Int direction)
    {
        Debug.Log("움직입니다");
        this.direction = direction;
        isMoving = true;
        if (coMove != null) return;
        coMove = StartCoroutine(Move());
    }
    public void StopMove()
    {
        isMoving = false;
    }

    public Action onMoved;
}

public class MoverData : PropertyData
{
    public float speed { get; set; }
}