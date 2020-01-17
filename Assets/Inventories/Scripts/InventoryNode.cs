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
        public Dictionary<string, GameObject> itemDict = new Dictionary<string, GameObject>();
        public delegate void OnItemSet(string itemTag, GameObject item);
        public static event OnItemSet onItemSet;
        public int MaximumItems = 1;
        public bool isAtCapacity => itemDict.Count >= MaximumItems;

        public Transform objectAttachmentPoint;

        private void Awake()
        {
            onItemSet += PruneDuplicateItems;
        }

        public bool SetItemByTag(string itemTag, GameObject itemObject)
        {
            if (!isAtCapacity)
            {
                itemObject.GetComponent<Item>().attachedToHand.DetachObject(itemObject);
                onItemSet?.Invoke(itemTag, itemObject);
                return itemDict[itemTag] = itemObject;
            }
            return false;
        }

        public GameObject RetrieveItemByTag(string itemTag)
        {
            if (itemDict.TryGetValue(itemTag, out var value))
            {
                return value;
            }
            Debug.LogError("Item does not exist with tag: " + itemTag);
            return null;
        }

        public void PruneDuplicateItems(string itemTag, GameObject item)
        {
            foreach (var pair in itemDict.Where(pair => pair.Value == item && pair.Key != itemTag))
            {
                // If the item exists in this dictionary but the key doesn't match what was most recently assigned,
                // it's been reassigned elsewhere and we need to prune the item from our list
                itemDict.Remove(pair.Key);
            }
        }

        public bool CanAcceptItem(GameObject item)
        {
            return !isAtCapacity || itemDict.ContainsValue(item);
        }

        public void OnItemDiscarded(GameObject item)
        {
            // itemDict.Remove(itemTag);
        }
    }
    
}
