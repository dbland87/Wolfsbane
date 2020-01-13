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
        public delegate void OnItemSet(string itemTag, GameObject item);
        public static event OnItemSet onItemSet;

        public Transform objectAttachmentPoint;

        private void Awake()
        {
            onItemSet += PruneDuplicateItems;
        }

        public void SetItemByTag(string itemTag, GameObject itemObject)
        {
            itemDict[itemTag] = itemObject;
            itemObject.GetComponent<Item>().attachedToHand.DetachObject(itemObject);
            itemObject.SetActive(false);
            onItemSet?.Invoke(itemTag, itemObject);
        }

        public GameObject RetrieveItemByTag(string itemTag)
        {
            if (itemDict.TryGetValue(itemTag, out var value))
            {
                value.SetActive(true);
                return value;
            }
            else
            {
                Debug.LogError("Item does not exist with tag: " + itemTag);
                return null;
            }
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
    }
}
