using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text CurrentBalanceText;
    public Text CompanyNameText;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        gamemanager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;
    }

    private void OnDisable()
    {
        gamemanager.OnUpdateBalance -= UpdateUI;
        LoadGameData.OnLoadDataComplete -= UpdateUI;
    }

    public void UpdateUI()
    {
        CurrentBalanceText.text =  gamemanager.instance.GetCurrentBalance().ToString("C2");
        CompanyNameText.text = gamemanager.instance.CompanyName;
    }
}
