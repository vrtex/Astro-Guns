using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class BonusManager : MonoBehaviour
{
	private static BonusManager instance;
	public static BonusManager Instance { get => instance; }

	public UpgradesManager      upgradesManager = null;

	[Header("Bonus Buttons")]
	public Button               fill		= null;
	public Button               multipler	= null;
	public Button               autoMerge	= null;
	public Button               buyAuto     = null;


	[Header("Active bar")]
	public GameObject           activeBonus     = null;
	public Slider               activeSlider    = null;
	public Image                activeImage     = null;

	[Header("Bonus List")]
	public List<Boost>          boosts          = new List<Boost>();

	private float               activeBoostTime = 0f;
	private int                 currentBoost    = -1;
	private bool                boostIsActive   = false;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
	}

	void Start()
	{
		AdsManager.Instance.rewarderComplited.AddListener(AfterWatchAd);
	}

	public void CheckBonus()
	{
		if(Advertising.IsRewardedAdReady())
		{
			fill.interactable = true;
			multipler.interactable = true;
			autoMerge.interactable = true;

			if(currentBoost != -1)
			{
				multipler.interactable = false;
				autoMerge.interactable = false;
			}
		}
		else
		{
			fill.interactable = false;
			multipler.interactable = false;
			autoMerge.interactable = false;
		}

		if(currentBoost != -1)
		{
			buyAuto.interactable = false;
		}
		else
		{
			if(MoneyPocket.Instance.Ether.ActualValue >= 15)
			{
				buyAuto.interactable = true;
			}
			else
			{
				buyAuto.interactable = false;
			}
		}
	}

	void Update()
	{
		if(boostIsActive)
		{
			if(activeBoostTime > 0f) //aktywny przyśpieszacz
			{
				activeBoostTime -= Time.deltaTime;
				if(activeBoostTime < 0f) activeBoostTime = 0f;
				activeSlider.value = activeBoostTime / boosts[currentBoost].time;
			}
			else //zkończenie przyśpieszacza
			{
				currentBoost = -1;
				activeBonus.SetActive(false);
				boostIsActive = false;
			}
		}
	}

	public void FillBonus()
	{
		for(int i = 0; i < 12; ++i)
		{
			upgradesManager.Spawner.ForceSpawnWeapon();
		}
	}

	public void ActiveAd(int index)
	{
		currentBoost = index;
		AdsManager.Instance.RewardedAds();
	}

	public void AfterWatchAd()
	{
		if(currentBoost == -1) return;

		if(currentBoost == 2)
		{
			FillBonus();
			return;
		}

		activeBoostTime = boosts[currentBoost].time;
		activeSlider.value = 1f;
		activeImage.sprite = boosts[currentBoost].sprite;

		activeBonus.SetActive(true);
		boostIsActive = true;

		currentBoost = -1;
		CheckBonus();
	}

	public void BuyAutoMerge()
	{
		currentBoost = 1;
		AfterWatchAd();
	}
}
