using UnityEngine;

public class Fishing : MonoBehaviour
{
    public float fishingRange = 2f; 

    public float[] fishingTimes = { 10f, 13f, 15f, 17f, 20f }; 
    public GameObject[] fishPrefabs; 

    private bool isFishing = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isFishing)
        {
            TryStartFishing();
        }
    }

    private void TryStartFishing()//낚시 가능지역인지 검사
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fishingRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("river"))
            {
                
                StartFishing();
                break;
            }
        }
    }

    private void StartFishing()
    {
        isFishing = true;
        float fishingTime = GetRandomFishingTime();
        int fishIndex = GetRandomFishIndex();

        
        Debug.Log("낚시 시작");
        Debug.Log(fishingTime + " 초");
        Debug.Log("등급: " + fishIndex);

        //물고기프리펩생성,파괴
        GameObject fishPrefab = fishPrefabs[fishIndex];
        GameObject fish = Instantiate(fishPrefab, transform.position + Vector3.right, Quaternion.identity);
        Destroy(fish, 2f); 

        
        Invoke("FinishFishing", fishingTime);
    }

    private float GetRandomFishingTime()//낚시시간
    {
        int randomIndex = Random.Range(0, fishingTimes.Length);
        return fishingTimes[randomIndex];
    }

    private int GetRandomFishIndex()
    {
        return Random.Range(0, fishPrefabs.Length);
    }

    private void FinishFishing()
    {
        isFishing = false;
        
        Debug.Log("낚시 완료");
    }
}
