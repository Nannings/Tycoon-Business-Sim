using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    float CurrentBalance;
    public Text CurrentBalanceText;

    private void Start()
    {
        CurrentBalance = 2.00f;
        CurrentBalanceText.text = CurrentBalance.ToString("C2");
    }

    private void Update()
    {
        
    }

    public void AddToBalance(float amount)
    {
        CurrentBalance += amount;
        CurrentBalanceText.text = CurrentBalance.ToString("C2");
    }

    public bool CanBuy(float AmtToSpend)
    {
        return AmtToSpend <= CurrentBalance;
    }
}
