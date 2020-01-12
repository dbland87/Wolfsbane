using System;
using System.Collections.Generic;
using System.Linq;
using Items.Scripts;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

namespace Inventories.Scripts
{
    public class InventoryNode : MonoBehaviour
    {
        private readonly Dictionary<string, GameObject> itemDict = new Dictionary<string, GameObject>();
        
        public void SetItemByTag(string tag, GameObject itemObject)
        {
            itemDict[tag] = itemObject;
            itemObject.SetActive(false);
            gameObject.GetComponentInParent<InventoryManager>().OnSetItemInChild(tag, itemObject);
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

        public void PruneDuplicateItems(String tag, GameObject item)
        {
            foreach (var pair in itemDict.Where(pair => pair.Value == item && pair.Key != tag))
            {
                // If the item exists in this dictionary but the key doesn't match what was most recently assigned,
                // it's been reassigned elsewhere and we need to prune the item from our list
                itemDict.Remove(pair.Key);
            }
        }
    }
}
