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
    public bool ManagerUnlocked;
    public float StoreMultiplier;
    public bool StoreUnlocked;
    public int StoreTimerDivision = 20;
    public float NextStoreCost;
    public float CurrentTimer = 0;
    public bool StartTimer;

    private void Start()
    {
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
                gamemanager.instance.AddToBalance(BaseStoreProfit * StoreCount);
            }
        }
    }

    public void BuyStore()
    {
        StoreCount += 1;
        gamemanager.instance.AddToBalance(-NextStoreCost);
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        if (StoreCount % StoreTimerDivision == 0)
            StoreTimer = StoreTimer / 2;
    }

    public void OnStartTimer()
    {
        if (!StartTimer && StoreCount > 0)
            StartTimer = true;
    }
}
