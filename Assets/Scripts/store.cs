using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    float CurrentBalance;
    float BaseStoreCost;
    float BaseStoreProfit;

    int StoreCount;
    public Text StoreCountText;
    public Text CurrentBalanceText;

    float StoreTimer = 4f;
    float CurrentTimer = 0;
    bool StartTimer;

    private void Start()
    {
        StoreCount = 1;
        CurrentBalance = 2.00f;
        BaseStoreCost = 1.50f;
        BaseStoreProfit = .50f;
        CurrentBalanceText.text = CurrentBalance.ToString("C2");
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
                CurrentBalance += BaseStoreProfit * StoreCount;
                CurrentBalanceText.text = CurrentBalance.ToString("C2");
            }
        }
    }

    public void BuyStoreOnClick()
    {
        if (BaseStoreCost > CurrentBalance)
            return;
        StoreCount += 1;
        StoreCountText.text = StoreCount.ToString();
        CurrentBalance = CurrentBalance - BaseStoreCost;
        CurrentBalanceText.text = CurrentBalance.ToString("C2");
    }

    public void StoreOnClick()
    {
        if (!StartTimer)
            StartTimer = true;

    }
}
