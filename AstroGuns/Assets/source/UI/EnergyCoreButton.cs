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
        FindObjectOfType<CoresContainer>().currentCoreLevel = Level;
        FindObjectOfType<CoresContainer>().currentCoreType = Type;
        MenuManager.Instance.CloseAllPanels();
        MenuManager.Instance.OpenPanel(Panels.EnergyCoreDet);
    }

}
