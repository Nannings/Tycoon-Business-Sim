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

    public Text StoreCountText;

    public Slider ProgressSlider;
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
                gamemanager.instance.AddToBalance(BaseStoreProfit * StoreCount);
            }
        }
        ProgressSlider.value = CurrentTimer / StoreTimer;
        CheckStoreBuy();
    }

    public void CheckStoreBuy()
    {
        CanvasGroup cg = transform.GetComponent<CanvasGroup>();
        if (!StoreUnlocked && !gamemanager.instance.CanBuy(NextStoreCost))
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

        if (gamemanager.instance.CanBuy(NextStoreCost))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;
    }

    public void BuyStoreOnClick()
    {
        if (!gamemanager.instance.CanBuy(NextStoreCost))
            return;
        StoreCount += 1;
        StoreCountText.text = StoreCount.ToString();
        gamemanager.instance.AddToBalance(-NextStoreCost);
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        if (StoreCount % StoreTimerDivision == 0)
            StoreTimer = StoreTimer / 2;
        BuyButtonUpdate();
    }

    public void StoreOnClick()
    {
        if (!StartTimer && StoreCount > 0)
            StartTimer = true;
    }
}
