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
        private readonly Dictionary<string, Item> itemDict = new Dictionary<string, Item>();
        
        public void SetItemByTag(string tag, Item item)
        {
            Debug.Log("Set item: " + tag);
            itemDict[tag] = item;
        }

        public Item RetrieveItemByTag(string tag)
        {
            Debug.Log("Trying to retrieve item: " + tag);
            if (itemDict.TryGetValue(tag, out var value))
            {
                Debug.Log("You want: " + value.name);
                return value;
            }
            else
            {
                Debug.Log("Item does not exist with tag: " + tag);
                return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
            
        }
    }
}
