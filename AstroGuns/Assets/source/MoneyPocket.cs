using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPocket : MonoBehaviour
{
    public PlayerMoney Money
    {
        get;
        private set;
    }

    private void Awake()
    {
        Money = new PlayerMoney();
    }

}
