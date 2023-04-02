using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerController playerCon;
    public InteractionController interactionCon;

    public static bool isPause = false; // pause 기본 false 상태


    void Awake()
    {
        //싱글톤 패턴
        if (instance != null) Destroy(gameObject);

        instance = this;//이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
        DontDestroyOnLoad(gameObject);//씬 전환이 되더라도 파괴되지 않게 한다.

    }

}
