using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpawner : MonoBehaviour
{
    public float SpawnProgress
    {
        get => CurrentProgress;
    }

    private float CurrentProgress = 0.0f;
    public float SpawnInterval = 5.0f;
    public Slider ProgressSlider;


    private void Update()
    {
        AddTime(Time.deltaTime);
        ProgressSlider.value = SpawnProgress;
        Debug.Log(CurrentProgress);
    }

    public void AddTime(float timeInSecons)
    {
        CurrentProgress += timeInSecons / SpawnInterval;
        while(CurrentProgress > 1)
        {
            CurrentProgress -= 1;
            SpawnWeaponRepetitively();
        }
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
