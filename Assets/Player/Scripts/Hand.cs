using System;
using Items.Weapons;
using Inventory.Scripts;
using Items.Scripts;
using Items.Weapons.Scripts;

namespace Player.Scripts
{
    public class Hand : Valve.VR.InteractionSystem.Hand
    {

        public Inventory.Scripts.Inventory inventory;
        public Item grippedItem;

        private bool isArmed => grippedItem is Weapon;
        private bool isHoveringInventory => hoveringInteractable is Inventory.Scripts.Inventory;

        public void OnGrabGripDown()
        {
            if (isHoveringInventory)
            {
                inventory.RetrieveItemByTag(handType.ToString());
            }
        }

        public void OnGrabGripUp()
        {
            if (isArmed)
            {
                (grippedItem as Weapon)?.OnReleaseGrip();
            }
        }
    }
}
