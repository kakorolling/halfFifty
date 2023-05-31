using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTf;
    Coroutine coTraceTarget = null;
    public void StartTraceTarget()
    {
        if (coTraceTarget != null) return;
        coTraceTarget = StartCoroutine(TraceTarget());
    }
    public void StopTraceTarget()
    {
        if (coTraceTarget == null) return;
        StopCoroutine(coTraceTarget);
        coTraceTarget = null;
    }
    IEnumerator TraceTarget()
    {
        while (true)
        {
            if (targetTf == null)
            {
                GameObject playerObj = GameManager.instance.playerObj;
                if (playerObj == null)
                {
                    yield return null;
                    continue;
                }
                targetTf = playerObj.transform;
            }
            transform.position = new Vector3(0, 0, -10) + (Vector3)Vector2.Lerp(transform.position, targetTf.position, 0.01f);
            yield return null;
        }
    }
}
