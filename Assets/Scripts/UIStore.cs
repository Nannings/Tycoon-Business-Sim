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

    private void Awake()
    {
        Store = GetComponent<store>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        ProgressSlider.value = Store.CurrentTimer / Store.StoreTimer;
    }
}
