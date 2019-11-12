using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Text DescriptionText;
    public Text PriceText;

    Upgrade upgrade;
    public int UpgradeIndex;

    private void Start()
    {
        upgrade = UpgradesManager.Manager.GetUpgrade(UpgradeIndex);

        upgrade.OnUpgrade.AddListener(() => UpdateDisplay());
        UpdateDisplay();
    }

    public void UseUpgrade()
    {
        upgrade = UpgradesManager.Manager.GetUpgrade(UpgradeIndex);
        upgrade.Increase();
    }

    private void UpdateDisplay()
    {
        DescriptionText.text = upgrade.GetDescription();
        PriceText.text = PlayerMoney.GetMoneyString(upgrade.GetUpgradeCost());
    }
}
