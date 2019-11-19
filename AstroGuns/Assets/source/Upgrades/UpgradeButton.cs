using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Text DescriptionText;
    public Text PriceText;
    public Button UpgradeButButton;

    Upgrade upgrade;
    public int UpgradeIndex;

    private void Start()
    {
        upgrade = UpgradesManager.Manager.GetUpgrade(UpgradeIndex);

        upgrade.OnUpgrade.AddListener(() => UpdateDisplay());
        UpdateDisplay();

        MoneyPocket.Instance.Money.OnValueUpdated.AddListener(() => this.UpdateDisplay());
    }

    public void UseUpgrade()
    {
        double currentMoney = InventorySystem.Instance.Pocket.Money.ActualValue;
        if(currentMoney < upgrade.GetUpgradeCost())
            return;
        InventorySystem.Instance.Pocket.Money.Remove(upgrade.GetUpgradeCost());
        upgrade = UpgradesManager.Manager.GetUpgrade(UpgradeIndex);
        upgrade.Increase();
    }

    public void UpdateDisplay()
    {
        DescriptionText.text = upgrade.GetDescription();
        PriceText.text = PlayerMoney.GetMoneyString(upgrade.GetUpgradeCost());
        UpgradeButButton.interactable = !upgrade.IsMaxLevel && InventorySystem.Instance.Pocket.Money.ActualValue > upgrade.GetUpgradeCost();
    }
}
