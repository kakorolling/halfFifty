using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Mesh
{
    public Vector2Int offset;
    public Vector2Int[] points;
    public Vector2Int[] colliderPoints;

    //offset 1,1
    //메쉬값 1,0 / 0,1 / 1,1 / 2,1 / 1,2
    //collider는 Mesh에 종속
}



