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

    public Text StoreCountText;

    public Slider ProgressSlider;
    public gamemanager Gamemanager;
    public Text BuyButtonText;
    public Button BuyButton;

    private float NextStoreCost;
    float CurrentTimer = 0;
    bool StartTimer;

    private void Start()
    {
        StoreCountText.text = StoreCount.ToString();
        StartTimer = false;
        NextStoreCost = BaseStoreCost;
        BuyButtonUpdate();
    }

    private void BuyButtonUpdate()
    {
        BuyButtonText.text = "Buy " + NextStoreCost.ToString("C2");
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
        CheckStoreBuy();
    }

    public void CheckStoreBuy()
    {
        CanvasGroup cg = transform.GetComponent<CanvasGroup>();
        if (!StoreUnlocked && !Gamemanager.CanBuy(NextStoreCost))
        {
            cg.interactable = false;
            cg.alpha = 0;
        }
        else
        {
            cg.interactable = true;
            cg.alpha = 1;
            StoreUnlocked = true;
        }

        if (Gamemanager.CanBuy(NextStoreCost))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;
    }

    public void BuyStoreOnClick()
    {
        if (!Gamemanager.CanBuy(NextStoreCost))
            return;
        StoreCount += 1;
        StoreCountText.text = StoreCount.ToString();
        Gamemanager.AddToBalance(-NextStoreCost);
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        BuyButtonUpdate();
    }

    public void StoreOnClick()
    {
        if (!StartTimer)
            StartTimer = true;
    }
}
