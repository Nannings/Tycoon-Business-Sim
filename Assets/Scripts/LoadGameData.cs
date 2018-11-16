﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadGameData : MonoBehaviour
{
    public TextAsset GameData;

    private void Start()
    {
        Invoke("LoadData", .1f);
    }

    public void LoadData()
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(GameData.text);

        XmlNodeList StoreList = xmlDoc.GetElementsByTagName("store");

        foreach (XmlNode StoreInfo in StoreList)
        {
            XmlNodeList StoreNodes = StoreInfo.ChildNodes;
            foreach (XmlNode StoreNode in StoreNodes)
            {
                Debug.Log(StoreNode.Name);
                Debug.Log(StoreNode.InnerText);
            }
        }
    }
}
