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

    private SlotController currentSlot;

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
            if(i >= currentSlot.managedSlot.EnergyCores.Count)
            {
                CoreImages[i].sprite = EmptyCoreIcon;
                continue;
            }

            EnergyCore currentCore = currentSlot.managedSlot.EnergyCores[i];
            CoreImages[i].sprite = GetImageMapping(currentCore.Type).sprites[currentCore.Level];
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

    public void OpenEnergyCoreInfo(int coreSlotIndex)
    {

    }
}
