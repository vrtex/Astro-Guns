using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoController : MonoBehaviour
{
    public Button CloseButton;
    public Image WeaponIcon;
    public Text IncomeText;
    public List<Image> CoreImages;
    public Text CoresInfo;

    public Sprite EmptyWeaponIcon;
    public Sprite EmptyCoreIcon;
    public EnergyCoreImageMapping ProfitIcons;
    public EnergyCoreImageMapping HasteImages;
    public EnergyCoreImageMapping FortuneImages;

    public SlotController currentSlot { get; private set; }
    public int currentCoreSlotIndex { get; private set; }

    private void Start()
    {
        CloseButton.onClick.AddListener(() => MenuManager.Instance.ClosePanelAndBackground(Panels.ItemInfo));
    }

    public void SetToSlot(SlotController slot)
    {
        currentSlot = slot;
        int weaponIndex = slot.weaponIndex;
        WeaponObject weapon = Inventory.GetWeapon(weaponIndex);
        WeaponIcon.sprite = weapon == null ? EmptyWeaponIcon : weapon.sprite;

        UpdateCoresInfo();

        IncomeText.text = "Earning: " + slot.LastIncomeAmount + " per second";
    }

    private void UpdateCoresInfo()
    {
        string CoresInfoDescription = "";
        for(int i = 0; i < CoreImages.Count; ++i)
        {
            if(i >= currentSlot.managedSlot.EnergyCores.Count || currentSlot.managedSlot.GetCoreAtIndex(i) == null)
            {
                CoreImages[i].sprite = EmptyCoreIcon;
                continue;
            }

            EnergyCore currentCore = currentSlot.managedSlot.EnergyCores[i];
            Debug.Log(GetImageMapping(currentCore == null ? EnergyCore.EnergyCoreType.Fortune : currentCore.Type));
            CoreImages[i].sprite = currentCore == null ? EmptyCoreIcon : GetImageMapping(currentCore.Type).sprites[currentCore.Level];
            CoresInfoDescription += currentCore.Description + "\n";
        }

        CoresInfo.text = CoresInfoDescription == "" ? "No cores" : "Some cores";
    }

    private EnergyCoreImageMapping GetImageMapping(EnergyCore.EnergyCoreType type)
    {
        return type == EnergyCore.EnergyCoreType.Fortune ? FortuneImages :
            type == EnergyCore.EnergyCoreType.Haste ? HasteImages :
            type == EnergyCore.EnergyCoreType.Profit ? ProfitIcons :
            null;
    }

    public void OpenEnergyCoresPanel(int coreSlotIndex)
    {
        // FUCK THIS MOTHERFUCKING GAY SHIT
        // FUUUUUUUUUUUUUUUUUUUUUUUCK
        FindObjectOfType<CoresContainer>().SetCurrentCoreSlot(new KeyValuePair<Slot, int>(currentSlot.managedSlot, coreSlotIndex));
        MenuManager.Instance.ClosePanel(Panels.ItemInfo);
        MenuManager.Instance.OpenPanel(Panels.EnergyCore);
    }

}
