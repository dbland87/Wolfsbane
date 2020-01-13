using System;
using Inventories.Scripts;
using Items.Weapons;
using Items.Scripts;
using Items.Weapons.Scripts;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

namespace Player.Scripts
{
    public class Hand : Valve.VR.InteractionSystem.Hand
    {

        public Collider inventoryCollider;
        private bool isHoveringInventory => inventoryCollider != null;

        public void OnGrabGripDown()
        {
            if (isHoveringInventory)
            {
                AttachObject(inventoryCollider.GetComponent<InventoryNode>().RetrieveItemByTag(handType.ToString()), GrabTypes.Grip);
            }
        }

        public void OnGrabGripUp()
        {
            if (currentAttachedObject != null && currentAttachedObject.GetComponent<Item>())
            {
                currentAttachedObject.GetComponent<Item>()?.OnReleaseGrip();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<InventoryNode>())
            {
                inventoryCollider = other;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == inventoryCollider)
            {
                inventoryCollider = null;
            }
        }
    }
}
