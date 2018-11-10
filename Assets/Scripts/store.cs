using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    public float BaseStoreCost;
    public float BaseStoreProfit;
    public float StoreTimer = 4f;
    public int StoreCount;

    public Text StoreCountText;

    public Slider ProgressSlider;
    public gamemanager Gamemanager;

    float CurrentTimer = 0;
    bool StartTimer;

    private void Start()
    {
        StoreCountText.text = StoreCount.ToString();
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
        if (!Gamemanager.CanBuy(BaseStoreCost))
            return;
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
