using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergyCoreButton : MonoBehaviour
{
    public int Level;
    public EnergyCore.EnergyCoreType Type;

    public class EnergyCoreButtonEvent : UnityEvent<int, EnergyCore.EnergyCoreType> { };
    public EnergyCoreButtonEvent OnEquipButtonPressed = new EnergyCoreButtonEvent();


    public void OnPress()
    {
        Debug.Log("!!!!" + Level);
        bool equiped = FindObjectOfType<CoresContainer>().Equip(Level, Type);
        if(equiped)
            MenuManager.Instance.CloseAllPanels();
        else
            Debug.Log("DUPA");
    }

}
