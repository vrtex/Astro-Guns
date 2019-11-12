using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimeUpgrade : Upgrade
{
    private float TimeToSpawn { get => 6 - (CurrentLevel * 0.1f); }

    public override string GetDescription()
    {
        return string.Format("New item take {0} seconds to spawn", TimeToSpawn);
    }

    public override double GetUpgradeCost()
    {
        return 0.01447 * Math.Pow(CurrentLevel, 10);
    }
}
