using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameData : MonoBehaviour
{

    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;

    public TextAsset GameData;
    public GameObject StorePrefab;
    public GameObject StorePanel;

    public GameObject ManagerPanel;
    public GameObject ManagerPrefab;

    private void Start()
    {
        LoadData();
        if (OnLoadDataComplete != null)
            OnLoadDataComplete();
    }

    public void LoadData()
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(GameData.text);

        LoadGameManagerData(xmlDoc);

        LoadStores(xmlDoc);
    }

    private void LoadGameManagerData(XmlDocument xmlDoc)
    {
        float StartingBalanceNode = float.Parse(xmlDoc.GetElementsByTagName("StartingBalance")[0].InnerText);
        gamemanager.instance.AddToBalance(StartingBalanceNode);

        string CompanyName = xmlDoc.GetElementsByTagName("CompanyName")[0].InnerText;
        gamemanager.instance.CompanyName = CompanyName;
    }

    private void LoadStores(XmlDocument xmlDoc)
    {
        XmlNodeList StoreList = xmlDoc.GetElementsByTagName("store");

        foreach (XmlNode StoreInfo in StoreList)
        {
            LoadStoreNodes(StoreInfo);
        }
    }
    private void LoadStoreNodes(XmlNode StoreInfo)
    {
        GameObject NewStore = (GameObject)Instantiate(StorePrefab);

        store storeobj = NewStore.GetComponent<store>();

        XmlNodeList StoreNodes = StoreInfo.ChildNodes;
        foreach (XmlNode StoreNode in StoreNodes)
        {
            SetStoreObj(NewStore, storeobj, StoreNode);
        }

        storeobj.SetNextStoreCost(storeobj.BaseStoreCost);
        NewStore.transform.SetParent(StorePanel.transform);
    }

    private void SetStoreObj(GameObject NewStore, store storeobj, XmlNode StoreNode)
    {
        if (StoreNode.Name == "name")
        {
            Text StoreText = NewStore.transform.Find("StoreNameText").GetComponent<Text>();
            StoreText.text = StoreNode.InnerText;
            storeobj.StoreName = StoreNode.InnerText;
        }
        if (StoreNode.Name == "image")
        {
            Sprite newSprite = Resources.Load<Sprite>(StoreNode.InnerText);
            Image StoreImage = NewStore.transform.Find("ImageButtonClick").GetComponent<Image>();
            StoreImage.sprite = newSprite;
        }
        if (StoreNode.Name == "BaseStoreCost")
        {
            storeobj.BaseStoreCost = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "BaseStoreProfit")
        {
            storeobj.BaseStoreProfit = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "StoreTimer")
        {
            storeobj.StoreTimer = float.Parse(StoreNode.InnerText);
        }

        if (StoreNode.Name == "StoreMultiplier")
        {
            storeobj.StoreMultiplier = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "StoreTimerDivision")
        {
            storeobj.StoreTimerDivision = int.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "StoreCount")
        {
            storeobj.StoreCount = int.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "ManagerCost")
        {
            CreateManager(StoreNode, storeobj);
        }
    }

    private void CreateManager(XmlNode StoreNode, store storeobj)
    {
        GameObject NewManager = Instantiate(ManagerPrefab);
        NewManager.transform.SetParent(ManagerPanel.transform);
        Text ManagerNameText = NewManager.transform.Find("ManagerNameText").GetComponent<Text>();
        ManagerNameText.text = storeobj.StoreName;
        storeobj.ManagerCost = float.Parse(StoreNode.InnerText);
        Button ManagerButton = NewManager.transform.Find("UnlockManagerButton").GetComponent<Button>();
        Text ButtonText = ManagerButton.transform.Find("UnlockManagerButtonText").GetComponent<Text>();
        ButtonText.text = "Unlock " + storeobj.ManagerCost.ToString("C2");
    }
}
