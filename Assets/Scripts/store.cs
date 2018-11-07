using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    int StoreCount;
    public Text StoreCountText;

    private void Start()
    {
        StoreCount = 1;
    }

    private void Update()
    {
        
    }

    public void BuyStoreOnClick()
    {
        StoreCount += 1;
        StoreCountText.text = StoreCount.ToString();
    }
}
