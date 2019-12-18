using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    public WeaponObject weapon; // jeżeli nie ma broni w slocie, to weapon jest nullem
    public List<EnergyCore> EnergyCores = new List<EnergyCore>();

    public Slot()
    {
        EnergyCore toAdd = new EnergyCore
        {
            Level = 1,
            Type = EnergyCore.EnergyCoreType.Profit
        };
        EnergyCores.Add(toAdd);
    }
}
