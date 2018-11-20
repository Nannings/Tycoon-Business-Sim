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

        XmlNodeList StoreList = xmlDoc.GetElementsByTagName("store");

        foreach (XmlNode StoreInfo in StoreList)
        {
            GameObject NewStore = (GameObject)Instantiate(StorePrefab);

            store storeobj = NewStore.GetComponent<store>();

            XmlNodeList StoreNodes = StoreInfo.ChildNodes;
            foreach (XmlNode StoreNode in StoreNodes)
            {
                if (StoreNode.Name == "name")
                {
                    Text StoreText = NewStore.transform.Find("StoreNameText").GetComponent<Text>();
                    StoreText.text = StoreNode.InnerText;
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

                storeobj.SetNextStoreCost(storeobj.BaseStoreCost);
                NewStore.transform.SetParent(StorePanel.transform);
            }
        }
    }
}
