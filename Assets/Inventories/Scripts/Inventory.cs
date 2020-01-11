using System;
using System.Collections.Generic;
using Items.Scripts;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

namespace Inventories.Scripts
{
    public class Inventory : MonoBehaviour
    {
        private readonly Dictionary<string, GameObject> itemDict = new Dictionary<string, GameObject>();
        
        public void SetItemByTag(string tag, GameObject itemObject)
        {
            itemDict[tag] = itemObject;
            itemObject.SetActive(false);
        }

        public GameObject RetrieveItemByTag(string tag)
        {
            Debug.Log("Trying to retrieve item: " + tag);
            if (itemDict.TryGetValue(tag, out var value))
            {
                Debug.Log("You want: " + value.name);
                value.SetActive(true);
                return value;
            }
            else
            {
                Debug.Log("Item does not exist with tag: " + tag);
                return null;
            }
        }
    }
}
