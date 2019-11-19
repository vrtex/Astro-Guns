using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPocket : MonoBehaviour
{
    private static MoneyPocket instance;
    public static MoneyPocket Instance { get => instance; }

    private Queue<double> LastIncreases = new Queue<double>();

    public Text MoneyPerSecondLabel;
    public PlayerMoney Money
    {
        get;
        private set;
    }

    public PlayerMoney Ether {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        Money = new PlayerMoney();
        Ether = new PlayerMoney();
        StartCoroutine(TickMoney());
    }

    private IEnumerator TickMoney()
    {
        while(true)
        {
            List<Slot> ValidWeapons = Inventory.slots.FindAll((Slot S) => { return S.weapon != null; });
            double thisIncrease = 0;
            foreach(Slot s in ValidWeapons)
            {
                thisIncrease += s.weapon.value;
                Money.Add(s.weapon.value);
                int slotNumber = Inventory.slots.FindIndex((Slot _s) => { return _s == s; });
                SlotController slotController = InventorySystem.Instance.weaponsInSlots[slotNumber].transform.parent.GetComponent<SlotController>();
                slotController.BumpIncome(s.weapon.value);
            }

            while(LastIncreases.Count > 10)
                LastIncreases.Dequeue();
            LastIncreases.Enqueue(thisIncrease);

            double sum = 0;
            foreach(double d in LastIncreases)
                sum += d;

            sum = sum / LastIncreases.Count;

            MoneyPerSecondLabel.text = PlayerMoney.GetMoneyString(sum) + "/second";
            //Debug.Log(sum);
            //Debug.Log(PlayerMoney.GetMoneyString(sum));

            if(ValidWeapons.Count != 0)
                AudioManager.Instance.Play("coin");

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void TryAddEther()
    {
        if(UnityEngine.Random.value > Upgrade.Spawner.EtherChance)
            return;

        Ether.Add(1);
    }
}
