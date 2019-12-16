using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoController : MonoBehaviour
{
    public Button CloseButton;
    public Image WeaponIcon;
    public Text IncomeText;

    public Sprite EmptyWeaponIcon;

    private void Start()
    {
        CloseButton.onClick.AddListener(() => MenuManager.Instance.ClosePanelAndBackground(Panels.ItemInfo));
    }

    public void SetToSlot(SlotController slot)
    {
        int weaponIndex = slot.weaponIndex;
        WeaponObject weapon = Inventory.GetWeapon(weaponIndex);
        WeaponIcon.sprite = weapon == null ? EmptyWeaponIcon : weapon.sprite;

        IncomeText.text = "Earning: " + slot.LastIncomeAmount + " per second";
    }
}
