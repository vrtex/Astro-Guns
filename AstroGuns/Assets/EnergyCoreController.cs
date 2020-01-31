using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCoreController : MonoBehaviour
{
    public Button craftButton;
    public Button assignButton;
    public Image coreImage;
    public Image previousCoreImage;
    public Text coreNameText;
    public Text coreDescriptionText;
    public Text coreAmountText;

    private readonly List<string> levelNames = new List<string> { "Common", "Improved",  "Rare", "Epic", "Legendary"};

    private Slot currentSlot;
    private int currentSlotIndex;
    private int currentCoreLevel;
    private EnergyCore.EnergyCoreType currentCoreType;

    public void Craft()
    {
        CoresContainer container = GetCurrentContainer();
        for(int i = 0; i < 4; ++i)
            container.RemoveCore(currentCoreLevel - 1, currentCoreType);
        container.AddCore(new EnergyCore { Level = currentCoreLevel, Type = currentCoreType });
        UpdateCore();
    }

    public void Assign()
    {
        CoresContainer container = GetCurrentContainer();
        EnergyCore core = container.pollCore(currentCoreLevel, currentCoreType);
        currentSlot.EquipCore(currentSlotIndex, core);
        MenuManager.Instance.CloseAllPanels();
    }

    public void Unassign()
    {
        CoresContainer container = GetCurrentContainer();
        currentSlot.UnequipCore(currentSlotIndex);
        MenuManager.Instance.CloseAllPanels();
    }

    public void UpdateCore()
    {
        CoresContainer container = GetCurrentContainer();
        currentSlot = container.currentCoreSlot.Key;
        currentSlotIndex = container.currentCoreSlot.Value;
        currentCoreType = container.currentCoreType;
        currentCoreLevel = container.currentCoreLevel;

        string coreName = levelNames[currentCoreLevel] + " " + currentCoreType + " energy core";
        coreNameText.text = coreName;
        coreAmountText.text = container.CountCores(currentCoreLevel, currentCoreType).ToString();
        coreDescriptionText.text = EnergyCore.GetDescription(currentCoreLevel, currentCoreType);

        coreImage.sprite = container.GetIconForCore(currentCoreLevel, currentCoreType);
        previousCoreImage.sprite =currentCoreLevel == 0 ? null : container.GetIconForCore(currentCoreLevel - 1, currentCoreType);

        craftButton.enabled = currentCoreLevel > 0 && container.CountCores(currentCoreLevel - 1, currentCoreType) >= 4;
    }

    private CoresContainer GetCurrentContainer()
    {
        return FindObjectOfType<CoresContainer>();
    }
}
