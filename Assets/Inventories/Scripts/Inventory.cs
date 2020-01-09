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
        public Transform headTransform;
        
        private readonly Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

        void FixedUpdate()
        {
            //TODO this is temp logic
            transform.localPosition = new Vector3(transform.localPosition.x, -headTransform.position.y / 2, transform.localPosition.z);
        }
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
