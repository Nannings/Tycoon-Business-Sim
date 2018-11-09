using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    float BaseStoreCost;
    float BaseStoreProfit;

    int StoreCount;
    public Text StoreCountText;
    public Slider ProgressSlider;
    public gamemanager Gamemanager;

    float StoreTimer = 4f;
    float CurrentTimer = 0;
    bool StartTimer;

    private void Start()
    {
        StoreCount = 1;
        BaseStoreCost = 1.50f;
        BaseStoreProfit = .50f;
        StartTimer = false;
    }

    private void Update()
    {
        if(StartTimer)
        {
            CurrentTimer += Time.deltaTime;
            if(CurrentTimer > StoreTimer)
            {
                StartTimer = false;
                CurrentTimer = 0;
                Gamemanager.AddToBalance(BaseStoreProfit * StoreCount);
            }
        }
        ProgressSlider.value = CurrentTimer / StoreTimer;
    }

    public void BuyStoreOnClick()
    {
        //if (BaseStoreCost > CurrentBalance)
        //    return;
        StoreCount += 1;
        StoreCountText.text = StoreCount.ToString();
        Gamemanager.AddToBalance(-BaseStoreCost);
    }

    public void StoreOnClick()
    {
        if (!StartTimer)
            StartTimer = true;
    }
}
