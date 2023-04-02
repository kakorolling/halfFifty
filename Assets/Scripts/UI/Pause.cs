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
        if(Input.GetKeyDown(KeyCode.P)){
            if(!GameManager.isPause)
                CallMenu();
            else
            {
                CloseMenu();
            }
        }
        
    }
    private void CallMenu(){
        GameManager.isPause=true;
        Pause_UI.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseMenu(){
        GameManager.isPause=false;
        Pause_UI.SetActive(false);
        Time.timeScale=1f;
    }
}
