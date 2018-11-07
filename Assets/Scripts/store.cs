using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    float CurrentBalance;
    float BaseStoreCost;

    int StoreCount;
    public Text StoreCountText;
    public Text CurrentBalanceText;

    private void Start()
    {
        StoreCount = 1;
        CurrentBalance = 2.00f;
        BaseStoreCost = 1.50f;
        CurrentBalanceText.text = CurrentBalance.ToString("C2");
    }

    private void Update()
    {
        
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
}
