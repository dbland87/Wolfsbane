using System;
using UnityEngine;
using Valve.VR;

namespace Player.Scripts
{
    public class WBHand : Valve.VR.InteractionSystem.Hand
    {

        public WBInventory Inventory;

        private void MaterializeWeapon(string weaponTag)
        {
            Inventory.RetrieveItemByTag(weaponTag);
        }

        private void DematerializeWeapon()
        {
            
        }
        
        public void OnGrabGripDown()
        {
            if (hoveringInteractable == null)
            {
                MaterializeWeapon(handType.ToString());
            }
            
        }

        public void OnGrabGripUp()
        {
            DematerializeWeapon();
        }
    }
}
