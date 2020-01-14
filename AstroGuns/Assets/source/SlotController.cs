using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public Animation bumpAnimation;
    public Text incomeText;
    public Button InfoButton;
    [HideInInspector]
    public double LastIncomeAmount { get; private set; }

    public int weaponIndex { get; private set; }

    public Slot managedSlot { get; private set;  }

    private void Start()
    {
        weaponIndex = transform.GetSiblingIndex();
        managedSlot = Inventory.slots[weaponIndex];
        InfoButton.onClick.AddListener(() => OpenInfoPanel());
    }

    public void BumpIncome(double amount)
    {
        LastIncomeAmount = amount;
        incomeText.text = PlayerMoney.GetMoneyString(amount);
        bumpAnimation.Play();
    }

    public void OpenInfoPanel()
    {
        MenuManager.Instance.OpenPanel(Panels.ItemInfo);
        GameObject itemInfoPanel = MenuManager.Instance.GetPanel(Panels.ItemInfo);
        itemInfoPanel.GetComponent<ItemInfoController>().SetToSlot(this);
    }
}
