using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoresContainer : MonoBehaviour
{
    [HideInInspector]
    public List<EnergyCore> availibleCores = new List<EnergyCore>();
    [HideInInspector]
    public KeyValuePair<Slot, int> currentCoreSlot;
    [HideInInspector]
    public int currentCoreLevel;
    [HideInInspector]
    public EnergyCore.EnergyCoreType currentCoreType;

    public List<Sprite> profitIcons;
    public List<Sprite> hasteIcons;
    public List<Sprite> fortuneIcons;


    private void Start()
    {
        if(FindObjectsOfType<CoresContainer>().Length > 1)
            Debug.LogError("REEEEEEEEEEEEEEEEEEEEEEEEEEEE");
    }

    public List<EnergyCore> GetCoresOfType(EnergyCore.EnergyCoreType energyCoreType)
    {
        return availibleCores.FindAll((EnergyCore e) => e.Type == energyCoreType);
    }

    public EnergyCore pollCore(int level, EnergyCore.EnergyCoreType type)
    {
        EnergyCore found = availibleCores.Find((EnergyCore e) => e.Type == type && e.Level == level);
        availibleCores.Remove(found);
        return found;
    }

    public bool RemoveCore(int level, EnergyCore.EnergyCoreType type)
    {
        return pollCore(level, type) != null;
    }

    public void AddCore(EnergyCore toAdd)
    {
        if(toAdd == null)
        {
            return;
        }
        availibleCores.Add(toAdd);
    }

    public void SetCurrentCoreSlot(KeyValuePair<Slot, int> p)
    {
        currentCoreSlot = p;
    }

    public bool Equip(int level, EnergyCore.EnergyCoreType type)
    {
        currentCoreSlot.Key.EquipCore(currentCoreSlot.Value, pollCore(level, type));
        return currentCoreSlot.Key.GetCoreAtIndex(currentCoreSlot.Value) != null;
    }

    public int CountCores(int level, EnergyCore.EnergyCoreType energyCoreType)
    {
        return availibleCores.Count((EnergyCore e) => e.Level == level && e.Type == energyCoreType);
    }

    public Sprite GetIconForCore(int level, EnergyCore.EnergyCoreType type)
    {
        List<Sprite> list = type == EnergyCore.EnergyCoreType.Fortune ? fortuneIcons :
            type == EnergyCore.EnergyCoreType.Haste ? hasteIcons :
            profitIcons;
        return list[level];
    }
}
