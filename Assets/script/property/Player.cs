using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Property
{
    // Vector2Int coord = Vector2Int.zero;
    // public Vector2Int Coord
    // {
    //     get
    //     {
    //         return coord;
    //     }
    // }
    // float walkSpeed = 2f;
    // float runRate = 1.5f;
    // void Update()
    // {
    //     Vector2Int newCoord = Vector2Int.FloorToInt(transform.position);
    //     if (newCoord != coord) UpdateCoord(newCoord);
    // }
    // void UpdateCoord(Vector2Int newCoord)
    // {
    //     coord = newCoord;
    // }
    // public void Move(Vector2 delta, bool isRunning)
    // {

    //     float moveSpeed = walkSpeed;
    //     if (isRunning) moveSpeed *= runRate;
    //     transform.position += (Vector3)(delta * moveSpeed * Time.deltaTime);
    // }
    struct Data
    {

    }
    Data data;

    public void Move(Vector2Int direction)
    {
        common.center += direction;
    }

    public override string GetData() { return JsonUtility.ToJson(data); }
    public override void SetData(string json) { data = JsonUtility.FromJson<Data>(json); }
}