using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCoreLibraryController : MonoBehaviour
{
    [HideInInspector]
    public Slot CurrentlyControlledSlot;

    public CoresContainer coresContainer;

    public void SelectCore(int level, EnergyCore.EnergyCoreType coreType)
    {

    }

    public void Unequip()
    {
        Slot currentSlot = coresContainer.currentCoreSlot.Key;
        int slotIndex = coresContainer.currentCoreSlot.Value;
        EnergyCore returnedCore = currentSlot.GetCoreAtIndex(slotIndex);

        currentSlot.UnequipCore(slotIndex);
        if(returnedCore != null)
            coresContainer.AddCore(returnedCore);
        MenuManager.Instance.CloseAllPanels();
    }

    public void CraftAll()
    {
        for(int i = 0; i < 4; ++i)
        {
            MergeCores(i, EnergyCore.EnergyCoreType.Fortune);
            MergeCores(i, EnergyCore.EnergyCoreType.Haste);
            MergeCores(i, EnergyCore.EnergyCoreType.Profit);
        }

        MenuManager.Instance.CloseAllPanels();
    }

    private void MergeCores(int level, EnergyCore.EnergyCoreType type)
    {
        while(coresContainer.CountCores(level, type) >= 3)
        {
            coresContainer.RemoveCores(3, level, type);
            coresContainer.AddCore(new EnergyCore { Level = level + 1, Type = type });
        }
    }
}
