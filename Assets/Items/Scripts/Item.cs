using System;
using Inventories.Scripts;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Valve.VR.InteractionSystem;

namespace Items.Scripts
{
    public class Item : Interactable
    {
        private Collider inventoryCollider;
        private InventoryNode assignedInventory;
        private bool isHoveringInventory => inventoryCollider != null;
        private bool isReturningToInventory = false;
        public float returnToInventorySpeed;
        
        protected override void Update()
        {
            base.Update();
            if (isReturningToInventory)
            {
                ReturnToAssignedInventory();
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

        private void OnAssignedToInventory(InventoryNode inventoryNode)
        {
            assignedInventory = inventoryNode;
            assignedInventory.SetItemByTag(
                attachedToHand.handType.ToString(), 
                gameObject
            );
        }

        private void AttachToAssignedInventory()
        {
            transform.SetParent(assignedInventory.objectAttachmentPoint.transform);
        }

        public void ReturnToAssignedInventory()
        {
            if (Vector3.Distance(transform.position, assignedInventory.transform.position) < 0.01f)
            {
                isReturningToInventory = false;
                AttachToAssignedInventory();
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().detectCollisions = true;
            }
            else
            {
                var step = returnToInventorySpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, assignedInventory.transform.position, step);
            }
        }
        
        public void OnReleaseGrip()
        {
            if (isHoveringInventory && attachedToHand != null)
            {
                OnAssignedToInventory(inventoryCollider.GetComponent<InventoryNode>());
            } else if (assignedInventory != null)
            {
                isReturningToInventory = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().detectCollisions = false;
            }
        }
    }
}
