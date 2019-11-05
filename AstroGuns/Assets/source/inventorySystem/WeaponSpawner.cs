using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnWeaponRepetitively", 0.5f, 1f);
    }

    void SpawnWeaponRepetitively()
    {
        int slot = FirstEmptySlot();
        if(slot != -1)
        {
            setWeaponData(slot, Inventory.weaponSpawnLevel);
            resetWeaponView(slot);
        }        
    }

    int FirstEmptySlot()
    {
        for (int i = 0; i < Inventory.SLOT_QUANTITY; i++)
        {
            if (Inventory.Slots[i].weapon == null)
            {
                return i;
            }
        }

        return -1; // jeżeli wszystkie zajęte
    }

    void setWeaponData(int slotNumber, int weaponLevel)
    {
        Inventory.Slots[slotNumber].weapon = InventorySystem.Instance.weaponObjects[weaponLevel];
    }

    void resetWeaponView(int slotNumber)
    {
        if (Inventory.Slots[slotNumber].weapon != null)
        {
            InventorySystem.Instance.weaponsInSlots[slotNumber].GetComponent<Image>().sprite = Inventory.Slots[slotNumber].weapon.sprite;
            InventorySystem.Instance.weaponsInSlots[slotNumber].SetActive(true);
        }
        else
        {
            InventorySystem.Instance.weaponsInSlots[slotNumber].SetActive(false);
        }
    }
}
