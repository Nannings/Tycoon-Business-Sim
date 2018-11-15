using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    public Text StoreCountText;

    public Slider ProgressSlider;
    public Text BuyButtonText;
    public Button BuyButton;

    public store Store;

    private void OnEnable()
    {
        gamemanager.OnUpdateBalance += UpdateUI;
    }

    private void OnDisable()
    {
        gamemanager.OnUpdateBalance -= UpdateUI;
    }

    private void Awake()
    {
        Store = GetComponent<store>();
    }

    private void Start()
    {
        StoreCountText.text = Store.StoreCount.ToString();
    }

    private void Update()
    {
        ProgressSlider.value = Store.GetCurrentTimer() / Store.GetStoreTimer();
    }

    public void UpdateUI()
    {
        CanvasGroup cg = transform.GetComponent<CanvasGroup>();
        if (!Store.StoreUnlocked && !gamemanager.instance.CanBuy(Store.GetNextStoreCost()))
        {
            cg.interactable = false;
            cg.alpha = 0;
        }
        else
        {
            cg.interactable = true;
            cg.alpha = 1;
            Store.StoreUnlocked = true;
        }

        if (gamemanager.instance.CanBuy(Store.GetNextStoreCost()))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;

        BuyButtonText.text = "Buy " + Store.GetNextStoreCost().ToString("C2");
    }

    public void BuyStoreOnClick()
    {
        if (!gamemanager.instance.CanBuy(Store.GetNextStoreCost()))
            return;
        Store.BuyStore();
        StoreCountText.text = Store.StoreCount.ToString();
        UpdateUI();
    }

    public void OnTimerClick()
    {
        Store.OnStartTimer();
    }
}
