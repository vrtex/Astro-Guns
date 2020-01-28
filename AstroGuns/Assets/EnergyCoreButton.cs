using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnergyCoreButton : MonoBehaviour
{
    public int Level;
    public EnergyCore.EnergyCoreType Type;
    public Text DisplayCountText;

    public class EnergyCoreButtonEvent : UnityEvent<int, EnergyCore.EnergyCoreType> { };
    public EnergyCoreButtonEvent OnEquipButtonPressed = new EnergyCoreButtonEvent();

    public void UpdateDisplayedText()
    {
        int countedCores = FindObjectOfType<CoresContainer>().CountCores(Level, Type);
        DisplayCountText.text = countedCores.ToString();
    }

    public void OnPress()
    {
        Debug.Log("!!!!" + Level + " " + Type);
        bool equiped = FindObjectOfType<CoresContainer>().Equip(Level, Type);
        if(equiped)
            MenuManager.Instance.CloseAllPanels();
        else
            Debug.Log("DUPA");
    }

}
