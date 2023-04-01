using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2Int coord = Vector2Int.zero;
    public Vector2Int Coord
    {
        get
        {
            return coord;
        }
    }
    float walkSpeed = 2f;
    float runRate = 1.5f;
    void Update()
    {
        Vector2Int newCoord = Vector2Int.FloorToInt(transform.position);
        if (newCoord != coord) UpdateCoord(newCoord);
    }
    void UpdateCoord(Vector2Int newCoord)
    {
        coord = newCoord;
    }
    public void Move(Vector2 delta, bool isRunning)
    {

        float moveSpeed = walkSpeed;
        if (isRunning) moveSpeed *= runRate;
        transform.position += (Vector3)(delta * moveSpeed * Time.deltaTime);
    }

}