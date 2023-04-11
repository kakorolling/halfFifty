using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mesh : Property
{
    public Vector2Int offset;
    public Vector2Int[] pointArr;
    public Vector2Int[] colliderPointArr;

    public override string GetData()
    {
        MeshData data = new MeshData();
        data.offset = offset;
        data.pointArr = pointArr;
        data.colliderPointArr = colliderPointArr;
        string json = JsonUtility.ToJson(data);
        return json;
    }

    //offset 1,1
    //메쉬값 1,0 / 0,1 / 1,1 / 2,1 / 1,2
    //collider는 Mesh에 종속

    public override void SetData(string json)
    {
        MeshData data = JsonUtility.FromJson<MeshData>(json);
        offset = data.offset;
        pointArr = data.pointArr;
        colliderPointArr = data.colliderPointArr;
    }


}
public class MeshData
{
    public Vector2Int offset;
    public Vector2Int[] pointArr;
    public Vector2Int[] colliderPointArr;
}



