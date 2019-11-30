using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeEtherChance : Upgrade
{
    private float Chance { get => (CurrentLevel - 1) * 0.25f / 100; }

    public override string GetDescription()
    {
        return string.Format("Each merge has a {0}% chance of giving a gem", Chance * 100);
    }

    public override double GetUpgradeCost()
    {
        return Math.Round(0.01223 * Math.Pow(CurrentLevel, 10.75), MidpointRounding.AwayFromZero);
    }

    public override void Increase()
    {
        base.Increase();
        Spawner.EtherChance = Chance;
    }

	public override void SetLevel(int lvl)
	{
		base.SetLevel(lvl);
		Spawner.EtherChance = Chance;
	}
}
