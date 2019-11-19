using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDropSystem : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // wyłączenie przesuwanej broni
        InventorySystem.Instance.movableWeapon.gameObject.SetActive(false);        

        // sprawdzenie numeru slota
        for (int i = 0; i < InventorySystem.Instance.slots.Count; i++)
        {
            if (this.gameObject.Equals(InventorySystem.Instance.slots[i].gameObject))
            {
                InventorySystem.lastDropedSlotNumber = i;

                break;
            }
        }
        // drop on self
        if(InventorySystem.lastDropedSlotNumber == InventorySystem.lastMovedSlotNumber)
            return;

        // właczenie grafiki
        InventorySystem.Instance.weaponsInSlots[InventorySystem.lastMovedSlotNumber].GetComponent<Image>().enabled = true;


        // jeżeli jest pusty
        if (Inventory.slots[InventorySystem.lastDropedSlotNumber].weapon == null)
        {
            // dodawanie broni do nowego
            WeaponSpawner.setWeaponData(InventorySystem.lastDropedSlotNumber, Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id);

            // usuwanie broni ze starego
            WeaponSpawner.setWeaponData(InventorySystem.lastMovedSlotNumber, -1);

            // update widoku slotów
            WeaponSpawner.resetWeaponView(InventorySystem.lastDropedSlotNumber);
            WeaponSpawner.resetWeaponView(InventorySystem.lastMovedSlotNumber);

            AudioManager.Instance.Play("move weapon");
        }
        // jeżeli jest taka sama broń
        else if (Inventory.slots[InventorySystem.lastDropedSlotNumber].weapon.id == 
            Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id) 
        {
			int levelHigh = 1;
			if(BoostManager.Instance.IsActive())
			{
				Boost b = BoostManager.Instance.GetCurrentBoost();
				if(b.type == BoostType.MergeHigher)
				{
					levelHigh += (int)b.value;
				}
			}
			// dodawanie broni do nowego
			WeaponSpawner.setWeaponData(InventorySystem.lastDropedSlotNumber, Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id + levelHigh); // to +1 to później będzie lvl o jaki upgrejdujemy przy merge

            // usuwanie broni ze starego
            WeaponSpawner.setWeaponData(InventorySystem.lastMovedSlotNumber, -1);

            // update widoku slotów
            WeaponSpawner.resetWeaponView(InventorySystem.lastDropedSlotNumber);
            WeaponSpawner.resetWeaponView(InventorySystem.lastMovedSlotNumber);

            AudioManager.Instance.Play("merge weapon");
        }
        // jeżeli jest inna broń
        else if(Inventory.slots[InventorySystem.lastDropedSlotNumber].weapon.id !=
            Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id)
        {
            // id podniesionej broni
            int dragId = Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id;

            // id broni na upuszczonym miejscu
            int dropId = Inventory.slots[InventorySystem.lastDropedSlotNumber].weapon.id;


            // zmiana broni z w upuszczonym miejscu
            WeaponSpawner.setWeaponData(InventorySystem.lastDropedSlotNumber, dragId);

            // zmiana broni z miejsca z którego podnieslismy
            WeaponSpawner.setWeaponData(InventorySystem.lastMovedSlotNumber, dropId);

            // update widoku slotów
            WeaponSpawner.resetWeaponView(InventorySystem.lastDropedSlotNumber);
            WeaponSpawner.resetWeaponView(InventorySystem.lastMovedSlotNumber);
        }
    }
}
