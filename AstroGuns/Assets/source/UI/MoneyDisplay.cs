using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public MoneyPocket Pocket;
    public Text DisplayText;

    private void Start()
    {
        Pocket.Money.OnValueUpdated.AddListener(() => DisplayText.text = Pocket.Money.ToString());
    }
}
