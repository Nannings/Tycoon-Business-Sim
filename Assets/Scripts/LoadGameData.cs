using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadGameData : MonoBehaviour
{
    public TextAsset GameData;
    public GameObject StorePrefab;
    public GameObject StorePanel;

    private void Start()
    {
        LoadData();
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
                    storeobj.time = float.Parse(StoreNode.InnerText);
                }

                NewStore.transform.SetParent(StorePanel.transform);
            }
        }
    }
}
