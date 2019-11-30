using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevelUpgrade : Upgrade
{
    public SpawnLevelUpgrade()
    {
        MaxLevel = 81;
    }

    public override string GetDescription()
    {
        return string.Format("New item spawn at level {0}", CurrentLevel);
    }

    public override double GetUpgradeCost()
    {
        return Math.Round(0.03993 * Math.Pow(CurrentLevel, 10), MidpointRounding.AwayFromZero);
    }

    public override void Increase()
    {
        base.Increase();
        Inventory.weaponSpawnLevel = CurrentLevel;

        List<Slot> slots = Inventory.slots.FindAll((Slot s) => { return s.weapon != null && s.weapon.id < CurrentLevel; });
        foreach(Slot s in slots)
        {
            WeaponSpawner.setWeaponData(s, CurrentLevel);
            WeaponSpawner.resetWeaponView(Inventory.slots.FindIndex((Slot _s) => { return _s == s; }));
        }
    }

	public override void SetLevel(int lvl)
	{
		base.SetLevel(lvl);
		Inventory.weaponSpawnLevel = CurrentLevel;

		List<Slot> slots = Inventory.slots.FindAll((Slot s) => { return s.weapon != null && s.weapon.id < CurrentLevel; });
		foreach(Slot s in slots)
		{
			WeaponSpawner.setWeaponData(s, CurrentLevel);
			WeaponSpawner.resetWeaponView(Inventory.slots.FindIndex((Slot _s) => { return _s == s; }));
		}
	}
}
