using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;

    public static gamemanager instance;
    float CurrentBalance;
    public string CompanyName;

    private void Start()
    {
        if (OnUpdateBalance != null)
            OnUpdateBalance();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddToBalance(float amount)
    {
        CurrentBalance += amount;
        if (OnUpdateBalance != null)
            OnUpdateBalance();
    }

    public bool CanBuy(float AmtToSpend)
    {
        return AmtToSpend <= CurrentBalance;
    }

    public float GetCurrentBalance()
    {
        return CurrentBalance;
    }
}
