using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpawner : MonoBehaviour
{

    public float CurrentProgress
    {
        get => SpawnProgress / SpawnInterval;
    }

    private float SpawnProgress;
    public float SpawnInterval = 5.0f;


    private void Update()
    {
        AddTime(Time.deltaTime);

        while(CurrentProgress > 1 && FirstEmptySlot() >= 0)
        {
            SpawnWeaponRepetitively();
            SpawnProgress -= SpawnInterval;
        }

        if(SpawnProgress < 0)
            SpawnProgress = 0;
    }

    public void AddTime(float t)
    {
        SpawnProgress += t;
    }

    void SpawnWeaponRepetitively()
    {
        int slot = FirstEmptySlot();
        setWeaponData(slot, Inventory.weaponSpawnLevel);
        resetWeaponView(slot);
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
