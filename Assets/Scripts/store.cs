using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store : MonoBehaviour
{
    int StoreCount;

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
    }
}
