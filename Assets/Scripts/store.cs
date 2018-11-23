using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    public delegate void ManagerUnlockDelegate();
    public static event ManagerUnlockDelegate OnManagerUnlocked;

    public string StoreName;
    public float BaseStoreCost;
    public float BaseStoreProfit;
    public float StoreTimer;
    public int StoreCount;
    public bool ManagerUnlocked;
    public float StoreMultiplier;
    public bool StoreUnlocked;
    public int StoreTimerDivision;
    float NextStoreCost;
    float CurrentTimer = 0;
    public float ManagerCost;
    public bool StartTimer;

    private void Start()
    {
        StartTimer = false;
    }

    public float GetNextStoreCost()
    {
        return NextStoreCost;
    }

    public float GetCurrentTimer()
    {
        return CurrentTimer;
    }

    public float GetStoreTimer()
    {
        return StoreTimer;
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
                gamemanager.instance.AddToBalance(BaseStoreProfit * StoreCount);
            }
        }
    }

    public void BuyStore()
    {
        StoreCount += 1;

        float Amt = -NextStoreCost;
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        gamemanager.instance.AddToBalance(Amt);

        if (StoreCount % StoreTimerDivision == 0)
            StoreTimer = StoreTimer / 2;
    }

    public void OnStartTimer()
    {
        if (!StartTimer && StoreCount > 0)
            StartTimer = true;
    }

    public void SetNextStoreCost(float baseStoreCost)
    {
        NextStoreCost = baseStoreCost;
    }

    public void UnlockManager()
    {
        if (ManagerUnlocked)
            return;
        if (gamemanager.instance.CanBuy(ManagerCost))
        {
            gamemanager.instance.AddToBalance(-ManagerCost);
            ManagerUnlocked = true;
            if (OnManagerUnlocked != null)
                OnManagerUnlocked();
        }
    }
}
