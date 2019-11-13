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

        Debug.Log("DROP");
        // sprawdzenie numeru slota
        for (int i = 0; i < InventorySystem.Instance.slots.Count; i++)
        {
            if (this.gameObject.Equals(InventorySystem.Instance.slots[i].gameObject))
            {
                InventorySystem.lastDropedSlotNumber = i;
                Debug.Log("Upuszczony na: " + i);

                break;
            }
        }

        // właczenie grafiki
        InventorySystem.Instance.weaponsInSlots[InventorySystem.lastMovedSlotNumber].GetComponent<Image>().enabled = true;

        // jeżeli jest pusty
        if (Inventory.slots[InventorySystem.lastDropedSlotNumber].weapon == null)
        {
            Debug.Log("EMPTY");
            // dodawanie broni do nowego
            WeaponSpawner.setWeaponData(InventorySystem.lastDropedSlotNumber, Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id);

            // usuwanie broni ze starego
            WeaponSpawner.setWeaponData(InventorySystem.lastMovedSlotNumber, -1);

            // update widoku slotów
            WeaponSpawner.resetWeaponView(InventorySystem.lastDropedSlotNumber);
            WeaponSpawner.resetWeaponView(InventorySystem.lastMovedSlotNumber);
        }
        // jeżeli jest taka sama broń
        else if (Inventory.slots[InventorySystem.lastDropedSlotNumber].weapon.id == 
            Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id) 
        {
            Debug.Log("TAKI SAM");
            // dodawanie broni do nowego
            WeaponSpawner.setWeaponData(InventorySystem.lastDropedSlotNumber, Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id + 1); // to +1 to później będzie lvl o jaki upgrejdujemy przy merge

            // usuwanie broni ze starego
            WeaponSpawner.setWeaponData(InventorySystem.lastMovedSlotNumber, -1);

            // update widoku slotów
            WeaponSpawner.resetWeaponView(InventorySystem.lastDropedSlotNumber);
            WeaponSpawner.resetWeaponView(InventorySystem.lastMovedSlotNumber);
        }
        // jeżeli jest inna broń
        else if(Inventory.slots[InventorySystem.lastDropedSlotNumber].weapon.id !=
            Inventory.slots[InventorySystem.lastMovedSlotNumber].weapon.id)
        {
            Debug.Log("RÓŻNE");
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
