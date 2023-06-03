using UnityEngine;

public class Fishing : MonoBehaviour
{
    public float fishingRange = 2f; 

    public float[] fishingTimes; 
    public GameObject[] fishPrefabs; 

    public bool isFishing = false;

    private Animator animator;
    string animationState = "AnimationState";

     enum FarmerFishingStates
    {
        up = 17,
        down = 18,
        left = 19,
        right = 20
    } 

    //낚시 가능지역인지 검사
    public void TryStartFishing()
    {
        GetComponent<SamplePlayerController>().enabled = false;
        animator = GetComponent<Animator>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fishingRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("river"))
            {
                if(collider.transform.position.x < transform.position.x)
                {
                    animator.SetInteger(animationState, (int)FarmerFishingStates.left);
                }
                else if(collider.transform.position.x > transform.position.x)
                {
                    animator.SetInteger(animationState, (int)FarmerFishingStates.right);
                }
                StartFishing();
                break;
            }
        }
    }

    private void StartFishing()
    {
        isFishing = true;
        float fishingTime = GetRandomFishingTime();
        
        Debug.Log("낚시 시작");
        Debug.Log(fishingTime + " 초");
        
        Invoke("FinishFishing", fishingTime);
    }

    private float GetRandomFishingTime()//낚시시간
    {
        int randomIndex = Random.Range(0, fishingTimes.Length);
        Debug.Log(randomIndex);
        return fishingTimes[randomIndex];
    }

    private int GetRandomFishIndex()
    {
        return Random.Range(0, fishPrefabs.Length);
    }

    private void FinishFishing()
    {
        GetComponent<SamplePlayerController>().enabled = true;
        int fishIndex = GetRandomFishIndex();

        // 물고기 프리팹 생성 및 파괴
        GameObject fishPrefab = fishPrefabs[fishIndex];
        GameObject Gottenfish = Instantiate(fishPrefab, transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
        Destroy(Gottenfish, 1.5f);

        InventoryManager.AddItem(new Item("랍스터", 1, "sea_river_crayfish", "생선"));

        isFishing = false;

        Debug.Log("낚시 완료");
    }
}
