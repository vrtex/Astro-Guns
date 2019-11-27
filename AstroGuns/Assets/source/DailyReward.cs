using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DailyReward : MonoBehaviour
{
	private static DailyReward	instance;
	public	static DailyReward	Instance { get => instance; }

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
		dailyButton.SetActive(false);
		MenuManager.Instance.CloseAllPanels();
	}
}
