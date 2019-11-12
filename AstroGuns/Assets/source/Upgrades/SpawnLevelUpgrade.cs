using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevelUpgrade : Upgrade
{
    public override string GetDescription()
    {
        return string.Format("New item spawn at level {0}", CurrentLevel);
    }

    public override double GetUpgradeCost()
    {
        return 0.03993 * Math.Pow(CurrentLevel, 10);
    }

}
