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
        private bool isReturningToInventory = false;
        private Action returnToInventoryAction;
        private bool isHoveringInventory => inventoryCollider != null;
        
        public float returnToInventorySpeed;
        public float returnToInventoryDelay;
        
        private void Awake()
        {
            returnToInventoryAction = OnTriggerReturnToInventory;
        }
        
        protected override void Update()
        {
            base.Update();
            if (isReturningToInventory)
            {
                MoveObjectToAssignedInventory();
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

        private void OnThrown(Rigidbody rigidbody)
        {
            Utils.Utils.AddTimer(gameObject, returnToInventoryDelay, returnToInventoryAction, true);
        }

        private void OnTriggerReturnToInventory()
        {
            isReturningToInventory = true;
            // GetComponent<Rigidbody>().velocity = Vector3.zero;
            // GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().detectCollisions = false;
        }

        private void AttachToAssignedInventory()
        {
            transform.SetParent(assignedInventory.objectAttachmentPoint.transform);
        }

        private void OnObjectArrivedAtInventory(Rigidbody rigidbody)
        {
            isReturningToInventory = false;
            AttachToAssignedInventory();
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.detectCollisions = true;
        }

        public void MoveObjectToAssignedInventory()
        {
            if (Vector3.Distance(transform.position, assignedInventory.transform.position) < 0.01f)
            {
                OnObjectArrivedAtInventory(GetComponent<Rigidbody>());
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
                OnThrown(GetComponent<Rigidbody>());
            }
        }
    }
}
