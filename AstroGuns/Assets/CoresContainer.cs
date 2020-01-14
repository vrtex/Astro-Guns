using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoresContainer : MonoBehaviour
{
    private List<EnergyCore> availibleCores = new List<EnergyCore>();
    private KeyValuePair<Slot, int> currentCoreSlot;

    private void Start()
    {
        if(FindObjectsOfType<CoresContainer>().Length > 1)
            Debug.LogError("REEEEEEEEEEEEEEEEEEEEEEEEEEEE");

        AddCore(new EnergyCore { Level = 0, Type = EnergyCore.EnergyCoreType.Profit });
    }

    public List<EnergyCore> GetCoresOfType(EnergyCore.EnergyCoreType energyCoreType)
    {
        return availibleCores.FindAll((EnergyCore e) => e.Type == energyCoreType);
    }

    public EnergyCore pollCore(int level, EnergyCore.EnergyCoreType type)
    {
        EnergyCore found = availibleCores.Find((EnergyCore e) => e.Type == type && e.Level == level);
        availibleCores.Remove(found);
        Debug.Log("UUUUUUU" + found == null);
        return found;
    }

    public void AddCore(EnergyCore toAdd)
    {
        if(toAdd == null)
        {
            Debug.LogError("HUEHUEHHE");
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

}
