using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject Pause_UI;


    void Start()
    {
        Pause_UI.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.isPause)
                OpenMenu();
            else
            {
                CloseMenu();
            }
        }

    }
    private void OpenMenu()
    { // CallMenu -> OpenMenu: CloseMenu랑 대비되게 함수 이름 바꿈
        GameManager.isPause = true;
        Pause_UI.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseMenu()
    {
        GameManager.isPause = false;
        Pause_UI.SetActive(false);
        Time.timeScale = 1f;
    }
}
