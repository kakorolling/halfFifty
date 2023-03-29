using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    void Start()
    {

    }

    Animator PlayerAnim;

    void Update()
    {

    }
    public void Move(Vector2 delta)
    {
        transform.position += (Vector3)delta;
    }
    public void Run()
    {

    }
}
