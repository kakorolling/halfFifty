using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 참고: https://ansohxxn.github.io/unity%20lesson%203/ch4/
public class StatusController : MonoBehaviour
{
    // hp
    [SerializeField]
    private int hp;
    private int currentHp;

    // 스태미나
    [SerializeField]
    private int sp;
    private int currentSp;

    // 스태미나 증가량
    [SerializeField]
    private int spIncreaseSpeed;

    // 스태미나 재회복 딜레이
    [SerializeField]
    private float spRechargeTime;
    private int currentSpRechargeTime;
    
    // 스태미나 감소 여부
    [SerializeField]
    private bool spUsed;

    // 허기
    [SerializeField]
    private int hungry;
    private int currentHungry;

    //허기 줄어드는 속도
    [SerializeField]
    private float hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    //필요한 이미지
    [SerializeField]
    private Image[] image_guage;
    private const int HP = 0, SP =1, HUNGRY =2;

    void Start()
    {
        currentHp = hp;
        currentHungry = hungry;
        currentSp = sp;
    }

    void Update()
    {
        GaugeUpdate();
        Hungry();
        SPRechargeTime();
        SPRecover();
    }
    
    private void GaugeUpdate()
    {
        image_guage[HP].fillAmount = (float)currentHp / hp;
        image_guage[SP].fillAmount = (float)currentSp / sp;
        image_guage[HUNGRY].fillAmount = (float)currentHungry / hungry;
    }

    private void Hungry()
    {
        if(currentHungry >0)
        {
            if(currentHungryDecreaseTime <= hungryDecreaseTime)
                currentHungryDecreaseTime++;
            else
            {
                currentHungry --;
                currentHungryDecreaseTime = 0;
            }
        }
    }

    //다른곳에서 배고픔 증가 처리 할 때 사용
    public void IncreaseHungry(int _count)
    {
        if(currentHungry + _count < hungry)
            currentHungry += _count;
        else
            currentHungry = hungry;
    }

    //인스펙터 창에서 스태미너 사용 중 표시 및 음수 방지
    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if(currentSp - _count > 0)
        {
            currentSp -= _count;
        }
        else
            currentSp = 0;
    }

    // 스태미너 재충전 제어
    private void SPRechargeTime()
    {
        if(spUsed)
        {
            if(currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
                spUsed = false;
        }

    }
    
    //스태미나 미사용 시 스태미나 충전
    private void SPRecover()
    {
        if(!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    //현재 스태미나 리턴
    public int GetCurrentSP()
    {
        return currentSp;
    }
}
