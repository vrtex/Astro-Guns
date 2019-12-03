using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpawner : MonoBehaviour
{
    public float DoubleSpawnChance = 0.0f;
    public float HigherSpawnChance = 0.0f;
    public float EtherChance = 0.0f;

    public float CurrentProgress
    {
        get => SpawnProgress / SpawnInterval;
    }

    private float SpawnProgress;
    public float SpawnInterval = 6.0f;


    private void Update()
    {
		float currTime = Time.deltaTime;
		if(BoostManager.Instance.IsActive())
		{
			Boost b = BoostManager.Instance.GetCurrentBoost();
			if(b.type == BoostType.FastResearch)
			{
				currTime *= b.value;
			}
		}
		AddTime(currTime);

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
        // TODO: consider first slot's energy core bonus
        SpawnProgress = Mathf.Clamp(SpawnProgress + t, 0, SpawnInterval * 1.01f);
    }

    void SpawnWeaponRepetitively(bool bAllowDoubleSpawn = true)
    {
		int tempSpawnLevel = Inventory.weaponSpawnLevel;
		if(BoostManager.Instance.IsActive())
		{
			Boost b = BoostManager.Instance.GetCurrentBoost();
			if(b.type == BoostType.SpawnHigher)
			{
				tempSpawnLevel += (int)b.value;
			}
		}
		int slot = FirstEmptySlot();
        int spawnLevel = UnityEngine.Random.value < HigherSpawnChance ? tempSpawnLevel + 1 : tempSpawnLevel;
        setWeaponData(slot, spawnLevel);
        resetWeaponView(slot);

        if(!bAllowDoubleSpawn)
            return;

        if(UnityEngine.Random.value > DoubleSpawnChance || FirstEmptySlot() < 0)
            return;

        SpawnWeaponRepetitively(false);

    }

	public void ForceSpawnWeapon()
	{
		SpawnWeaponRepetitively(false);
	}

    int FirstEmptySlot()
    {
        for (int i = 0; i < Inventory.SLOT_QUANTITY; i++)
        {
            if (Inventory.slots[i].weapon == null)
            {
                return i;
            }
        }

        return -1; // jeżeli wszystkie zajęte
    }

    static public void setWeaponData(int slotNumber, int weaponId)
    {
        setWeaponData(Inventory.slots[slotNumber], weaponId);
    }

    static public void setWeaponData(Slot slot, int weaponId)
    {
        slot.weapon = weaponId < 0 ? null : InventorySystem.Instance.weaponObjects[weaponId];
    }


    public static void resetWeaponView(int slotNumber)
    {
        if (Inventory.slots[slotNumber].weapon != null)
        {
            InventorySystem.Instance.weaponsInSlots[slotNumber].GetComponent<Image>().sprite = Inventory.slots[slotNumber].weapon.sprite;
            InventorySystem.Instance.weaponsInSlots[slotNumber].SetActive(true);
        }
        else
        {
            InventorySystem.Instance.weaponsInSlots[slotNumber].SetActive(false);
        }
    }
}
