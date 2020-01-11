using System;
using Inventories.Scripts;
using Items.Weapons;
using Items.Scripts;
using Items.Weapons.Scripts;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Player.Scripts
{
    public class Hand : Valve.VR.InteractionSystem.Hand
    {

        private Collider inventoryCollider;
        private bool isHoveringInventory => inventoryCollider != null;

        public void OnGrabGripDown()
        {
            if (isHoveringInventory)
            {
                AttachObject(inventoryCollider.GetComponent<Inventory>().RetrieveItemByTag(handType.ToString()), GrabTypes.Grip);
            }
        }

        public void OnGrabGripUp()
        {
            Debug.Log("OnGrabGripUp");
            if (currentAttachedObject != null && currentAttachedObject.CompareTag("Item"))
            {
                currentAttachedObject.GetComponent<Item>()?.OnReleaseGrip();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Inventory"))
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
