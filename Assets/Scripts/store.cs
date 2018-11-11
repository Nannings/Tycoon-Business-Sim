﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    public float BaseStoreCost;
    public float BaseStoreProfit;
    public float StoreTimer = 4f;
    public int StoreCount;
    public bool ManagerUnlocked;
    public float StoreMultiplier;

    public Text StoreCountText;

    public Slider ProgressSlider;
    public gamemanager Gamemanager;

    private float NextStoreCost;
    float CurrentTimer = 0;
    bool StartTimer;

    private void Start()
    {
        StoreCountText.text = StoreCount.ToString();
        StartTimer = false;
        NextStoreCost = BaseStoreCost;
    }

    private void Update()
    {
        if(StartTimer)
        {
            CurrentTimer += Time.deltaTime;
            if(CurrentTimer > StoreTimer)
            {
                if(!ManagerUnlocked)
                    StartTimer = false;
                CurrentTimer = 0;
                Gamemanager.AddToBalance(BaseStoreProfit * StoreCount);
            }
        }
        ProgressSlider.value = CurrentTimer / StoreTimer;
    }

    public void BuyStoreOnClick()
    {
        if (!Gamemanager.CanBuy(NextStoreCost))
            return;
        StoreCount += 1;
        StoreCountText.text = StoreCount.ToString();
        Gamemanager.AddToBalance(-NextStoreCost);
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        
    }

    public void StoreOnClick()
    {
        if (!StartTimer)
            StartTimer = true;
    }
}
