using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text CurrentBalanceText;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        gamemanager.OnUpdateBalance += UpdateUI;
    }

    private void OnDisable()
    {
        gamemanager.OnUpdateBalance -= UpdateUI;
    }

    public void UpdateUI()
    {
        CurrentBalanceText.text =  gamemanager.instance.GetCurrentBalance().ToString("C2");
    }
}
