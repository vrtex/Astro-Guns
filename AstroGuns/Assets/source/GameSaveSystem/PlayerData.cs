using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int weaponSpawnLevel;
    public int[] weaponsId;

    public PlayerData()
    {
        // level spawnowanej broni
        weaponSpawnLevel = Inventory.weaponSpawnLevel;

        // poziomy broni w slotach, jeżeli nie ma, to -1
        weaponsId = new int[Inventory.SLOT_QUANTITY];
        for(int i = 0; i < Inventory.SLOT_QUANTITY; i++)
        {
            if(Inventory.slots[i].weapon != null)
            {
                weaponsId[i] = Inventory.slots[i].weapon.id;
            }
            else
            {
                weaponsId[i] = -1;
            }
        }
    }
}
