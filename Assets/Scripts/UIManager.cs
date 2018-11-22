using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum State
    {
        Main,
        Managers
    }

    public Text CurrentBalanceText;
    public Text CompanyNameText;

    public State CurrentState;
    public GameObject ManagerPanel;

    private void Start()
    {
        CurrentState = State.Main;
    }

    private void OnEnable()
    {
        gamemanager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;
    }

    void onShowManagers()
    {
        CurrentState = State.Managers;
        ManagerPanel.SetActive(true);
    }

    void onShowMain()
    {
        CurrentState = State.Main;
        ManagerPanel.SetActive(false);
    }

    public void onClickManagers()
    {
        if (CurrentState == State.Main)
        {
            onShowManagers();
        }
        else
        {
            onShowMain();
        }
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
