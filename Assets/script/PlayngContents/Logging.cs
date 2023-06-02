using UnityEngine;
using System.Collections;

public class Logging : MonoBehaviour
{
    //public float speed = 5f; 
    
    public float LoggingDuration = 3f; 
    public GameObject TreeStumpPrefab;
    public GameObject WoodPrefab;
    private Rigidbody2D rb;
    private Collider2D currentTreeCollider;

    private Animator animator;
    string animationState = "AnimationState";
    public float duration = 3f;
    public AnimationClip animationClip;

    //QuickSlotController의 Usingitem을 사용하기 위한 참조
    private QuickSlotController QuickSlotController;

    enum FarmerLoggingStates
    {
        left = 15,
        right = 16
    }

    public void TryStartLogging()
    {
        //주변에 콜라이더 오브젝트 탐색
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f);

        animator = GetComponent<Animator>();
       
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("good");
            // 탐색된 콜라이더 오브젝트의 태그가 tree인지 확인
            if (collider.CompareTag("Tree"))
            {
                Debug.Log("벌목을 시작합니다: " + collider.gameObject.name);
                currentTreeCollider = collider;
                //animator.SetInteger(animationState, (int)FarmerLoggingStates.left);
                //StartCoroutine(PlayLoggingAnimation());
                //animator.SetTrigger("Logging_left");
                StartCoroutine(Loggingstart());
                break;
            }
        }
            
    }


    //벌목이 ~초후에 완료되게 하는 함수
    private IEnumerator Loggingstart()
    {
        
        yield return new WaitForSeconds(LoggingDuration);

        

        
        FinishLogging();
    }

    //오브젝트 ~초후에 파괴하게하는함수
    private IEnumerator DestroyAfterDelay(GameObject targetObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(targetObject);
    }

    private void FinishLogging()
    {
        QuickSlotController = FindObjectOfType<QuickSlotController>();
        Item Usingitem = QuickSlotController.Usingitem;
        Debug.Log("벌목이 완료되었습니다.");

        if(Usingitem.GetName() == "")
        {
            if (currentTreeCollider != null)
            {   
                //나무 파괴
                Destroy(currentTreeCollider.gameObject); 

                //나무 그루터기, 나무 아이템 생성
                GameObject TreeStumpObject = Instantiate(TreeStumpPrefab, currentTreeCollider.transform.position, Quaternion.identity);
                GameObject WoodObject1 = Instantiate(WoodPrefab, TreeStumpObject.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);

                //인벤토리에 나무 도막 아이템 추가
                InventoryManager.AddItem(new Item("나무 도막", 1, "Wood", "재료"));
                
                //나무 아이템 3초 뒤 제거
                StartCoroutine(DestroyAfterDelay(WoodObject1, 3f));

                currentTreeCollider = null;
            }
        }
        
        else if(Usingitem.GetImageName() == "WoodAxe")
        {
            if (currentTreeCollider != null)
            {   
                //나무파괴
                Destroy(currentTreeCollider.gameObject); 

                //나무 그루터기, 나무 아이템 생성
                GameObject TreeStumpObject = Instantiate(TreeStumpPrefab, currentTreeCollider.transform.position, Quaternion.identity);
                GameObject WoodObject1 = Instantiate(WoodPrefab, TreeStumpObject.transform.position + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity);
                GameObject WoodObject2 = Instantiate(WoodPrefab, TreeStumpObject.transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity);

                //인벤토리에 나무 도막 아이템 추가
                InventoryManager.AddItem(new Item("나무 도막", 2, "Wood", "재료"));
                
                //나뭇 아이템 3초 뒤 제거
                StartCoroutine(DestroyAfterDelay(WoodObject1, 3f));
                StartCoroutine(DestroyAfterDelay(WoodObject2, 3f));

                currentTreeCollider = null;
            }
        }
            
    }
}