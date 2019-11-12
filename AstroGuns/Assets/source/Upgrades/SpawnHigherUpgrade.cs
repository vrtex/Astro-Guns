using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHigherUpgrade : Upgrade
{
    public float Chance { get => (CurrentLevel - 1) * 0.1f; }
    public override string GetDescription()
    {
        return string.Format("Item have a {0}% chance to spawn 1 level higher", Chance);
    }

    public override double GetUpgradeCost()
    {
        return 0.000868 * Math.Pow(CurrentLevel, 13);
    }
}