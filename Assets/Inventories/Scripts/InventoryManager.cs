using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories.Scripts
{
    public class InventoryManager : MonoBehaviour
    {

        public InventoryNode[] inventoryNodes;

        public void OnSetItemInChild(String tag, GameObject item)
        {
            foreach (var node in inventoryNodes)
            {
                node.PruneDuplicateItems(tag, item);
            }
        }
    }
}
