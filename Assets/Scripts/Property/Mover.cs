using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Mover : Property
{
    static readonly float sqrt2 = Mathf.Sqrt(2);

    static string _type = typeof(Mover).ToString();
    [JsonProperty] public override string type { get => _type; set { } }
    [JsonProperty] float _speed { get; set; }
    public static Property Create(float speed)
    {
        Mover mover = new Mover();
        mover._speed = speed;
        return mover;
    }
    public float speed//meter per sec
    {
        get => _speed;
    }

    Vector2Int direction;
    bool isMoving;
    Coroutine coMove;
    IEnumerator Move()
    {
        while (isMoving)
        {
            onMoved?.Invoke();
            obj.GetProperty<Common>().position += direction;
            yield return new WaitForSeconds(1 / (2 * speed));
        }
        coMove = null;

    }
    public void StartMove(Vector2Int direction)
    {
        Debug.Log("움직입니다");
        obj.GetProperty<Common>().position += direction;
        // Debug.Log("움직입니다");
        // this.direction = direction;
        // isMoving = true;
        // if (coMove != null) return;
        // coMove = StartCoroutine(Move()); // Todo: unity life cycle이 아닌, 커스텀 틱시스템에서 작동하는 coroutine 작성
    }
    public void StopMove()
    {
        isMoving = false;
    }

    public override void OnLoadGo() { }

    public Action onMoved;
}