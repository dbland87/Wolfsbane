using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBInventory : MonoBehaviour
{

    private Dictionary<string, string> itemDict;

    public void SetItemByTag(string tag, string item)
    {
        itemDict[tag] = item;
    }

    public string RetrieveItemByTag(string tag)
    {
        if (itemDict.TryGetValue(tag, out var value))
        {
            return value;
        }
        else
        {
            Debug.Log("Item does not exist with tag: " + tag);
            return null;
        }
    }
}
