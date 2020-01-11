using Inventories.Scripts;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Items.Scripts
{
    public class Item : Interactable
    {
        private Collider inventoryCollider;
        private bool isHoveringInventory => inventoryCollider != null;
        
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
        
        public void OnReleaseGrip()
        {
            Debug.Log("On release grip");
            if (isHoveringInventory && attachedToHand != null)
            {
                inventoryCollider.GetComponent<Inventory>()
                    .SetItemByTag(
                        attachedToHand.handType.ToString(), 
                        gameObject
                    );
            }
        }
    }
}
