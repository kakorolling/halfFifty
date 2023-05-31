using UnityEngine;
using System.Collections;

public class cmove : MonoBehaviour
{
    public float speed = 5f; 
    public float chopDuration = 3f; 
    public GameObject rockPrefab;
    private Rigidbody2D rb;
    private Collider2D currentTreeCollider; 
    public GameObject leafPrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY);

        if (movement != Vector2.zero)
        {
            rb.velocity = movement * speed;
            transform.up = movement;
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TryStartChop();
        }
    }

    private void TryStartChop()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);//주변에 콜라이더오브젝트탐색

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Tree"))//콜라이더 오브젝트의 태그가 tree인지 확인
            {
                Debug.Log("벌목을 시작합니다: " + collider.gameObject.name);
                currentTreeCollider = collider;
                StartCoroutine(Chopstart());
               
                break;
            }
        }
    }

    private IEnumerator Chopstart()//벌목이 ~초후에 완료되게 하는 함수
    {
        yield return new WaitForSeconds(chopDuration);

        FinishChop();
    }
    private IEnumerator DestroyAfterDelay(GameObject targetObject, float delay)//오브젝트 ~초후에 파괴하게하는함수
    {
        yield return new WaitForSeconds(delay);
        Destroy(targetObject);
    }

    private void FinishChop()
    {
        Debug.Log("벌목이 완료되었습니다.");

        if (currentTreeCollider != null)
        {   
            //나무파괴
            Destroy(currentTreeCollider.gameObject); 

            //바위,나뭇잎 등장
            GameObject rockObject = Instantiate(rockPrefab, currentTreeCollider.transform.position, Quaternion.identity);
            GameObject leafObjectLeft = Instantiate(leafPrefab, rockObject.transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
            GameObject leafObjectRight = Instantiate(leafPrefab, rockObject.transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            //나뭇잎 2초뒤 파괴
            StartCoroutine(DestroyAfterDelay(leafObjectLeft, 2f));
            StartCoroutine(DestroyAfterDelay(leafObjectRight, 2f));

            currentTreeCollider = null;
        }
    }
    }