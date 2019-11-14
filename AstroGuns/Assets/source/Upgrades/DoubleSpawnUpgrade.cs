using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoubleSpawnUpgrade : Upgrade
{
    private float DoubleChance { get => (CurrentLevel - 1) * 0.25f / 100; }
    public override string GetDescription()
    {
        return string.Format("{0}% chance to spawn an extra item", Math.Round((double)(DoubleChance) * 100, 2));
    }

    public override double GetUpgradeCost()
    {
        return Math.Round(0.146 * Math.Pow(CurrentLevel, 10), MidpointRounding.AwayFromZero);
    }

    public override void Increase()
    {
        base.Increase();
        Spawner.DoubleSpawnChance = DoubleChance;
    }
}
