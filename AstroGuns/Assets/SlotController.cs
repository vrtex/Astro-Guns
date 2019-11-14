using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public Animation bumpAnimation;
    public Text incomeText;

    public void BumpIncome(double amount)
    {
        Debug.Log(bumpAnimation.name);
        incomeText.text = PlayerMoney.GetMoneyString(amount);
        bumpAnimation.Play();
    }
}
