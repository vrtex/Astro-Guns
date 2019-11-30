using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DailyReward : MonoBehaviour
{
	private static DailyReward	instance;
	public	static DailyReward	Instance { get => instance; }

	public UpgradesManager      upgradesManager = null;

	public GameObject           dailyButton	= null;
	public Transform			dailyRows	= null;
	private List<GameObject>    dailyCovers	= new List<GameObject>();

	void Awake()
    {
		if(instance == null) instance = this;

		for(int i = 0; i < 4; ++i)
		{
			for(int j = 0; j < dailyRows.GetChild(i).childCount; ++j)
			{
				GameObject cover = dailyRows.GetChild(i).GetChild(j).Find("Cover").gameObject;
				if(cover != null)
				{
					dailyCovers.Add(cover);
				}
			}
		}
	}

	void Start()
	{
		DateTimeSystem.Instance.afterGetTime.AddListener(ShowRewardButton);
	}

    void Update()
    {
        
    }

	public void ShowRewardButton()
	{
		//dailyButton.SetActive(true); ///DEBUG

		//jeśli nagroda nie została odebrana w jakiś dzień
		int tempDate = DateTimeSystem.Instance.GetCurrentDateNow() - DateTimeSystem.Instance.lastTimeDailyReward;
		if(tempDate > 1) DateTimeSystem.Instance.lastDailyReward = -1;

		//wyświetlenie przycisku
		if(DateTimeSystem.Instance.GetCurrentDateNow() > DateTimeSystem.Instance.lastTimeDailyReward)
		{
			dailyButton.SetActive(true);
			DateTimeSystem.Instance.lastTimeDailyReward = DateTimeSystem.Instance.GetCurrentDateNow();
		}
	}

	public void ShowCurrentReward()
	{
		dailyCovers[DateTimeSystem.Instance.lastDailyReward + 1].SetActive(false);
	}

	public void GetCurrentReward()
	{
		dailyCovers[DateTimeSystem.Instance.lastDailyReward + 1].SetActive(true);
		++DateTimeSystem.Instance.lastDailyReward;

		CalcReward(DateTimeSystem.Instance.lastDailyReward);

		if(DateTimeSystem.Instance.lastDailyReward == 15) DateTimeSystem.Instance.lastDailyReward = 4;

		dailyButton.SetActive(false);
		MenuManager.Instance.CloseAllPanels();
	}

	void CalcReward(int reward)
	{
		switch(reward)
		{
			case 0:
				GiveCredits(360.0);
			break;
			case 1:
				GiveEther(5.0);
			break;
			case 2:
				FillItem(12);
			break;
			case 3:
				GiveChest(0);
			break;
			case 4:
				GiveCredits(4320.0);
			break;
			case 5:
				GiveEther(10.0);
			break;
			case 6:
				FillItem(24);
			break;
			case 7:
				GiveChest(1);
			break;
			case 8:
				GiveCredits(8640.0);
			break;
			case 9:
				GiveEther(15.0);
			break;
			case 10:
				GiveCredits(17280.0);
			break;
			case 11:
				GiveChest(2);
			break;
			case 12:
				GiveCredits(34560.0);
			break;
			case 13:
				GiveEther(20.0);
			break;
			case 14:
				GiveCredits(69120.0);
			break;
			case 15:
				GiveChest(3);
			break;
		}
	}

	void GiveCredits(double creditsMultipler)
	{
		double allMoney = 0.0;
		List<Slot> ValidWeapons = Inventory.slots.FindAll((Slot S) => { return S.weapon != null; });
		foreach(Slot s in ValidWeapons)
		{
			allMoney += s.weapon.value;
		}

		MoneyPocket.Instance.Money.Add(allMoney * creditsMultipler);
	}

	void GiveEther(double ether)
	{
		MoneyPocket.Instance.Ether.Add(ether);
	}

	void FillItem(int amount)
	{
		for(int i = 0; i < amount; ++i)
		{
			upgradesManager.Spawner.ForceSpawnWeapon();
		}
	}

	void GiveChest(int lvl)
	{

	}
}
