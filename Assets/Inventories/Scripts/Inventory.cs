using System.Collections.Generic;
using Items.Scripts;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Inventories.Scripts
{
    public class Inventory : MonoBehaviour
    {
        private readonly Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

        public void SetItemByTag(string tag, Item item)
        {
            itemDict[tag] = item;
        }

        public Item RetrieveItemByTag(string tag)
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
}
