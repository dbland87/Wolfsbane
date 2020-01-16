using System;
using Inventories.Scripts;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Rendering;
using Valve.VR.InteractionSystem;

namespace Items.Scripts
{
    public class Item : Interactable
    {
        private Collider inventoryCollider;
        private InventoryNode assignedInventory;
        private State state;

        private bool isReturningToInventory = false;
        
        private Action returnToInventoryAction;
        private bool isHoveringInventory => inventoryCollider != null;
        
        public float returnToInventorySpeed;
        public float returnToInventoryDelay;
        
        private void Awake()
        {
            returnToInventoryAction = OnTriggerReturnToInventory;
            state = GetComponentInChildren<State>();
        }
        
        protected override void Update()
        {
            base.Update();
            if (isReturningToInventory)
            {
                MoveObjectToAssignedInventory();
            }
        }

        private void OnEnterInventory(InventoryNode node)
        {
            if (!node.canAcceptItem)
            {
                state.Set(State.ItemState.Error);
            }
        }
        
        private void OnLeaveInventory(InventoryNode node)
        {
            state.Reset();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<InventoryNode>())
            {
                inventoryCollider = other;
                OnEnterInventory((other.gameObject.GetComponent<InventoryNode>()));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == inventoryCollider)
            {
                OnLeaveInventory((other.gameObject.GetComponent<InventoryNode>()));
                inventoryCollider = null;
            }
        }

        private void OnAssignedToInventory(InventoryNode inventoryNode)
        {
            if (inventoryNode.SetItemByTag(
                attachedToHand.handType.ToString(),
                gameObject
            ))
            {
                assignedInventory = inventoryNode;
            }
        }

        private void OnThrown(Rigidbody rigidbody)
        {
            Utils.Utils.AddTimer(gameObject, returnToInventoryDelay, returnToInventoryAction, true);
        }

        private void OnTriggerReturnToInventory()
        {
            isReturningToInventory = true;
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
